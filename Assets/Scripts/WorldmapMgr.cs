using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldmapMgr : MonoBehaviour {

    // 월드 맵의 좌표는 마을 0을 기준으로 - <<< 마을(0) >>> + 인덱스가 되고
    // 음수와 양수로 이루어지는 3개의 값으로 로컬맵(현재보이는 씬, 좌우씬 3개)을 구성한다.

    int _cur_map_pos = 0;
    public LocalmapMgr _localmgr;
    public CamMgr _cmrmgr;
    public KmMgr _kmmgr;

    public void UpdateWorld(int pos)
    {
        Debug.Log(pos);
        _cur_map_pos = pos;
        _localmgr.UpdateScene(pos);
        _cmrmgr.UpdateScene();
        _kmmgr.UpdateScene(pos);
    }

    public int GetWorldPos()
    {
        return _cur_map_pos;
    }

}
