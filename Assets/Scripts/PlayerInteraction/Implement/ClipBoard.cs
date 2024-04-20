using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class ClipBoard : InteractionBase
{
    public GameObject clipBoardPanel; // 클립보드
    private RectTransform _rectTransform; // 클립보드패널 렉트

    // 초기화
    protected override void Awake()
    {
        base.Awake();
        _rectTransform = clipBoardPanel.GetComponent<RectTransform>();
    }

    // 플레이어가 근처에 없음
    // 클립보드는 Exit 할때
    // 확인한 상태일때만 내려놓음
    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            if(isInter) PutBoard().Forget(); // 클립보드 내려놓기
            interactionText.text = "";
        }
    }

    // 클립보드 확인하기
    protected override void On()
    {
        clipBoardPanel.gameObject.SetActive(true);
        isInter = true;
        interactionText.text = "";
        _rectTransform.DOMoveY(500, 2f).SetEase(Ease.OutCubic);
        SoundManager.Instance.SFXPlay(SfxType.CheckClipBoard);
    }

    // 클립보드 내려놓기
    private async UniTaskVoid PutBoard()
    {
        await _rectTransform.DOMoveY(-1200, 2f).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        clipBoardPanel.gameObject.SetActive(false);
        isInter = false;
        await UniTask.Yield();
    }
}
