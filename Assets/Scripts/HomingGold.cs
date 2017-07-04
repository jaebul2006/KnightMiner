using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingGold : MonoBehaviour
{

    public float _speed = 10f;
    public float _rotating_speed = 200f;
    public GameObject _target;

    Rigidbody2D _rigid;
    GameAssetMgr _game_asset_mgr;

    void Start()
    {
        _target = GameObject.Find("HomingGoldTarget");
        gameObject.transform.parent = _target.transform.parent;
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _game_asset_mgr = GameObject.Find("GameAssetMgr").GetComponent<GameAssetMgr>();
    }

    void FixedUpdate()
    {
        Vector2 point2target = (Vector2)transform.position - (Vector2)_target.transform.position;
        point2target.Normalize();
        float value = Vector3.Cross(point2target, transform.right).z;

        _rigid.angularVelocity = _rotating_speed * value;
        _rigid.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "HomingGoldTarget")
        {
            _game_asset_mgr.HandoverGold(100/*테스트*/);
            Destroy(gameObject);
        }
    }


}
