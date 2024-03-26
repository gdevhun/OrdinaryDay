using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    // 플레이어가 근처에있는지 체크
    bool isNear = false;

    // 문이 열린 상태인지 체크
    public bool isOpen = false;

    // 목표 회전 각도
    public Quaternion targetRotation;

    // 문이 열리거나 닫히는 속도
    private float rotationSpeed = 2f;

    // 맵 이동
    private MapTeleport mapTeleport;

    void Awake()
    {
        // 문 각도로 초기화
        targetRotation = transform.rotation;

        // 맵 이동
        mapTeleport = GetComponent<MapTeleport>();
    }

    // 플레이어가 근처에 있음
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            isNear = true;
        }
    }

    // 플레이어가 근처에 없음
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isNear = false;
        }
    }

    // 플레이어가 근처에 있는경우
    // E를 눌러서 문을 열고 닫음
    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen) // 닫힌상태였으면 열음
            {
                // 맵을 이동하는 문이면 맵 텔레포트
                if (gameObject.tag.Equals("MapTeleport"))
                {
                    mapTeleport.Teleport();
                }
                else
                {
                    if(gameObject.tag.Equals("LeftDoor")) // 왼쪽문
                    {
                        targetRotation *= Quaternion.Euler(0, 0, 90f);
                    }
                    else if(gameObject.tag.Equals("RightDoor")) // 오른쪽문
                    {
                        targetRotation *= Quaternion.Euler(0, 0, -90f);
                    }

                    // 플래그
                    isOpen = true;

                    // 문여는소리
                    PoolManager.instance.GetObj(ObjType.문여는소리);
                }
            }
            else // 열린 상태였으면 닫음
            {
                if(gameObject.tag.Equals("LeftDoor")) // 왼쪽문
                {
                    targetRotation *= Quaternion.Euler(0, 0, -90f);
                }
                else if(gameObject.tag.Equals("RightDoor")) // 오른쪽문
                {
                    targetRotation *= Quaternion.Euler(0, 0, 90f);
                }

                // 플래그
                isOpen = false;

                // 문닫는소리
                PoolManager.instance.GetObj(ObjType.문닫는소리);
            }
        }

        // 문 서서히 열리거나 닫힘
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
