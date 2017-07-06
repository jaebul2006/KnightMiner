using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Km : MonoBehaviour {

    public enum MoveState
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

    bool _is_leader = false;
    BoxCollider2D _box_collider;

    int _mining_cap_min = 5;
    int _mining_cap_max = 15;
    int _possession_limit = 100;
    int _cur_mining_gold = 0;

    public Transform _goldmax;

    private bool _is_jumping;
    private KmMgr _km_mgr;
    private GameObject _max_goldcaution_go; // 최대골드가되면 표시되는 스프라이트.

    public GameObject _gold_ui_go;
    public Transform _gold_ui_top_tm;

    int _max_hp = 100;
    int _cur_hp = 100;
    int _ac = -10;
    int _critical = 2;
    int _hit = 50; // 적중, 회피공식은 정하기나름.

    void Start()
    {
        SetLimitLeftRightPos(_cur_world_pos);
        _box_collider = gameObject.GetComponent<BoxCollider2D>();
        _km_mgr = GameObject.Find("KmMgr").GetComponent<KmMgr>();
        for(int i=0; i<transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if(child.name == "MaxGoldCaution")
            {
                _max_goldcaution_go = child.gameObject;
                _max_goldcaution_go.SetActive(false);
            }
        }
        _gold_ui_go.SetActive(false);
    }

    void UpdateAnimation(string animation_state)
    {
        string prev_spr_name = "";
        string[] split_name;
        string new_sprite_name = "";

        switch(animation_state)
        {
            case "Mining_Move":
                prev_spr_name = _spr.CurrentSprite.name;
		        split_name = prev_spr_name.Split ('_');
		        new_sprite_name = split_name [0] + "_" + "a";
		        _spr.SetSprite (_spr.GetSpriteIdByName (new_sprite_name));
                ShowPosssesionGold(false);
                break;

            case "Mining_JumpBack":
                prev_spr_name = _spr.CurrentSprite.name;
		        split_name = prev_spr_name.Split ('_');
		        new_sprite_name = split_name [0] + "_" + "b";
		        _spr.SetSprite (_spr.GetSpriteIdByName (new_sprite_name));
                break;

            case "Mining_Backhome":
                prev_spr_name = _spr.CurrentSprite.name;
		        split_name = prev_spr_name.Split ('_');
		        new_sprite_name = split_name [0] + "_" + "c";
		        _spr.SetSprite (_spr.GetSpriteIdByName (new_sprite_name));
                ShowPosssesionGold(true);
                break;

            case "Battle_Move":
                prev_spr_name = _spr.CurrentSprite.name;
		        split_name = prev_spr_name.Split ('_');
		        new_sprite_name = split_name [0] + "_" + "d";
		        _spr.SetSprite (_spr.GetSpriteIdByName (new_sprite_name));
                ShowPosssesionGold(false);
                break;

            case "Battle_JumpBack":
                prev_spr_name = _spr.CurrentSprite.name;
		        split_name = prev_spr_name.Split ('_');
		        new_sprite_name = split_name [0] + "_" + "e";
		        _spr.SetSprite (_spr.GetSpriteIdByName (new_sprite_name));
                break;

            case "Battle_Backhome":
                prev_spr_name = _spr.CurrentSprite.name;
                split_name = prev_spr_name.Split('_');
                new_sprite_name = split_name[0] + "_" + "f";
                _spr.SetSprite(_spr.GetSpriteIdByName(new_sprite_name));
                ShowPosssesionGold(true);
                break;
        }
    }

    private IEnumerator JumpBack(MoveState dir)
    {
        float elapsed_time = 0f;
        //float jump_time = 0.5f;
        float jump_time = 1f;
		float jump_height = Random.Range (1f, 1.5f);
        _box_collider.enabled = false;
        _is_jumping = true;

        while(true)
        {
            yield return null;
            elapsed_time += Time.deltaTime;
            float t = Mathf.Lerp(0f, 1f, elapsed_time / jump_time);
            //float y = Mathf.Sin(Mathf.PI * (t * 2f)) * jump_height;
            //float x = Mathf.Sin(Mathf.PI * (t * 2f)) * 0.2f * jump_height;
            float y = Mathf.Sin(Mathf.PI * t) * jump_height * 2f;
            float x = Mathf.Sin(Mathf.PI * t) * 0.06f * jump_height;
            if(dir == MoveState.LEFT)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + x, y, transform.localPosition.z);
                UpdateAnimation("Mining_JumpBack");
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x - x, y, transform.localPosition.z);
                UpdateAnimation("Battle_JumpBack");
            }

            if(t >= jump_time)
            {
                y = Mathf.Clamp(y, 0f, 0f);
                if (dir == MoveState.LEFT)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x + x, y, transform.localPosition.z);
                    UpdateAnimation("Mining_Move");
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x - x, y, transform.localPosition.z);
                    UpdateAnimation("Battle_Move");
                }
                _box_collider.enabled = true;
                _is_jumping = false;
                break;
            }
        }
    }

    void FixedUpdate()
    {
        if (!_cmrmgr.IsMoving() && (_is_jumping == false))
        {
            if (_move_state == MoveState.LEFT)
            {
                transform.position -= new Vector3(2f * Time.deltaTime, 0f, 0f);
                if (transform.position.x < LEFT_MAX_POS && _is_leader)
                {
                    _cmrmgr.ReqMoveCamera(CamMgr.Direction.Left);
                }
            }
            else if (_move_state == MoveState.RIGHT)
            {
                transform.position += new Vector3(2f * Time.deltaTime, 0f, 0f);
                if (transform.position.x > RIGHT_MAX_POS && _is_leader)
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
            int min_cap = Random.RandomRange(_mining_cap_min, _mining_cap_max + 1);
            _cur_mining_gold += min_cap;
            if (_cur_mining_gold >= _possession_limit)
            {
                _cur_mining_gold = _possession_limit;
                _max_goldcaution_go.SetActive(true);
            }
            collision.collider.gameObject.GetComponent<Mineral>().Damage(min_cap);
            StartCoroutine("JumpBack", _move_state);

            UpdateGoldBarUI();
        }
        if (collision.collider.name == "Monster(Clone)")
        {
            int min_cap = Random.RandomRange(_mining_cap_min, _mining_cap_max + 1);
            _cur_mining_gold += min_cap;
            if (_cur_mining_gold >= _possession_limit)
            {
                _cur_mining_gold = _possession_limit;
                _max_goldcaution_go.SetActive(true);
            }
            collision.collider.gameObject.GetComponent<Monster>().Damage(min_cap);
            StartCoroutine("JumpBack", _move_state);
        }
    }

    public void SetLeader()
    {
        _is_leader = true;
    }

    public void UnSetLeader()
    {
        _is_leader = false;
    }

    public bool IsLeader()
    {
        return _is_leader;
    }

    public MoveState GetMoveState()
    {
        return _move_state;
    }

    private void ShowPosssesionGold(bool flag)
    {
        if(flag)
        {
            _goldmax.localScale = Vector3.one;
        }
        else
        {
            _goldmax.localScale = Vector3.zero;
        }
    }

    public bool IsJumping()
    {
        return _is_jumping;
    }

    public void TouchLeftBtn()
    {
        if (_km_mgr.IsJumpingAnyone())
        {
            if(_is_jumping)
            {
                // 내가 점프중이다
                StartCoroutine("JumpLasyMove", Constans.eMoveDirection.LEFT);
                _move_state = MoveState.NONE;
                _spr.FlipX = true;
            }
            else
            {
                // 누군가가 점프중이지만 나는아니다
                _move_state = MoveState.NONE;
                _spr.FlipX = true;
                if (_cur_world_pos < 0)
                {
                    UpdateAnimation("Mining_Move");
                }
                else if (_cur_world_pos > 0)
                {
                    UpdateAnimation("Battle_Backhome");
                }
                else
                {
                    UpdateAnimation("Mining_Move");
                }
            }
        }
        else
        {
            _move_state = MoveState.LEFT;
            _spr.FlipX = true;
            if (_cur_world_pos < 0)
            {
                UpdateAnimation("Mining_Move");
            }
            else if (_cur_world_pos > 0)
            {
                UpdateAnimation("Battle_Backhome");
            }
            else
            {
                UpdateAnimation("Mining_Move");
            }
        }
    }

    public void TouchRightBtn()
    {
        if (_km_mgr.IsJumpingAnyone())
        {
            if(_is_jumping)
            {
                // 내가 점프중.
                StartCoroutine("JumpLasyMove", Constans.eMoveDirection.RIGHT);
                _move_state = MoveState.NONE;
                _spr.FlipX = false;
            }
            else
            {
                // 누군가 점프중이나 난 아님.
                _move_state = MoveState.NONE;
                _spr.FlipX = false;
                if (_cur_world_pos < 0)
                {
                    UpdateAnimation("Mining_Move");
                }
                else if (_cur_world_pos > 0)
                {
                    UpdateAnimation("Battle_Backhome");
                }
                else
                {
                    UpdateAnimation("Mining_Move");
                }
            }
        }
        else
        {
            _move_state = MoveState.RIGHT;
            _spr.FlipX = false;
            if (_cur_world_pos < 0)
            {
                UpdateAnimation("Mining_Backhome");
            }
            else if (_cur_world_pos > 0)
            {
                UpdateAnimation("Battle_Move");
            }
            else
            {
                UpdateAnimation("Battle_Move");
            }
        }
    }

    // 점프중인 광부가 있을때 이동키가 눌렸을때처리, 일종의 입력버퍼 개념으로 동작.
    private IEnumerator JumpLasyMove(Constans.eMoveDirection dir)
    {
        while(true)
        {
            yield return null;
            if(!_km_mgr.IsJumpingAnyone())
            {
                if(dir == Constans.eMoveDirection.RIGHT)
                {
                    _km_mgr.TouchRightBtn();
                }
                else
                {
                    _km_mgr.TouchLeftBtn();
                }
                break;
            }
        }
    }

    public void Stop()
    {
        _move_state = MoveState.NONE;
    }

    public void IdleAniSet()
    {
        UpdateAnimation("Mining_Move");
    }

    public void InitAsset()
    {
        _cur_mining_gold = 0;
    }

    private void UpdateGoldBarUI()
    {
        StopCoroutine("FadeoutGoldBarUI");
        StartCoroutine("FadeoutGoldBarUI");
        _gold_ui_go.SetActive(true);
        float tmp = (float)_cur_mining_gold / (float)_possession_limit;
        _gold_ui_top_tm.localScale = new Vector3(tmp, 1f, 1f);
    }

    private IEnumerator FadeoutGoldBarUI()
    {
        float cur_time = 0f;

        while(true)
        {
            yield return null;
            cur_time += Time.deltaTime;
            if (cur_time > Constans.MAX_GOLD_BAR_UI_TIME)
            {
                _gold_ui_go.SetActive(false);
                break;
            }
        }
    }

    public void WaitForRightMove()
    {
        if(_is_jumping)
        {
            Stop();
        }
    }

}

