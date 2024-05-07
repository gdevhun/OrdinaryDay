using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillerViewUpdate : MonoBehaviour
{
    private Image _image;
    public float speed = 1.0f; // 애니메이션 속도 조절을 위한 변수
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        // 시간에 따라 알파값을 변경
        float alpha = Mathf.Lerp(40f, 140f, Mathf.PingPong(Time.time * speed, 1f));
        
        // 이미지의 색상을 업데이트
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha / 255f);
    }
}
