using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PixelDungeon
{
    public class UI_ScreenBlock : UI_Base
    {
        [SerializeField] float _duration1 = 0.2f;
        [SerializeField] float _duration2 = 0.5f;
        [SerializeField] float _duration3 = 0.2f;

        Image _backgroundImg;
        Text _infoTxt;


        public override void Init()
        {
            _backgroundImg = transform.Find("backgroundImg").GetComponent<Image>();
            _infoTxt = transform.Find("infoTxt").GetComponent<Text>();
        }

        public void Play()
        {
            gameObject.SetActive(true);

            StartCoroutine(_Show());
        }

        IEnumerator _Show()
        {
            ///Debug.Log("연출 시작");

            _infoTxt.gameObject.SetActive(false);

            float elapsed = 0.0f; // 경과시간

            // 알파값 0 -> 1 (페이드인)
            while (elapsed < _duration1)
            {
                //Debug.Log("페이드인...");

                Color color = _backgroundImg.color;
                color.a = Mathf.Lerp(0.0f, 1.0f, elapsed / _duration1);
                _backgroundImg.color = color;

                elapsed += Time.deltaTime;

                yield return null;
            }
            elapsed = 0.0f;


            _infoTxt.gameObject.SetActive(true);
            //Debug.Log("텍스트 보이기...");

            yield return new WaitForSeconds(_duration2);

            _infoTxt.gameObject.SetActive(false);
            //Debug.Log("텍스트 숨기기...");


            // 알파값 1 -> 0 (페이드 아웃)
            while (elapsed < _duration3)
            {
                //Debug.Log("페이드 아웃...");
                Color color = _backgroundImg.color;
                color.a = Mathf.Lerp(1.0f, 0.0f, elapsed / _duration3);
                _backgroundImg.color = color;

                elapsed += Time.deltaTime;

                yield return null;
            }

            //Debug.Log("연출 종료");
        }

    
        public void Stop()
        {
            gameObject.SetActive(false);
        }
    
    }
}
