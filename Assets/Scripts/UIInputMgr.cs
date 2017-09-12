using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class UIInputMgr : MonoBehaviour 
{
    public Button _btn_myinfo_open;
    public Button _btn_myinfo_close;
    public GameObject _myinfo_wnd;

    public Button _btn_recruit_miner_open;
    public Button _btn_recruit_miner_close;
    public GameObject _recruit_wnd;

    public KmMgr _km_mgr;

    void Start()
    {
        _btn_myinfo_open.onClick
            .AsObservable()
            .Subscribe(_ =>
            {
                // user_data = UserData.GetUserData();
                // DwarkData dw_data = _km_mgr.GetDwarfData();
                _myinfo_wnd.SetActive(true);
            });

        _btn_myinfo_close.onClick
            .AsObservable()
            .Subscribe(_ =>
            {
                _myinfo_wnd.SetActive(false);
            });

        _btn_recruit_miner_open.onClick
            .AsObservable()
            .Subscribe(_ =>
            {
                _recruit_wnd.SetActive(true);
            });

        _btn_recruit_miner_close.onClick
            .AsObservable()
            .Subscribe(_ =>
            {
                _recruit_wnd.SetActive(false);
            });

    }

}
