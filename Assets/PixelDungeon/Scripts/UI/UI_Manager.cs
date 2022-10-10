using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelDungeon
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager I;

        [SerializeField] UI_ScreenBlock _screenBlock;
        public UI_ScreenBlock ScreenBlock { get { return _screenBlock;  } }


        [SerializeField] UI_Topbar _topbar;
        public UI_Topbar Topbar { get { return _topbar; } }

        [SerializeField] UI_GameOver _gameover;
        public UI_GameOver Gameover { get { return _gameover;  } }

        [SerializeField] UI_GameLog _gameLog;
        public UI_GameLog GameLog { get { return _gameLog; } }


        private void Awake()
        {
            I = this;
        }

        private void Start()
        {
            Init();
        }

        public  void Init()
        {
            _screenBlock = transform.Find("UI_ScreenBlock").GetComponent<UI_ScreenBlock>();
            _topbar = transform.Find("UI_Topbar").GetComponent<UI_Topbar>();
            _gameover = transform.Find("UI_GameOver").GetComponent<UI_GameOver>();
            _gameLog = transform.Find("UI_GameLog").GetComponent<UI_GameLog>();

            UI_Base[] uiList = GetComponentsInChildren<UI_Base>(true);
            foreach(UI_Base ui in uiList)
            {
                ui.Init();

                ui.Refresh();
            }

        }
    }
}
