using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ScatteringObject : PrisonEventBase
{
    [SerializeField] private Vector3 thisTargetPos;
    [SerializeField] private float thisMoveDuration;
    public List<GameObject> items;
    protected override void Awake()
    {
        base.Awake();
        targetPos = thisTargetPos;
        moveDuration = thisMoveDuration;
    }
    
    protected override void EventOccur()
    {
        base.EventOccur();
        MoveObject().Forget();
    }
    private async UniTask MoveObject()
    {
        Vector3 startPos = Trans.position;
        SoundManager.Instance.SFXPlay(SfxType.BodyBagMove);
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
        SoundManager.Instance.SFXPlay(SfxType.ScatteringItem1);
        SoundManager.Instance.SFXPlay(SfxType.ScatteringItem2);
        SoundManager.Instance.SFXPlay(SfxType.ScatteringItem3);
        SoundManager.Instance.SFXPlay(SfxType.ScatteringItem4);
        ActiveRigid();
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        SoundManager.Instance.SFXPlay(SfxType.WeirdSfx2);
        UnActiveRigid();
    } 
    protected override void ActiveRigid()
    {
        foreach (var item in items)
        {
            Rigidbody rigidbody = item.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.isKinematic = false;
                rigidbody.constraints = RigidbodyConstraints.None;
            }
        }
    }

    protected override void UnActiveRigid()
    {
        foreach (var item in items)
        {
            Rigidbody rigidbody = item.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.isKinematic = true;
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
