using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelDungeon
{
    public class Player : Unit
    {


        public static Player I;

        [SerializeField] int _floor = 1;
        public int Floor 
        { 
            get{  return _floor; }            
        }

        [SerializeField]float _speed = 1.0f;


        Rigidbody2D _rigid;

        bool _doingWARP = false;

        void Awake()
        {
            I = this;    
        }

        protected override void Start()
        {
            base.Start();
            
            _rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        //void Update()
        void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if( _anim != null )
            {
                if (h == 0 && v == 0)
                {
                    //_anim.SetBool("run", false);
                }
                else
                {
                    //_anim.SetBool("run", true);
                }
            }



            Move(h, v);
            Flip(h);
        }


        /*void Update()
        {
            if( Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveByGrid(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveByGrid(1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveByGrid(0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveByGrid(0, -1);
            }
            else if( Input.GetKeyDown(KeyCode.Home))
            {
                MoveByGrid(-1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.PageUp))
            {
                MoveByGrid(1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.End))
            {
                MoveByGrid(-1, -1);
            }
            else if (Input.GetKeyDown(KeyCode.PageDown))
            {
                MoveByGrid(1, -1);
            }
        }

        void MoveByGrid(int h, int v)
        {
            StartCoroutine(_Move(h, v));
        }

        bool _moving = false;
        IEnumerator _Move(int h, int v)
        {
            if( _moving == false )
            {
                _moving = true;
                Debug.Log("이동 시작");

                Collider2D col = GetComponent<Collider2D>();
                col.enabled = false;

                float elapsed = 0.0f;
                float duration = 0.1f;

                float startPosX = transform.position.x;
                float destPosX = startPosX + ((h > 0) ? 1.0f : -1.0f);
                if (h == 0) destPosX = startPosX;

                float startPosY = transform.position.y;
                float destPosY = startPosY + ((v > 0) ? 1.0f : -1.0f);
                if (v == 0) destPosY = startPosY;

                while (elapsed < duration)
                {
                    //elapsed += Time.fixedDeltaTime;
                    elapsed += Time.deltaTime;

                    Vector3 pos = transform.position;

                    pos.x = Mathf.Lerp(startPosX, destPosX, elapsed / duration);
                    pos.y = Mathf.Lerp(startPosY, destPosY, elapsed / duration);

                    transform.position = pos;

                    yield return null;
                }

                col.enabled = true;
                _moving = false;
                Debug.Log("이동 완료");
            }
        }
        */
        void Move(float h, float v)
        {
            Vector2 dir = new Vector2(h, v); // 방향 벡터

            // 이동거리 = 방향벡터 * 스피드

            //이동
            //transform.Translate(dir * _speed * Time.deltaTime);
            if (_doingWARP == false)
                _rigid.velocity = dir * _speed * Time.fixedDeltaTime;
            else
                _rigid.velocity = Vector2.zero;
        }

        void Flip(float h)
        {
            if (h < 0)
            {
                //좌
                _renderer.flipX = true;

            }
            else if (h > 0)
            {
                //우
                _renderer.flipX = false;
            }

        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log("트리거 이벤트! : " + collision.gameObject.name);

            Stair stair = collision.gameObject.GetComponent<Stair>();
            if(stair != null) // 트리거 오브젝트가 계단인 경우
            {
                // 방금 막 이동된 경우는 워프 안함 return
                if (_doingWARP == true) return;


                // 플레이어를 다음 목적지로 워프 시켜주기
                if (stair._destObj != null)
                {
                    transform.position = stair._destObj.transform.position;
                    StartWARP();
                }

                if( stair._direction == StairDirection.DOWN)
                {
                    _floor++;
                    UI_Manager.I.Topbar.Refresh();
                }
                else if(stair._direction == StairDirection.UP)
                {
                    if (_floor == 1)
                    {
                        PlatformDialog.Show(
                            "안내",
                            "현재는 던전을 벗어날 수 없습니다",
                            PlatformDialog.Type.SubmitOnly,
                            () => {
                                Debug.Log("OK");
                            },
                            null
                        );
                    }
                    else
                    {
                        _floor--;
                        UI_Manager.I.Topbar.Refresh();
                    }
                }

            }

        }
    
        void StartWARP()
        {
            UI_Manager.I.ScreenBlock.Play();
            _doingWARP = true;
            Invoke("StopWARP", 2.0f);
        }

        void StopWARP()
        {
            _doingWARP = false;
            UI_Manager.I.ScreenBlock.Stop();
        }

        
    
    }
}
