using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 주석
    public static UIManager I;

    UI_Chat _ui_chat;
    public UI_Chat ui_chat
    {
        get{ return _ui_chat;  }
    }

    private void Awake()
    {
        I = this;

        _ui_chat = transform.Find("UI_Chat").GetComponent<UI_Chat>();

        //_ui_chat = GetComponentInChildren<UI_Chat>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
