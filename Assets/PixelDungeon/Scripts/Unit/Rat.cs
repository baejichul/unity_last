using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelDungeon
{
    public class Rat : Unit
    {
        public AudioSource _hitSnd;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _hitSnd = GetComponent<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Attack()
        {
            // 어택 애니메이션 재생
            _anim.SetTrigger("attack");

            // 어택 효과음 재생
            _hitSnd.Play();

           // TODO: 공격 파티클 이펙트 재생

        }
    }
}
