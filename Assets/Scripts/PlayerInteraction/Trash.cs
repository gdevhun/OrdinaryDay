using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : InteractionBase
{
    private GameObject playerHand; // 플레이어 손
    private Rigidbody rigid; // 물리
    private MeshCollider meshCollider; // 콜라이더

    // 플레이어가 근처에 있음
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = "R키로 쓰레기와 상호작용이 가능하다.";
        }
    }

    // 초기화
    protected override void Awake()
    {
        base.Awake();

        // 플레이어 손
        playerHand = GameObject.FindGameObjectWithTag("PlayerHand");

        // 물리
        rigid = GetComponent<Rigidbody>();

        // 콜라이더
        meshCollider = GetComponent<MeshCollider>();
    }

    // 상호작용 실행
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(!isNear) return;

            Interaction();
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

    // 쓰레기 들기
    private void PickUp()
    {
        // 플레이어 손을 부모로 설정하고 로컬위치 초기화
        transform.SetParent(playerHand.transform);
        transform.localPosition = Vector3.zero;

        // 물리 비활성화
        rigid.isKinematic = true;

        // 콜라이더 비활성화
        meshCollider.enabled = false;

        // 쓰레기를 플레이어 손 위치로 이동
        rigid.position = playerHand.transform.position;

        // 플래그
        isInter = true;

        // 쓰레기 줍는소리
        SoundManager.Instance.SFXPlay(SfxType.PickUpThing);
    }

    // 쓰레기 놓기
    private void Drop()
    {
        // 플레이어 손을 부모에서 해제
        transform.SetParent(null);
        
        // 물리 활성화
        rigid.isKinematic = false;

        // 콜라이더 활성화
        meshCollider.enabled = true;

        // 플래그
        isInter = false;

        // 쓰레기 놓는소리
        SoundManager.Instance.SFXPlay(SfxType.DropThing);
        
        // 상호작용 텍스트 지우기
        interactionText.text = "";
    }
}
