using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class PhoneEvent : MonoBehaviour
{
    public GameObject triggerExceptionWall;
    public GameObject eventTrigger;
    private RectTransform _rectTransform; //스마트폰 이미지 rect트랜스폼.
    [SerializeField] private FirstPlayer firstPlayer;
    public Image[] textImages; //3개 메세지 이미지

    private void Awake()
    {
        triggerExceptionWall.SetActive(true);
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        PhoneEventTrig().Forget();
    }

    private async UniTaskVoid PhoneEventTrig()
    {
        firstPlayer.isFade = true;
        await _rectTransform.DOMoveY(500, 2.5f).SetEase(Ease.OutCubic).AsyncWaitForCompletion(); 
        //-1200에서 -100으로 오버레이가 정규좌표라 500으로 설정.
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        for (int i = 0; i<textImages.Length; i++)
        {
            SoundManager.Instance.SFXPlay(SfxType.PhoneSecondBell);
            textImages[i].gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        await _rectTransform.DOMoveY(-1200, 2f).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        //다시 -1200으로
        await UniTask.Yield();
        triggerExceptionWall.SetActive(false);
        firstPlayer.isFade = false;
        this.gameObject.SetActive(false);
        eventTrigger.SetActive(false);
    }
    
}
