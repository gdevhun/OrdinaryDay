using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : InteractionBase
{
    [SerializeField] private GameObject waterSound; // 물소리
    [SerializeField] private bool isWater; // 최종적으로 체크해야하는 오브젝트인지 체크 1, 4, 7, 9, 12
    private bool isFirst; // 처음 끈건지 체크 

    // 미션 카운트 초기화
    private void OnEnable()
    {
        MissionManager.Instance.curCnt = 0;
        MissionManager.Instance.completeCnt = 5;
    }

    // 플레이어가 근처에 있음
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = isInter ? "E키로 스위치를 끌 수 있다." : "E키로 스위치를 켤 수 있다.";
        }
    }

    // 스위치 켜기
    protected override void On()
    {
        interactionText.text = "E키로 스위치 끌 수 있다.";
        isInter = true;
        SoundManager.Instance.SFXPlay(SfxType.WaterSwitch);

        // 최종적으로 체크 할 오브젝트가 아니면 리턴
        if(!isWater) return;
        waterSound.SetActive(true);
    }

    // 스위치 끄기
    protected override void Off()
    {
        interactionText.text = "E키로 스위치 켤 수 있다.";
        isInter = false;
        SoundManager.Instance.SFXPlay(SfxType.WaterSwitch);

        // 최종적으로 체크 할 오브젝트가 아니면 리턴
        if(!isWater) return;
        waterSound.SetActive(false);

        // 처음 끈게 아니면 리턴
        if(isFirst) return;
        isFirst = true;
        MissionManager.Instance.curCnt++;
        MissionManager.Instance.CheckWaterLevelMission();

    }
}
