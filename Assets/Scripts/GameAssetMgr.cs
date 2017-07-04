using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetMgr : MonoBehaviour 
{

    public KmMgr _km_mgr;
    public Transform _gold_dest_target;
    NumberRollingMgr _nrmgr;

	void Start () 
    {
		_nrmgr = GameObject.Find("NumberRollingMgr").GetComponent<NumberRollingMgr>();
	}
	
	void Update () 
    {
		
	}

    public void GoldSave()
    {
        Transform house = GameObject.Find("House").transform;
        Transform leader_km = _km_mgr.GetLeader();
        Transform[] dist_objs = new Transform[2];
        dist_objs[0] = house;
        dist_objs[1] = leader_km;
        StartCoroutine("WaitForHouse", dist_objs);
    }

    private IEnumerator WaitForHouse(Transform[] dist_objs)
    {
        float distance = Vector2.Distance(dist_objs[0].position, dist_objs[1].position);

        while (true)
        {
            yield return null;
            distance = Vector2.Distance(dist_objs[0].position, dist_objs[1].position);
            if (distance < Constans.HOME_DISTANCE)
            {
                _km_mgr.StopKM();
                GoldSaveAni();
                _km_mgr.IdleAniSet();
                break;
            }
        }
    }

    private void GoldSaveAni() // 골드가 흩어졌다 빨려들어가는 애니메이션.
    {
        Vector3 target_pos = Vector3.zero;

        // 골드스프라이트의 프리펩 10 개를 만든다
        for (int i = 0; i < 10; i++)
        {
            StartCoroutine("LazyHomingGold", i * 0.1f);
        }
    }

    private IEnumerator LazyHomingGold(float delay_time)
    {
        float cur_time = 0f;

        while(true)
        {
            yield return null;
            cur_time += Time.deltaTime;
            if(cur_time > delay_time)
            {
                GameObject gold_go = Instantiate(Resources.Load("Prefabs/HomingGold")) as GameObject;
                // 골드스프라이트의 현재위치세팅
                float pos_x = _km_mgr.GetAverPosX(); // 현재 km들의 평균 위치를 조절해서 골드스프라이트의 위치를 세팅한다
                gold_go.transform.position = new Vector3(pos_x, 1f, 0f);
                break;
            }
        }
    }

    // 골드하나씩 UI 창에 부딪힐때마다 골드숫자 카운팅매니저에 넘기는 함수.
    public void HandoverGold(int gold_amount)
    {
        _nrmgr.AddGold(gold_amount);
    }

}

// 구현할 내용: 오른편에 몬스터 등장(근데 몬스터 구성은 현재단계로 광물과 다를바없음, 한종류만 테스트, 광부들의 체력이 존재?)