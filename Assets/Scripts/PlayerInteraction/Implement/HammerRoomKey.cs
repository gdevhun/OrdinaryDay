using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HammerRoomKey : InteractionBase, IHandPickable
{
    // 인터페이스 변수
    public GameObject playerHand { get; set; } // 플레이어 손
    public Rigidbody rigid { get; set; } // 물리
    public Collider coll { get; set; } // 콜라이더

    private bool isOpen; // 열쇠로 문을 열었는지 체크 => 열고나서는 D키 관련 전부 작동 안 하게
    private bool isNearControlRoomDoor; // ControlRoomDoor가 근처에 있는지 체크
    public GameObject controlRoomDoor; // ControlRoomDoor => 열쇠로 열면 AE_Door enable true

    private void OnTriggerStay(Collider other)
    {
        // 들고 있는 상태에서
        // ControlRoomDoor가 근처에있으면
        // T키로 열쇠를 사용할 수 있음
        if(!isOpen && isInter && other.CompareTag("ControlRoomDoor"))
        {
            isNearControlRoomDoor = true;
            interactionText.text = "T키로 열쇠를 사용할 수 있다.";
        }
    }

    // 플레이어가 근처에 없음
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        // ControlRoomDoor가 근처에 없음
        if(!isOpen && other.CompareTag("ControlRoomDoor"))
        {
            isNearControlRoomDoor = false;
            interactionText.text = "";
        }
    }

    // 초기화
    protected override void Awake()
    {
        base.Awake();
        playerHand = GameObject.FindGameObjectWithTag("PlayerHand");
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
    }

    // 상호작용 실행
    protected override void Update()
    {
        base.Update();

        // T 키로 문열기 => 한번만
        if(!isOpen && isNearControlRoomDoor && Input.GetKeyDown(KeyCode.T))
        {
            controlRoomDoor.GetComponent<AE_Door>().enabled = true;
            isOpen = true;
            interactionText.text = "E키로 문과 상호작용이 가능하다.";
            controlRoomDoor.GetComponent<AE_Door>().isControl = false;
            SoundManager.Instance.SFXPlay(SfxType.UseKey);
        }
    }

    // 열쇠 들기
    protected override void On()
    {
        // 들기
        PickUp();

        isInter = true;
        SoundManager.Instance.SFXPlay(SfxType.PickUpDropKey);
    }

    // 열쇠 놓기
    protected override void Off()
    {
        // 놓기
        Drop();

        isInter = false;
        interactionText.text = "";
        SoundManager.Instance.SFXPlay(SfxType.PickUpDropKey);
    }

    // 인터페이스 구현
    public void PickUp()
    {
        // 플레이어 손을 부모로 설정하고 로컬위치 초기화
        transform.SetParent(playerHand.transform);
        transform.localPosition = Vector3.zero + new Vector3(-0.4f, 0.7f, 0);

        // 물리 비활성화
        rigid.isKinematic = true;

        // 콜라이더 비활성화
        coll.enabled = false;
    }

    // 인터페이스 구현
    public void Drop()
    {
        // 플레이어 손을 부모에서 해제
        transform.SetParent(null);

        // 물리 활성화
        rigid.isKinematic = false;

        // 콜라이더 활성화
        coll.enabled = true;
    }
}
