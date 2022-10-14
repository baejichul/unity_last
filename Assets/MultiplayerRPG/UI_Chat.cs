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

        // 주석
        _player.CmdSendChatMessage(chatMessage);


    }


}
