using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KmMgr : MonoBehaviour {

    public Km[] _kms;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void UpdateScene(int worldpos)
    {
        _kms[0].SetLimitLeftRightPos(worldpos);
    }

}
