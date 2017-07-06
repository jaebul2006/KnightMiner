using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour 
{
    private Image _img;

	void Start () 
    {
        _img = GetComponent<Image>();
	}
	
	public void TouchStart()
    {
        StartCoroutine("FadeBlack");
    }

    private IEnumerator FadeBlack()
    {
        float cur_color = 1f;

        while(true)
        {
            yield return null;
            cur_color -= Time.deltaTime;
            _img.color = new Color(cur_color, cur_color, cur_color, 1f);
            if (cur_color <= 0f)
            {
                StartCoroutine("FadeOut");
                break;
            }
        }
    }

    private IEnumerator FadeOut()
    {
        float cur_alpha = 1f;

        while (true)
        {
            yield return null;
            cur_alpha -= Time.deltaTime;
            _img.color = new Color(0f, 0f, 0f, cur_alpha);
            if (cur_alpha <= 0f)
            {
                Destroy(gameObject);
                break;
            }
        }
    }

}
