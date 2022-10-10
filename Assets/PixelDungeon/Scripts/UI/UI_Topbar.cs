using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PixelDungeon
{
    public class UI_Topbar : UI_Base
    {
        public Text _floorTxt;

        // Start is called before the first frame update
        public override void Init()
        {
            _floorTxt = transform.Find("flootTxt").GetComponent<Text>();
        }

        /*public void RefreshFloor(int floor)
        {
            _floorTxt.text = floor.ToString();
        }*/

        public override void Refresh()
        {
            // 현재 층 리프레시
            int floor = Player.I.Floor;
            _floorTxt.text = floor.ToString();

            // TODO: 소지금, 체력, 열쇠갯수 등등 리프레시
        }
    }
}
