using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractionBase
{
    private Quaternion targetRotation; // 목표 회전 각도
    private float rotationSpeed = 2f; // 문이 열리거나 닫히는 속도
    private MapTeleport mapTeleport; // 맵 이동

    // 플레이어가 근처에 있음
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        // 튜토리얼문
        // 상호작용 텍스트
        if(gameObject.CompareTag("FirstDoor"))
        {
            if(!isInter)
            {
                interactionText.text = "E키로 문과 상호작용이 가능하다.";
            }
        }
    }

    // 플레이어가 근처에 없음
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        // 튜토리얼문
        // 상호작용 텍스트
        if(gameObject.CompareTag("FirstDoor"))
        {
            interactionText.text = "";
        }
    }

    protected override void Awake()
    {
        base.Awake();

        // 문 각도로 초기화
        targetRotation = transform.rotation;

        // 맵 이동
        mapTeleport = GetComponent<MapTeleport>();
    }

    // 상호작용 실행
    protected override void Update()
    {
        base.Update();

        // 문 서서히 열리거나 닫힘
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    // 상호작용
    protected override void Interaction()
    {
        if (!isInter) // 닫힌상태였으면 열음
        {
            Open();
        }
        else // 열린 상태였으면 닫음
        {
            Close();
        }
    }

    // 문 열기
    private void Open()
    {
        // 맵을 이동하는 문이면 맵 텔레포트
        if (gameObject.CompareTag("MapTeleport"))
        {
            mapTeleport.Teleport();
        }
        else
        {
            if(gameObject.CompareTag("LeftDoor")) // 왼쪽문
            {
                targetRotation *= Quaternion.Euler(0, 0, 90f);
            }
            else if(gameObject.CompareTag("RightDoor")) // 오른쪽문
            {
                targetRotation *= Quaternion.Euler(0, 0, -90f);
            }
            else if(gameObject.CompareTag("FirstDoor")) // 튜토리얼문
            {
                targetRotation *= Quaternion.Euler(0, 0, 90f);
            }

            // 플래그
            isInter = true;

            // 문여는소리
            SoundManager.instance.SFXPlay(SfxType.OpenDoor);
        }
    }

    // 문 닫기
    private void Close()
    {
        if(gameObject.CompareTag("LeftDoor")) // 왼쪽문
        {
            targetRotation *= Quaternion.Euler(0, 0, -90f);
        }
        else if(gameObject.CompareTag("RightDoor")) // 오른쪽문
        {
            targetRotation *= Quaternion.Euler(0, 0, 90f);
        }
        else if(gameObject.CompareTag("FirstDoor")) // 튜토리얼문
        {
            targetRotation *= Quaternion.Euler(0, 0, -90f);
        }

        // 플래그
        isInter = false;

        // 문닫는소리
        SoundManager.instance.SFXPlay(SfxType.CloseDoor);
    }
}
