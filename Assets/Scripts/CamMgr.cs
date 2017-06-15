using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 화면은 항상 중앙, 좌우가 준비된 화면이어야 한다.
public class CamMgr : MonoBehaviour 
{
    public enum Direction
    {
        Left = 0,
        Right
    }

	float TEN_SIXTEEN_RATIO = 0.625f;

    public WorldmapMgr _worldmapmgr;

    private bool _is_moving = false;

	void Start () 
    {
		ScrResolution ();	
	}
	
    public void ReqMoveCamera(Direction dir)
    {
        if(!_is_moving)
        {
            StartCoroutine("MoveCamera", dir);
        }
    }

    IEnumerator MoveCamera(Direction dir)
    {
        int world_pos = _worldmapmgr.GetWorldPos();
        _is_moving = true;

        while (true)
        {
            yield return null;
            if (dir == Direction.Left)
            {
                if (transform.localPosition.x > (_worldmapmgr.GetWorldPos()-1) * 10f)
                {
                    transform.localPosition -= new Vector3(30f * Time.deltaTime, 0f, 0f);
                }
                else
                {
                    _is_moving = false;
                    //transform.localPosition = new Vector3(-10f, 0f, 0f);
                    world_pos--;
                    CmrMoveEnd(world_pos);
                    break;
                }
            }
            else
            {
                if (transform.localPosition.x < (_worldmapmgr.GetWorldPos()+1) * 10f)
                {
                    transform.localPosition += new Vector3(30f * Time.deltaTime, 0f, 0f);
                }
                else
                {
                    _is_moving = false;
                    //transform.localPosition = new Vector3(10f, 0f, 0f);
                    world_pos++;
                    CmrMoveEnd(world_pos);
                    break;
                }
            }
        }
    }

	private void ScrResolution()
	{
		// 목표 해상도는 16:10.
//		float cur_ratio = (float)Screen.width / (float)Screen.height;
//		if (cur_ratio < TEN_SIXTEEN_RATIO) {
//			Camera.main.aspect = 1f/1.6f;
//		}
		//Camera.main.aspect = 1f/1.6f;
		Camera.main.aspect = TEN_SIXTEEN_RATIO;
	}

    private void CmrMoveEnd(int world_pos)
    {
        _worldmapmgr.UpdateWorld(world_pos);
    }

    public void UpdateScene()
    {
        transform.localPosition = new Vector3(_worldmapmgr.GetWorldPos() * 10f, 0f, 0f);
    }

    public bool IsMoving()
    {
        return _is_moving;
    }

}
