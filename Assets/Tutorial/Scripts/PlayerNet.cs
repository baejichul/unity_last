using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNet : NetworkBehaviour
{
    [SerializeField]float _speed = 5.0f;    
    Rigidbody2D _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public override void OnStartLocalPlayer()
    {
        //Camera.main.transform.SetParent(transform);

        CameraFollowEx camFollow = Camera.main.GetComponent<CameraFollowEx>();
        camFollow.target = transform;

        float camZ = Camera.main.transform.position.z;
        Camera.main.transform.localPosition = new Vector3(0, 0, camZ);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) { return; }

        /*float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);*/
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
    }

    void Move(float h, float v)
    {
        Vector2 dir = new Vector2(h, v); // 방향 벡터

        _rigid.velocity = dir * 100.0f * _speed * Time.fixedDeltaTime;
    
    }
}
