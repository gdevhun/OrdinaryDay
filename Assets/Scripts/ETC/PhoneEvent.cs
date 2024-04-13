using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.EventSystems;


public class PhoneEvent : MonoBehaviour
{
    public GameObject EventTrigger;
    private RectTransform _rectTransform; //스마트폰 이미지 rect트랜스폼.
    [SerializeField] private FirstPlayer FirstPlayer;
    public Image[] textImages; //3개 메세지 이미지

    private void OnEnable()
    {
        PhoneEventTrig().Forget();
    }

    private async UniTaskVoid PhoneEventTrig()
    {
        FirstPlayer.isFade = true;
        await _rectTransform.DOMoveY(-100, 2.5f).AsyncWaitForCompletion(); //-1200에서 -100으로 
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        for (int i = 0; i<textImages.Length; i++)
        {
            SoundManager.Instance.SFXPlay(SfxType.PhoneTextBell);
            textImages[i].gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        await _rectTransform.DOMoveY(-1200, 2f).AsyncWaitForCompletion();
        await UniTask.Yield();
        FirstPlayer.isFade = false;
        this.gameObject.SetActive(false);
        EventTrigger.SetActive(false);
    }
    
}
