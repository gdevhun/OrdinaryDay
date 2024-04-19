using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HammerRoomKey : InteractionBase
{
    public GameObject playerHand; // 플레이어 손
    private Rigidbody rigid; // 물리
    private BoxCollider boxCollider; // 콜라이더
    private bool isOpen; // 열쇠로 문을 열었는지 체크 => 열고나서는 D키 관련 전부 작동 안 하게
    private bool isNearHammerRoomDoor; // HammerRoomDoor가 근처에 있는지 체크
    public GameObject hammerRoomDoor; // HammerRoomDoor => 열쇠로 열면 AE_Door enable true

    // 플레이어가 근처에 있음
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = "R키로 열쇠와 상호작용이 가능하다.";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 들고 있는 상태에서
        // HammerRoomDoor가 근처에있으면
        // T키로 열쇠를 사용할 수 있음
        if(!isOpen && isInter && other.CompareTag("HammerRoomDoor"))
        {
            isNearHammerRoomDoor = true;
            interactionText.text = "T키로 열쇠를 사용할 수 있다.";
        }
    }

    // 플레이어가 근처에 없음
    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            interactionText.text = "";
        }

        // HammerRoomDoor가 근처에 없음
        if(!isOpen && other.CompareTag("HammerRoomDoor"))
        {
            isNearHammerRoomDoor = false;
            interactionText.text = "";
        }
    }

    // 초기화
    protected override void Awake()
    {
        base.Awake();

        // 물리
        rigid = GetComponent<Rigidbody>();

        // 콜라이더
        boxCollider = GetComponent<BoxCollider>();
    }

    // 상호작용 실행
    protected override void Update()
    {
        // R키로 줍고 떨구기
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(!isNear) return;

            Interaction();
        }

        // T 키로 문열기 => 한번만
        if(!isOpen && isNearHammerRoomDoor && Input.GetKeyDown(KeyCode.T))
        {
            hammerRoomDoor.GetComponent<AE_Door>().enabled = true;
            isOpen = true;
            interactionText.text = "E키로 문과 상호작용이 가능하다.";
            hammerRoomDoor.GetComponent<AE_Door>().isHammer = false;
            SoundManager.Instance.SFXPlay(SfxType.UseKey);
        }
    }

    // 상호작용
    protected override void Interaction()
    {
        if (!isInter) // 아직 들지 않은 상태면 들음
        {
            PickUp();

            return;
        }

        // 이미 들고 있는 상태면 놓음
        Drop();
    }

    // 열쇠 들기
    private void PickUp()
    {
        // 플레이어 손을 부모로 설정하고 로컬위치 초기화
        transform.SetParent(playerHand.transform);
        transform.localPosition = Vector3.zero + new Vector3(-0.5f, 0.5f, 0);

        // 물리 비활성화
        rigid.isKinematic = true;

        // 콜라이더 비활성화
        boxCollider.enabled = false;

        // 플래그
        isInter = true;

        // 사운드
        SoundManager.Instance.SFXPlay(SfxType.PickUpDropKey);
    }

    // 열쇠 놓기
    private void Drop()
    {
        // 플레이어 손을 부모에서 해제
        transform.SetParent(null);

        // 물리 활성화
        rigid.isKinematic = false;

        // 콜라이더 활성화
        boxCollider.enabled = true;

        // 플래그
        isInter = false;
        
        // 상호작용 텍스트 지우기
        interactionText.text = "";

        // 사운드
        SoundManager.Instance.SFXPlay(SfxType.PickUpDropKey);
    }
}
