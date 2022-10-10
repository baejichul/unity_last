using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelDungeon
{
    public class GameWorld : MonoBehaviour
    {
        public static GameWorld I; // 싱글턴 인스턴스


        void Awake()
        {
            I = this;
        }

    }
}
