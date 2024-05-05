using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MovingObject : PrisonEventBase
{
    [SerializeField] private Vector3 thisTargetPos;
    [SerializeField] private float thisMoveDuration;
    protected override void Awake()
    {
        base.Awake();
        targetPos = thisTargetPos;
        moveDuration = thisMoveDuration;
        //targetPos = new Vector3(-426f, -207.8f, -10.5f);
        //moveDuration = 0.5f;
    }
    
    protected override void EventOccur()
    {
        base.EventOccur();
        MoveObject().Forget();
    }
    private async UniTask MoveObject()
    {
        Vector3 startPos = Trans.position;
        if (gameObject.CompareTag("Metal"))
        {
            SoundManager.Instance.SFXPlay(SfxType.BodyBagMove);
        }
        else
        {
            SoundManager.Instance.SFXPlay(SfxType.IronScratch);
        }
        
        while (elapsedTime < moveDuration)
        {
            // 현재 시간에 대한 비율을 계산하여 시작 위치에서 목표 위치까지 이동
            Trans.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            
            // 프레임 간 경과 시간 추가
            elapsedTime += Time.deltaTime;
            
            // 다음 프레임까지 대기
            await UniTask.Yield();
        }
        
        // 이동이 완료된 후 최종 위치 설정
        Trans.position = targetPos;
        SoundManager.Instance.SFXPlay(SfxType.WeirdSfx);
        ActiveRigid();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5));
        UnActiveRigid();
    }
}
