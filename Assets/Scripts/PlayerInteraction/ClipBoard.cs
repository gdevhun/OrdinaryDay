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

    protected override void Awake()
    {
        base.Awake();

        // 클립보드패널 렉트
        _rectTransform = clipBoardPanel.GetComponent<RectTransform>();
    }

    // 플레이어가 근처에 있음
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = "E키로 근무일지를 확인할 수 있다.";
        }
    }

    // 플레이어가 근처에 없음
    // 클립보드는 Exit 할때
    // 확인한 상태일때만 내려놓음
    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;

            // 클립보드 내려놓기
            if(isInter)
            {
                PutBoard().Forget();
            }

            interactionText.text = "";
        }
    }

    // 상호작용
    protected override void Interaction()
    {
        if (!isInter) CheckBoard();// 확인하지않은상태면 확인함
    }

    // 클립보드 확인하기
    private void CheckBoard()
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
