using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class OscarIllusion : PrisonEventBase
{
    protected override void Awake()
    {
        base.Awake();
        targetPos = new Vector3(-430.2f, -209.5f, 12f);
        moveDuration = 1f;
    }
    protected override void EventOccur()
    {
        base.EventOccur();
        ShowIllusion().Forget();
    }

    private async UniTask ShowIllusion()
    {
        Vector3 startPos = Trans.position;
        SoundManager.Instance.SFXPlay(SfxType.WeirdSfx2);
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
        gameObject.SetActive(false);
    }
}
