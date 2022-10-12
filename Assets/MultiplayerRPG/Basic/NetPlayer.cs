using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityStandardAssets._2D;

public class NetPlayer : NetworkBehaviour
{
    public float _speed = 5.0f;

    Rigidbody2D _rigid;
    
    // 캐릭터 머리위 이름(3D텍스트)
    TextMesh _nameText;

    // 동기화 변수(SyncVar)
    [SyncVar (hook = nameof(OnNameChanged))]
    public string _playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color _playerColor;

    void OnNameChanged(string oldName, string newName)
    {
        Debug.Log($"OnNameChanged : oldName=>{oldName} : newName=>{newName} : _playerName=>{_playerName}");
        _nameText.text = _playerName;
    }

    void OnColorChanged(Color oldColor, Color newColor)
    {
        
    }

    void Awake()
    {
        _nameText = GetComponentInChildren<TextMesh>();
        // _nameText = transform.Find("Info/NameTxt").GetComponent<TextMesh>();
        _rigid    = GetComponent<Rigidbody2D>();
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

    // 로컬플레이어 생성시
    public override void OnStartLocalPlayer()
    {
        // base.OnStartLocalPlayer();
        NetCamera2DFollow camFollow = Camera.main.gameObject.AddComponent<NetCamera2DFollow>();
        camFollow.damping = 0.5f;
        camFollow.lookAheadFactor = 1.0f;
        camFollow.target = transform;

        // 플레이어가 생성되는 시점에 랜덤으로 이름을 생성
        // string name = "Player" + Random.Range(100, 999);

        // 캔버스 아래, 네임인풋에서 이름 가져오기
        InputField inputName = UIManager.I.transform.Find("InputName").GetComponent<InputField>();
        string name = inputName.text;

        // 서버(호스트)에 이름이 변경되었음을 알린다.(Command함수)
        CmdSetupPlayer(name);
    }

    [Command]
    public void CmdSetupPlayer(string name) // 호스트에서 실행되는 코드
    {
        Debug.Log($"CmdSetupPlayer : {name}");
        _playerName = name;
    }
}
