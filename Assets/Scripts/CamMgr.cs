using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 화면은 항상 중앙, 좌우가 준비된 화면이어야 한다.
public class CamMgr : MonoBehaviour 
{
	float TEN_SIXTEEN_RATIO = 0.625f;

	void Start () 
    {
		ScrResolution ();	
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine("MoveCamera");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine("MoveCamera");
        }	
	}

    IEnumerator MoveCamera()
    {
        while (true)
        {
            yield return null;
            if (transform.localPosition.x > -1000f)
            {
                transform.localPosition -= new Vector3(2000f * Time.deltaTime, 0f, 0f);
            }
            else
            {
                transform.localPosition = new Vector3(-1000f, 0f, 0f);
                break;
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

}
