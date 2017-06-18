using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Km : MonoBehaviour {

    enum MoveState
    {
        NONE = 0,
        LEFT,
        RIGHT
    }
    MoveState _move_state = MoveState.NONE;

    public tk2dSprite _spr;

    int _cur_world_pos = 0;
    float LEFT_MAX_POS = 0f;
    float RIGHT_MAX_POS = 0f;

    public CamMgr _cmrmgr;

    void Start()
    {
        SetLimitLeftRightPos(_cur_world_pos);
    }

	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _move_state = MoveState.LEFT;
            _spr.FlipX = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _move_state = MoveState.RIGHT;
            _spr.FlipX = false;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            _move_state = MoveState.NONE;
        }
	}

    private IEnumerator JumpBack(MoveState dir)
    {
        float elapsed_time = 0f;
        float jump_time = 0.5f;
		float jump_height = Random.Range (1f, 1.5f);
		string prev_spr_name = _spr.CurrentSprite.name;
		string[] split_name = prev_spr_name.Split ('_');
		string new_sprite_name = split_name [0] + "_" + "b";
		_spr.SetSprite (_spr.GetSpriteIdByName (new_sprite_name));

        while(true)
        {
            yield return null;
            elapsed_time += Time.deltaTime;
            float t = Mathf.Lerp(0f, 1f, elapsed_time / jump_time);
			float y = Mathf.Sin(Mathf.PI * (t * 2f)) * jump_height;
			float x = Mathf.Sin(Mathf.PI * (t * 2f)) * 0.2f * jump_height;
            if(dir == MoveState.LEFT)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + x, y, transform.localPosition.z);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x - x, y, transform.localPosition.z);
            }

            if(t >= jump_time)
            {
                y = Mathf.Clamp(y, 0f, 0f);
                if (dir == MoveState.LEFT)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x + x, y, transform.localPosition.z);
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x - x, y, transform.localPosition.z);
                }
				_spr.SetSprite (_spr.GetSpriteIdByName (prev_spr_name));
                break;
            }
        }
    }

    void FixedUpdate()
    {
        if (!_cmrmgr.IsMoving())
        {
            if (_move_state == MoveState.LEFT)
            {
                transform.position -= new Vector3(2f * Time.deltaTime, 0f, 0f);
                if (transform.position.x < LEFT_MAX_POS)
                {
                    _cmrmgr.ReqMoveCamera(CamMgr.Direction.Left);
                }
            }
            else if (_move_state == MoveState.RIGHT)
            {
                transform.position += new Vector3(2f * Time.deltaTime, 0f, 0f);
                if (transform.position.x > RIGHT_MAX_POS)
                {
                    _cmrmgr.ReqMoveCamera(CamMgr.Direction.Right);
                }
            }
        }
    }

    // 현재 월드맵상의 좌표를 근거로 왼쪽최대 로컬좌표, 오른쪽최대 로컬좌표를 설정한다.
    public void SetLimitLeftRightPos(int world_pos)
    {
        _cur_world_pos = world_pos;

        if(_cur_world_pos != 0)
        {
            LEFT_MAX_POS = ((float)_cur_world_pos - 0.5f) * Constans.LOCAL_SCENE_WIDTH;
            RIGHT_MAX_POS = ((float)_cur_world_pos + 0.5f) * Constans.LOCAL_SCENE_WIDTH;
        }
        else
        {
            LEFT_MAX_POS = -5f;
            RIGHT_MAX_POS = 5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Mineral(Clone)")
        {
            collision.collider.gameObject.GetComponent<Mineral>().AddPunch();
            StartCoroutine("JumpBack", _move_state);
        }
    }

}
