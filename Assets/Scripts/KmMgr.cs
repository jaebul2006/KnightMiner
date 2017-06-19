using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KmMgr : MonoBehaviour {

    public Km[] _kms;

	void Start () 
    {
        //_kms[0].SetLeader();
	    
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

}
