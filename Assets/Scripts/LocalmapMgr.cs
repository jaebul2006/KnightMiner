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

    public SpriteRenderer _village_spr;
    public SpriteRenderer _mine_spr;
    public SpriteRenderer _battleground_spr;

	public List<LocalmapData> _local_info = new List<LocalmapData>();

	void Start()
	{
		LocalmapData map1 = new LocalmapData ();
		map1.name = "LEFT";
		map1.go = GameObject.Find ("Left");
		map1.pos = new Vector3 (-10f, 0f, 0f);
		_local_info.Add (map1);
		LocalmapData map2 = new LocalmapData ();
		map2.name = "MIDDLE";
		map2.go = GameObject.Find ("Middle");
		map2.pos = new Vector3 (0f, 0f, 0f);
		_local_info.Add (map2);
		LocalmapData map3 = new LocalmapData ();
		map3.name = "RIGHT";
		map3.go = GameObject.Find ("Right");
		map3.pos = new Vector3 (10f, 0f, 0f);
		_local_info.Add (map3);
	}

	public void UpdateScene(int world_pos, CamMgr.Direction dir)
    {
        if(world_pos > 0)
        {
            // 전투맵 업데이트
            if(world_pos == 1)
            {
				for (int i = 0; i < _local_info.Count; i++) 
				{
					switch (_local_info [i].name) 
					{
					case "LEFT":
						_local_info [i].name = "RESERVE_RIGHT";
						_local_info [i].pos = new Vector3 ((world_pos + 1) * 10f, 0f, 0f);
						_local_info [i].go.transform.localPosition = _local_info [i].pos;
						_local_info [i].go.GetComponent<SpriteRenderer> ().sprite = _battleground_spr.sprite;
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
					switch (_local_info [i].name) 
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
				if (dir == CamMgr.Direction.Right) 
				{
					for (int i = 0; i < _local_info.Count; i++) 
					{
						switch (_local_info [i].name) 
						{
						case "LEFT":
							_local_info [i].name = "RESERVE_RIGHT";
							_local_info [i].pos = new Vector3 ((world_pos + 1) * 10f, 0f, 0f);
							_local_info [i].go.transform.localPosition = _local_info [i].pos;
							_local_info [i].go.GetComponent<SpriteRenderer> ().sprite = _battleground_spr.sprite;
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
						switch (_local_info [i].name) 
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
					
				}
            }
        }
        else if(world_pos < 0)
        {
            // 채광맵 업데이트
            if(world_pos == -1)
            {
                
            }
            else
            {
                            }
        }
        else 
        {
            // 마을맵 업데이트
            
        }
    }

}

