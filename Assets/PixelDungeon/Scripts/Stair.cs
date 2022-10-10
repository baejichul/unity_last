using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelDungeon
{
    public enum StairDirection
    {
        UP,
        DOWN
    }

    public class Stair : MonoBehaviour
    {
        public StairDirection _direction;

        public GameObject _destObj; // destination 목적지

        void Start()
        {
            // 
            //int nextFloor = Player.I.Floor;

            string floor = transform.parent.parent.gameObject.name.Replace("Floor_", "");

            int nextFloor = Convert.ToInt32(floor); 

            if (_direction == StairDirection.DOWN)
            {
                nextFloor++;
            }
            else if( _direction == StairDirection.UP )
            {
                nextFloor--;
            }

            if (nextFloor == 0)
            {

            }
            else
            {

                string dir = (_direction == StairDirection.DOWN) ? "up" : "down";


                Transform floorTrans = GameWorld.I.transform.Find("Floor_" + nextFloor);
                if( floorTrans != null )
                {
                    Transform objTrans = floorTrans.Find("objects");
                    if( objTrans != null )
                    {
                        _destObj = objTrans.Find("stair_" + dir).gameObject;
                    }
                }
            }

        }


    }
}
