using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BodyBag : PrisonEventBase
{
    protected override void Awake()
    {
        base.Awake();
        targetPos = new Vector3(-426f, -207.8f, -10.5f);
        moveDuration = 2.5f;
        //Z -10.493
    }
    
    protected override void EventOccur()
    {
        base.EventOccur();
        MoveObject().Forget();
    }
    private async UniTask MoveObject()
    {
        Vector3 startPos = Trans.position;
        while (elapsedTime < moveDuration)
        {
            // 현재 시간에 대한 비율을 계산하여 시작 위치에서 목표 위치까지 이동
            Trans.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            
            // 프레임 간 경과 시간 추가
            elapsedTime += Time.deltaTime;
            
            // 다음 프레임까지 대기
            await UniTask.Yield();
            Debug.Log(gameObject.transform.position);
        }
        
        // 이동이 완료된 후 최종 위치 설정
        Trans.position = targetPos;
        

    }

}
