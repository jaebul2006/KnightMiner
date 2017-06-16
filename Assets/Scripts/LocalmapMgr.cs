using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalmapMgr : MonoBehaviour {

	public class LocalmapData
	{
		public string name;
		public GameObject go;
		public Vector3 pos;
	}

    public class RemainMineral
    {
        public GameObject go1;
        public GameObject go2;
        public GameObject go3;
    }

    public SpriteRenderer _village_spr;
    public SpriteRenderer _mine_spr;
    public SpriteRenderer _battleground_spr;

	public List<LocalmapData> _local_info = new List<LocalmapData>();

    Dictionary<int, RemainMineral> _remain_minerals = new Dictionary<int, RemainMineral>(); // 동적으로 제공되는 로컬맵때문에 캐다가 만 상황에서의 백업 광물정보.
    List<int> _visited_maps = new List<int>(); // 채굴이 완전히 끝난 로컬맵.

	void Start()
	{
		LocalmapData map1 = new LocalmapData ();
		map1.name = "LEFT";
		map1.go = GameObject.Find ("Left");
		map1.pos = new Vector3 (-Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
		_local_info.Add (map1);
		LocalmapData map2 = new LocalmapData ();
		map2.name = "MIDDLE";
		map2.go = GameObject.Find ("Middle");
		map2.pos = new Vector3 (0f, 0f, 0f);
		_local_info.Add (map2);
		LocalmapData map3 = new LocalmapData ();
		map3.name = "RIGHT";
		map3.go = GameObject.Find ("Right");
		map3.pos = new Vector3 (Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
		_local_info.Add (map3);
	}

	public void UpdateScene(int world_pos, CamMgr.Direction dir)
    {
        if(world_pos > 0)
        {
            UpdateMineral(world_pos);
            // 전투맵 업데이트
            if (dir == CamMgr.Direction.Right)
            {
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "LEFT":
                            _local_info[i].name = "RESERVE_RIGHT";
                            _local_info[i].pos = new Vector3((world_pos + 1) * Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
                            _local_info[i].go.transform.localPosition = _local_info[i].pos;
                            _local_info[i].go.GetComponent<SpriteRenderer>().sprite = _battleground_spr.sprite;
                            break;
                        case "MIDDLE":
                            _local_info[i].name = "RESERVE_LEFT";
                            break;
                        case "RIGHT":
                            _local_info[i].name = "RESERVE_MIDDLE";
                            break;
                    }
                }
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "RESERVE_LEFT":
                            _local_info[i].name = "LEFT";
                            break;
                        case "RESERVE_MIDDLE":
                            _local_info[i].name = "MIDDLE";
                            break;
                        case "RESERVE_RIGHT":
                            _local_info[i].name = "RIGHT";
                            break;
                    }
                }
            }
            else // camera left
            {
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "LEFT":
                            _local_info[i].name = "RESERVE_MIDDLE";
                            break;
                        case "MIDDLE":
                            _local_info[i].name = "RESERVE_RIGHT";
                            break;
                        case "RIGHT":
                            _local_info[i].name = "RESERVE_LEFT";
                            _local_info[i].pos = new Vector3((world_pos - 1) * Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
                            _local_info[i].go.transform.localPosition = _local_info[i].pos;
                            if (world_pos == 1)
                            {
                                _local_info[i].go.GetComponent<SpriteRenderer>().sprite = _village_spr.sprite;
                            }
                            else
                            {
                                _local_info[i].go.GetComponent<SpriteRenderer>().sprite = _battleground_spr.sprite;
                            }
                            break;
                    }
                }
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "RESERVE_LEFT":
                            _local_info[i].name = "LEFT";
                            break;
                        case "RESERVE_MIDDLE":
                            _local_info[i].name = "MIDDLE";
                            break;
                        case "RESERVE_RIGHT":
                            _local_info[i].name = "RIGHT";
                            break;
                    }
                }
            }
        }
        else if(world_pos < 0)
        {
            UpdateMineral(world_pos);
            // 채광맵 업데이트
            if (dir == CamMgr.Direction.Right)
            {
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "LEFT":
                            _local_info[i].name = "RESERVE_RIGHT";
                            _local_info[i].pos = new Vector3((world_pos + 1) * Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
                            _local_info[i].go.transform.localPosition = _local_info[i].pos;
                            if(world_pos == -1)
                            {
                                _local_info[i].go.GetComponent<SpriteRenderer>().sprite = _village_spr.sprite;
                            }
                            else
                            {
                                _local_info[i].go.GetComponent<SpriteRenderer>().sprite = _mine_spr.sprite;
                            }
                            break;
                        case "MIDDLE":
                            _local_info[i].name = "RESERVE_LEFT";
                            break;
                        case "RIGHT":
                            _local_info[i].name = "RESERVE_MIDDLE";
                            break;
                    }
                }
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "RESERVE_LEFT":
                            _local_info[i].name = "LEFT";
                            break;
                        case "RESERVE_MIDDLE":
                            _local_info[i].name = "MIDDLE";
                            break;
                        case "RESERVE_RIGHT":
                            _local_info[i].name = "RIGHT";
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "LEFT":
                            _local_info[i].name = "RESERVE_MIDDLE";
                            break;
                        case "MIDDLE":
                            _local_info[i].name = "RESERVE_RIGHT";
                            break;
                        case "RIGHT":
                            _local_info[i].name = "RESERVE_LEFT";
                            _local_info[i].pos = new Vector3((world_pos - 1) * Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
                            _local_info[i].go.transform.localPosition = _local_info[i].pos;
                            _local_info[i].go.GetComponent<SpriteRenderer>().sprite = _mine_spr.sprite;
                            break;
                    }
                }
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "RESERVE_LEFT":
                            _local_info[i].name = "LEFT";
                            break;
                        case "RESERVE_MIDDLE":
                            _local_info[i].name = "MIDDLE";
                            break;
                        case "RESERVE_RIGHT":
                            _local_info[i].name = "RIGHT";
                            break;
                    }
                }
            }
        }
        else 
        {
            // 마을맵 업데이트
            if (dir == CamMgr.Direction.Right)
            {
                // 광산에서 마을로 온 상황
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "LEFT":
                            _local_info[i].name = "RESERVE_RIGHT";
                            _local_info[i].pos = new Vector3((world_pos + 1) * Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
                            _local_info[i].go.transform.localPosition = _local_info[i].pos;
                            _local_info[i].go.GetComponent<SpriteRenderer>().sprite = _battleground_spr.sprite;
                            break;
                        case "MIDDLE":
                            _local_info[i].name = "RESERVE_LEFT";
                            break;
                        case "RIGHT":
                            _local_info[i].name = "RESERVE_MIDDLE";
                            break;
                    }
                }
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "RESERVE_LEFT":
                            _local_info[i].name = "LEFT";
                            break;
                        case "RESERVE_MIDDLE":
                            _local_info[i].name = "MIDDLE";
                            break;
                        case "RESERVE_RIGHT":
                            _local_info[i].name = "RIGHT";
                            break;
                    }
                }
            }
            else
            {
                // 배틀에서 마을로 온 상황
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "LEFT":
                            _local_info[i].name = "RESERVE_MIDDLE";
                            break;
                        case "MIDDLE":
                            _local_info[i].name = "RESERVE_RIGHT";
                            break;
                        case "RIGHT":
                            _local_info[i].name = "RESERVE_LEFT";
                            _local_info[i].pos = new Vector3((world_pos - 1) * Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
                            _local_info[i].go.transform.localPosition = _local_info[i].pos;
                            _local_info[i].go.GetComponent<SpriteRenderer>().sprite = _mine_spr.sprite;
                            break;
                    }
                }
                for (int i = 0; i < _local_info.Count; i++)
                {
                    switch (_local_info[i].name)
                    {
                        case "RESERVE_LEFT":
                            _local_info[i].name = "LEFT";
                            break;
                        case "RESERVE_MIDDLE":
                            _local_info[i].name = "MIDDLE";
                            break;
                        case "RESERVE_RIGHT":
                            _local_info[i].name = "RIGHT";
                            break;
                    }
                }
            }
        }
    }

    private void UpdateMineral(int world_pos)
    {
        if (_visited_maps.Contains(world_pos) == false)
        {
            // 월드 포지션에 따라 내구도와 보상이 차별되는 광물을 3개씩 생성
            GameObject mineral_1 = Instantiate(Resources.Load("Prefabs/Mineral")) as GameObject;
            mineral_1.transform.localPosition = new Vector3(world_pos * Constans.LOCAL_SCENE_WIDTH + -2.5f, 0f, 0f);
            GameObject mineral_2 = Instantiate(Resources.Load("Prefabs/Mineral")) as GameObject;
            mineral_2.transform.localPosition = new Vector3(world_pos * Constans.LOCAL_SCENE_WIDTH, 0f, 0f);
            GameObject mineral_3 = Instantiate(Resources.Load("Prefabs/Mineral")) as GameObject;
            mineral_3.transform.localPosition = new Vector3(world_pos * Constans.LOCAL_SCENE_WIDTH + 2.5f, 0f, 0f);
            
            RemainMineral remain_mineral = new RemainMineral();
            remain_mineral.go1 = mineral_1;
            remain_mineral.go2 = mineral_2;
            remain_mineral.go3 = mineral_3;
            _remain_minerals.Add(world_pos, remain_mineral);
            _visited_maps.Add(world_pos);
        }
        else
        {
            if(_remain_minerals.ContainsKey(world_pos))
            {
                foreach(KeyValuePair<int, RemainMineral>kv in _remain_minerals)
                {
                    Debug.Log("1:" + kv.Value.go1.GetComponent<Mineral>()._hp);
                    Debug.Log("2:" + kv.Value.go2.GetComponent<Mineral>()._hp);
                    Debug.Log("3:" + kv.Value.go3.GetComponent<Mineral>()._hp);
                }
            }
        }
    }

}

