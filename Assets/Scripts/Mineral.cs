﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    private bool _punching = false;
    List<int> _delay_punch = new List<int>();
    Vector3 _origin_pos;
    GameObject _target_obj;
    Vector3 _dir;

    public int _hp = 100;

    private TxtEffectMgr _txteffect_mgr;

    void Start()
    {
        _target_obj = gameObject;
        _origin_pos = _target_obj.transform.position;
        _dir = new Vector3(-0.03f, 0f, 0f);
        _txteffect_mgr = GameObject.Find("TxtEffectMgr").GetComponent<TxtEffectMgr>();
    }

    public void Damage(int mining_cap)
    {
        _delay_punch.Add(0);
        CheckPunch();
        HpCalculate(mining_cap);
        ShowDamageFont(mining_cap);
    }

    private void HpCalculate(int mining_cap)
    {
        _hp -= mining_cap;
        if (_hp <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void CheckPunch()
    {
        if (_delay_punch.Count > 0 && _punching == false)
        {
            _delay_punch.RemoveAt(0);
            StartCoroutine("Punch");
        }
    }

    private IEnumerator Punch()
    {
        _punching = true;
        float timer = 0f;
        float time = 0.05f;
        Vector3 orgin_pos = _origin_pos;
        //_dir.Normalize(); // -1 이므로 노말라이즈 필요x

        while (timer <= time)
        {
            _target_obj.transform.position = _origin_pos + (Mathf.Sin(timer / time * Mathf.PI) + 1f) * _dir;
            yield return null;
            timer += Time.deltaTime;
        }
        _target_obj.transform.position = _origin_pos;
        _punching = false;
        CheckPunch();
    }

    private void ShowDamageFont(int mining_cap)
    {
        _txteffect_mgr.ShowTxtAni(transform.position, mining_cap.ToString());
    }

}
