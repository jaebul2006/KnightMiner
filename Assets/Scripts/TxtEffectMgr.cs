using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TxtEffectMgr : MonoBehaviour 
{

    public void YourchildFinish(Animator child_anim)
    {
        Destroy(child_anim.gameObject);
    }

    public void ShowTxtAni(Vector3 target_obj_worldpos, string txt_content)
    {
        RectTransform canvas_rect = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<RectTransform>();
        Vector2 view_pos = Camera.main.WorldToViewportPoint(target_obj_worldpos);
        Vector2 screen_pos = new Vector2(((view_pos.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f)), ((view_pos.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f)));

        GameObject go = Instantiate(Resources.Load("Prefabs/DamageTxtAni")) as GameObject;
        go.transform.SetParent(transform);
        go.GetComponent<Animator>().SetTrigger("Do");
        go.GetComponent<RectTransform>().anchoredPosition = screen_pos;
        go.GetComponent<Text>().text = txt_content;
    }

}
