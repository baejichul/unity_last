using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelDungeon
{
    public class WaterScroll : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] float _scrollSpeed = 1.0f;
        Renderer _renderer;

        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            float offset = Time.time * _scrollSpeed;
            _renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
    }
}
