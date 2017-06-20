using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMgr : MonoBehaviour 
{
    public Text _time_text;
    public Image _top_line;
    public Image _bottom_line;
    public Text _txt_seconds;

    public float MAX_TIME = 120f;
   
    public WorldmapMgr _world_mgr;

    private float _caution_time = 60f;
    private float _cur_remain_time = 0f;

	void Start () 
    {
        _cur_remain_time = MAX_TIME;
	}
	
    void FixedUpdate()
    {
        CheckWorldMap();
    }

    private void CheckWorldMap()
    {
        // 마을이 아니면 시간이 흘러간다
        // 시간이 1분 이상이면 흰색폰트, 미만이면 빨간색폰트로
        if(_world_mgr.GetWorldPos() != 0)
        {
            _cur_remain_time -= Time.deltaTime;
            if(_cur_remain_time < _caution_time)
            {
                _time_text.color = Color.red;
                _top_line.color = Color.red;
                _bottom_line.color = Color.red;
                _txt_seconds.color = Color.red;
            }
            else
            {
                _time_text.color = Color.white;
                _top_line.color = Color.white;
                _bottom_line.color = Color.white;
                _txt_seconds.color = Color.white;
            }
            SetTime(_cur_remain_time);
        }
    }

    private void SetTime(float cur_time)
    {
        if(cur_time <= 0)
        {
            //GoldLose();
            return;
        }
        _time_text.text = ((int)cur_time).ToString();
    }

}
