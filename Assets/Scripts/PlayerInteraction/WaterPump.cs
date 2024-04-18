using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : InteractionBase
{
    public GameObject waterSound; // 물소리
    public bool isWater; // 최종적으로 체크해야하는 오브젝트인지 체크 1, 4, 7, 9, 12
    public bool isFirst; // 처음 끈건지 체크 
    private void OnEnable()
    {
        MissionManager.Instance.curCnt = 0;
        MissionManager.Instance.completeCnt = 5;
    }

    // 플레이어가 근처에 있음
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = isInter ? "E키로 스위치를 끌 수 있다." : "E키로 스위치를 켤 수 있다.";
        }
    }

    // 상호작용
    protected override void Interaction()
    {
        if (!isInter) // 아직 스위치를 켜지 않은 상태면 켬
        {
            TurnOn();

            return;
        }

        // 스위치를 켠 상태면 끔
        TurnOff();
    }

    // 스위치 켜기
    private void TurnOn()
    {
        interactionText.text = "E키로 스위치 끌 수 있다.";
        isInter = true;
        SoundManager.Instance.SFXPlay(SfxType.WaterSwitch);

        if(!isWater) return;

        waterSound.SetActive(true);
    }

    // 스위치 끄기
    private void TurnOff()
    {
        interactionText.text = "E키로 스위치 켤 수 있다.";
        isInter = false;
        SoundManager.Instance.SFXPlay(SfxType.WaterSwitch);

        if(!isWater) return;
        waterSound.SetActive(false);

        if(isFirst) return;
        isFirst = true;
        MissionManager.Instance.curCnt++;
        MissionManager.Instance.CheckWaterLevelMission();

    }
}
