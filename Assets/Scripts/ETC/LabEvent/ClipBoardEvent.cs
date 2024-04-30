using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ClipBoardEvent : MonoBehaviour
{
    private bool _isEventOccured; //일회성 대화이벤트를 위한 bool변수
    public GameObject nextTriggerObj; //활성화 시킬 오브젝트
    private void OnEnable()
    {
        if (!_isEventOccured)
        {
            ClipBoardCheckEvent();
        }
    }

    private async UniTask ClipBoardCheckEvent()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1.5));
        await UniTask.Yield();
        nextTriggerObj.gameObject.SetActive(true);
        _isEventOccured = true;
    }
}
