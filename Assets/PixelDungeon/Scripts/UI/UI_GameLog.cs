using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PixelDungeon
{
    public class UI_GameLog : MonoBehaviour
    {
        public GameObject _logObjTemplate;
        //public Text _logTxt;
        public float _playTime = 1.5f;


        // Start is called before the first frame update
        void Start()
        {
            //_logTxt = transform.Find("verticalLayout/logTxt").GetComponent<Text>();
            _logObjTemplate = transform.Find("verticalLayout/logTxt").gameObject;
            _logObjTemplate.SetActive(false);

            //_logTxt.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Play(string message)
        {
            StartCoroutine(_Play(message));
        }

        IEnumerator _Play(string message)
        {
            GameObject newLogObj = Instantiate(_logObjTemplate);

            newLogObj.transform.parent = transform.Find("verticalLayout");
            newLogObj.SetActive(true);

            Text logTxt = newLogObj.GetComponent<Text>();

            logTxt.text = message;

            yield return new WaitForSeconds(_playTime);

            Destroy(newLogObj);
        }
    }
}
