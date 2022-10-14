using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class UI_Chat : NetworkBehaviour
{

    Text _chatText;
    InputField _chatInput;
    public NetPlayer _player = null;

    void Awake()
    {
        _chatText = transform.Find("Scroll View/Viewport/Content/ChatText").GetComponent<Text>();

        _chatInput = transform.parent.Find("InputChat").GetComponent<InputField>();
    }


    [SyncVar (hook = nameof(OnStatusTextChanged))]
    public string _statusText;


    void OnStatusTextChanged(string oldText, string newText)
    {
        if(_chatText == null )
        {
            _chatText = transform.Find("Scroll View/Viewport/Content/ChatText").GetComponent<Text>();
        }

        if( _chatText != null )
        {

            _chatText.text = _statusText;
        }

    }


    // InputField 주석
    public void OnChatSubmit()
    {
        string chatMessage = _chatInput.text;

        Debug.Log("OnChatSubmit !! " + chatMessage);

        // 커맨드함수를 호출하여서 서버에서 동기화 변수인 _statusText수정하게 하기
        _player.CmdSendChatMessage(chatMessage);


    }


}
