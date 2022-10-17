using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class NetMonster : NetworkBehaviour
{
    public int _maxHp = 1000;

    [SyncVar]
    public int _hp = 100;

    SpriteRenderer _sr;
    Vector3 _startPos;
    Color _origColor;

    void Start()
    {
        SetDefaultSortOrder();

        _origColor = _sr.color;
        _startPos = transform.position;

        if (isServer)
        {
            _hp = _maxHp;       // 체력 초기화(서버에서)
        }
    }

    void Update()
    {
        /*if (isServer)
        {
            Debug.Log($"UpdateServer : {Time.deltaTime}");
        }
            

        if (isClient)
        {
            Debug.Log($"UpdateClient : {Time.deltaTime}");
        }*/
            

        if (isServer)
        {
            // 몬스터의 경우 Network Transfrom 콘트롤러의 Client Authority 체크안함
            Vector3 pos = transform.position;
            pos.x = _startPos.x + Mathf.PingPong(Time.time, 2.0f);
            transform.position = pos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 서버에서 충돌처리
        if (isServer)
        {
            _hp -= 10;

            // Debug.Log($"Attack Object : {collision.gameObject.name}");
            NetPlayer netPlayer = collision.gameObject.GetComponent<NetPlayer>();
            if (netPlayer != null)
            {
                Debug.Log($"Attack player name : {netPlayer._playerName}");
            }
            Rpc_DoDamageEffect();

            if (_hp <= 0)
            {
                // 사망처리
                _hp = 0;
                Destroy(gameObject);
                // Rpc_Destroy();
            }
        }
    }

    [ClientRpc]
    void Rpc_Destroy()
    {
        Destroy(gameObject);
    }

    // Server => Client 브로드캐스팅
    [ClientRpc]
    void Rpc_DoDamageEffect()
    {
        StartCoroutine(_ChangeColor());
    }

    IEnumerator _ChangeColor()
    {
        _sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _sr.color = _origColor;

        yield return new WaitForSeconds(0.1f);

        _sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _sr.color = _origColor;

        yield return new WaitForSeconds(0.1f);

        _sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _sr.color = _origColor;
    }

    void SetDefaultSortOrder()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (_sr != null)
            _sr.sortingOrder = 1;
    }

}
