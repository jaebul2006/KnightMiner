using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Km : MonoBehaviour {

    enum MoveState
    {
        NONE = 0,
        LEFT,
        RIGHT
    }
    MoveState _move_state = MoveState.NONE;

	void Start () {
		
	}
	
	void Update () {

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    _move_state = MoveState.LEFT;
        //    transform.localPosition -= new Vector3(1f, 0f, 0f);
        //    Debug.Log(transform.localPosition);
        //}
        //if(Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    _move_state = MoveState.RIGHT;
        //    transform.localPosition += new Vector3(1f, 0f, 0f);
        //    Debug.Log(transform.localPosition);
        //}

	}

}
