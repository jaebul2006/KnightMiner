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
        float start = _worldmapmgr.GetWorldPos() * Constans.LOCAL_SCENE_WIDTH;
        float left_end = (_worldmapmgr.GetWorldPos() - 1) * Constans.LOCAL_SCENE_WIDTH;
        float right_end = (_worldmapmgr.GetWorldPos() + 1) * Constans.LOCAL_SCENE_WIDTH;
        float time = 0f;

        while (true)
        {
            yield return null;
            if (dir == Direction.Left)
            {
                time += Time.deltaTime;
                if(time >= 0.5f)
                {
                    _is_moving = false;
                    world_pos--;
                    CmrMoveEnd(world_pos, dir);
                    break;
                }
                transform.localPosition = new Vector3(Mathf.Lerp(start, left_end, time/0.5f), 0f, 0f);
            }
            else
            {
                time += Time.deltaTime;
                if (time >= 0.5f)
                {
                    _is_moving = false;
                    world_pos++;
                    CmrMoveEnd(world_pos, dir);
                    break;
                }
                transform.localPosition = new Vector3(Mathf.Lerp(start, right_end, time / 0.5f), 0f, 0f);
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
		Camera.main.aspect = Constans.TEN_SIXTEEN_RATIO;
	}

	private void CmrMoveEnd(int world_pos, Direction dir)
    {
        _worldmapmgr.UpdateWorld(world_pos, dir);
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
