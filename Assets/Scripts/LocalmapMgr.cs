using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalmapMgr : MonoBehaviour {

    public SpriteRenderer _left_spr;
    public SpriteRenderer _right_spr;
    public SpriteRenderer _middle_spr;
    public SpriteRenderer _village_spr;
    public SpriteRenderer _mine_spr;
    public SpriteRenderer _battleground_spr;

    public void UpdateScene(int world_pos)
    {
        if(world_pos > 0)
        {
            // 전투맵 업데이트
            if(world_pos == 1)
            {
                _left_spr.sprite = _village_spr.sprite;
                _right_spr.sprite = _battleground_spr.sprite;
                _middle_spr.sprite = _battleground_spr.sprite;
            }
            else 
            {
                _left_spr.sprite = _battleground_spr.sprite;
                _right_spr.sprite = _battleground_spr.sprite;
                _middle_spr.sprite = _battleground_spr.sprite;
            }
        }
        else if(world_pos < 0)
        {
            // 채광맵 업데이트
            if(world_pos == -1)
            {
                _left_spr.sprite = _mine_spr.sprite;
                _right_spr.sprite = _village_spr.sprite;
                _middle_spr.sprite = _mine_spr.sprite;
            }
            else
            {
                _left_spr.sprite = _mine_spr.sprite;
                _right_spr.sprite = _mine_spr.sprite;
                _middle_spr.sprite = _mine_spr.sprite;
            }
        }
        else 
        {
            // 마을맵 업데이트
            _left_spr.sprite = _mine_spr.sprite;
            _right_spr.sprite = _battleground_spr.sprite;
            _middle_spr.sprite = _village_spr.sprite;
        }
    }

}

