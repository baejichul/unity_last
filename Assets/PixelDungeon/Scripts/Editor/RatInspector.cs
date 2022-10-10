using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PixelDungeon
{

    [CustomEditor(typeof(Rat))]
    public class RatInpector : Editor
    {
        Rat _rat;

        public override void OnInspectorGUI()
        {
            if (_rat == null)
                _rat = target as Rat;

            if (GUILayout.Button("공격 테스트"))
            {
                _rat.Attack();


                // 치트
                // 플레이어의 체력을 깍아주기(테스트)
                int damage = 10;

                Player.I.DoDamage(damage);

                string formatStr = string.Format("{0}가 {1}에게 {2}데미지를 입혔습니다",
                    _rat.gameObject.name, 
                    Player.I.gameObject.name,
                    damage);

                UI_Manager.I.GameLog.Play(formatStr);

            }

            base.OnInspectorGUI(); 
        }
    }
}
