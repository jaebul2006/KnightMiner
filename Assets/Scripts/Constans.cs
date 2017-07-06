using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constans : MonoBehaviour 
{
    public static float TEN_SIXTEEN_RATIO = 0.625f;
    public static float LOCAL_SCENE_WIDTH = 10f;
	public static float LOCAL_HEIGHT = 1.66f;
    public static float HOME_DISTANCE = 2.7f;

    public enum eMoveDirection
    {
        NONE = 0,
        RIGHT,
        LEFT
    }

    public static float MAX_GOLD_BAR_UI_TIME = 3f;
    public static int MAX_KM = 5;
}


// 요구 기능구현 목록
// 2. 드워프 한명으로 시작하는데 보석 구매로 늘리는거 구현
// 3. 내 정보 보기 구현