using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityStandardAssets._2D;

public class NetPlayer : NetworkBehaviour
{
    public float _speed = 5.0f;

    Rigidbody2D _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // 플레이어 이동구현
    void FixedUpdate()
    {
        if (isLocalPlayer == true)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector2 dir = new Vector2(h, v);
            _rigid.velocity = dir * Time.fixedDeltaTime * _speed * 100.0f;
        }
    }

    public override void OnStartLocalPlayer()
    {
        // base.OnStartLocalPlayer();
        NetCamera2DFollow camFollow = Camera.main.gameObject.AddComponent<NetCamera2DFollow>();
        camFollow.damping = 0.5f;
        camFollow.lookAheadFactor = 1.0f;
        camFollow.target = transform;
    }
}
