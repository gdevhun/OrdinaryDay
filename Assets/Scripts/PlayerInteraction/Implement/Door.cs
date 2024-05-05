using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractionBase
{
    private Quaternion targetRotation; // 목표 회전 각도
    private float rotationSpeed = 2f; // 문이 열리거나 닫히는 속도
    private MapTeleport mapTeleport; // 맵 이동
    private string tag; // 태그

    // 초기화
    protected override void Awake()
    {
        base.Awake();
        targetRotation = transform.rotation;
        mapTeleport = GetComponent<MapTeleport>();
        tag = gameObject.tag;
    }

    // 상호작용 실행
    protected override void Update()
    {
        base.Update();
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); // 문 서서히 열리거나 닫힘
    }

    // 문 열기
    protected override void On()
    {
        // 맵 이동 문
        if (tag == "MapTeleport")
        {
            mapTeleport.Teleport();
            return;
        }

        // 탈출 문 => True Ending
        if(tag == "EscapeDoor")
        {
            MissionManager.Instance.trueEndingCredit.SetActive(true);
            FirstPlayer firstPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPlayer>();
            firstPlayer.isFade = true;
            firstPlayer.gameObject.SetActive(false);
            firstPlayer.danielAI.gameObject.SetActive(false);
            SoundManager.Instance.BgmSoundPlay(BgmType.CreditBGM);
            SoundManager.Instance.SFXPlay(SfxType.OpenDoor);
            return;
        }

        switch (tag)
        {   
            // 왼쪽문 및 튜토리얼 문
            case "LeftDoor":
            case "FirstDoor":
                targetRotation *= Quaternion.Euler(0, 0, 90f);
                break;
            
            // 오른쪽 문
            case "RightDoor":
                targetRotation *= Quaternion.Euler(0, 0, -90f);
                break;
        }

        isInter = true;
        SoundManager.Instance.SFXPlay(SfxType.OpenDoor);
    }

    // 문 닫기
    protected override void Off()
    {
        switch (tag)
        {
            // 왼쪽문 및 튜토리얼 문
            case "LeftDoor":
            case "FirstDoor":
                targetRotation *= Quaternion.Euler(0, 0, -90f);
                break;
            
            // 오른쪽 문
            case "RightDoor":
                targetRotation *= Quaternion.Euler(0, 0, 90f);
                break;
        }

        isInter = false;
        SoundManager.Instance.SFXPlay(SfxType.CloseDoor);
    }
}
