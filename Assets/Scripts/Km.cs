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

	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _move_state = MoveState.LEFT;
            _spr.FlipX = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _move_state = MoveState.RIGHT;
            _spr.FlipX = true;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            _move_state = MoveState.NONE;
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
            LEFT_MAX_POS = ((float)_cur_world_pos - 0.5f) * 10f;
            RIGHT_MAX_POS = ((float)_cur_world_pos + 0.5f) * 10f;
        }
        else
        {
            LEFT_MAX_POS = -5f;
            RIGHT_MAX_POS = 5f;
        }
        Debug.Log(LEFT_MAX_POS + ":" + RIGHT_MAX_POS);
    }

}
