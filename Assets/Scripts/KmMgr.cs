using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KmMgr : MonoBehaviour {

    public Km[] _kms;
    public Text _ui_km_cnt;

	void Start () 
    {
        UpdateUI();
	}
	
	void Update () 
    {
        CheckLeader();
	}

    public void UpdateScene(int worldpos)
    {
        for (int i = 0; i < _kms.Length; i++ )
        {
            _kms[i].SetLimitLeftRightPos(worldpos);
        }
    }

    // 좌측이동일때는 가장왼쪽에 있는넘이, 우측이동일 때는 가장 오른쪽에 있는넘이 리더가 된다.
    private void CheckLeader()
    {
        int reader_idx = 0;
        float x_v = _kms[0].transform.localPosition.x;

        if(_kms[0].GetMoveState() == Km.MoveState.LEFT)
        {
            for (int i = 0; i < _kms.Length; i++)
            {
                if(_kms[i].transform.localPosition.x < x_v)
                {
                    reader_idx = i;
                    x_v = _kms[i].transform.localPosition.x;
                }
                _kms[i].UnSetLeader();
            }
            _kms[reader_idx].SetLeader();
        }
        else if(_kms[0].GetMoveState() == Km.MoveState.RIGHT)
        {
            for (int i = 0; i < _kms.Length; i++)
            {
                if (_kms[i].transform.localPosition.x > x_v)
                {
                    reader_idx = i;
                    x_v = _kms[i].transform.localPosition.x;
                }
                _kms[i].UnSetLeader();
            }
            _kms[reader_idx].SetLeader();
        }
    }

    public bool IsJumpingAnyone()
    {
        for(int i=0; i<_kms.Length; i++)
        {
            if(_kms[i].IsJumping())
            {
                return true;
            }
        }
        return false;
    }

    public void TouchLeftBtn()
    {
        for (int i = 0; i < _kms.Length; i++)
        {
            _kms[i].TouchLeftBtn();
        }
    }

    public void TouchRightBtn()
    {
        for (int i = 0; i < _kms.Length; i++)
        {
            _kms[i].TouchRightBtn();
        }
    }

    private void UpdateUI()
    {
        _ui_km_cnt.text = _kms.Length.ToString();
    }

    // 마을에서의 정산을 위한 멈춤
    public void StopKM()
    {
        for (int i = 0; i < _kms.Length; i++)
        {
            _kms[i].Stop();
        }
    }

    public Transform GetLeader()
    {
        for (int i = 0; i < _kms.Length; i++)
        {
            if(_kms[i].IsLeader())
            {
                return _kms[i].transform;
            }
        }
        return null;
    }

    public void IdleAniSet() // 기본 idle 애니메이션으로 모든 km들을 세팅한다.
    {
        for (int i = 0; i < _kms.Length; i++)
        {
            _kms[i].IdleAniSet();
        }
    }

    public float GetAverPosX() // km 들의 평균위치를 전달.
    {
        float aver_pos = 0f;
        for (int i = 0; i < _kms.Length; i++)
        {
            float x = _kms[i].transform.position.x;
            aver_pos += x;
        }
        aver_pos /= _kms.Length;

        return aver_pos;
    }

    // 캐온골드를 초기화.
    public void InitAsset()
    {
        for (int i = 0; i < _kms.Length; i++)
        {
            _kms[i].InitAsset();
        }
    }

}
