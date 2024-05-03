using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Hammer : InteractionBase, IHandPickable
{
    // 인터페이스 변수
    public GameObject PlayerHand { get; set; } // 플레이어 손
    public Rigidbody Rigid { get; set; } // 물리
    public Collider Coll { get; set; } // 콜라이더

    // 기타 변수
    private bool isBreak; // 문을 부셨는지 체크
    private bool isNearOscarRoomDoor; // OscarRoomDoor가 근처에 있는지 체크
    [SerializeField] private GameObject oscarRoomDoor; // oscarRoomDoor

    private void OnTriggerStay(Collider other)
    {
        // 들고 있는 상태에서
        // oscarRoomDoor 근처에있으면
        // T키로 망치를 사용할 수 있음
        if(isInter && other.CompareTag("OscarRoomDoor"))
        {
            isNearOscarRoomDoor = true;
            interactionText.text = "T키로 망치를 사용할 수 있다.";
        }
    }

    // 플레이어가 근처에 없음
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        // oscarRoomDoor가 근처에 없음
        if(!isBreak && other.CompareTag("OscarRoomDoor"))
        {
            isNearOscarRoomDoor = false;
            interactionText.text = "";
        }
    }

    // 초기화
    protected override void Awake()
    {
        base.Awake();
        PlayerHand = GameObject.FindGameObjectWithTag("PlayerHand");
        Rigid = GetComponent<Rigidbody>();
        Coll = GetComponent<BoxCollider>();

        // Test
        //PeterView();
    }

    // 상호작용 실행
    protected override void Update()
    {
        base.Update();

        // T 키로 문 부수기
        if(!isBreak && isNearOscarRoomDoor && Input.GetKeyDown(KeyCode.T))
        {
            isBreak = true;
            oscarRoomDoor.SetActive(false);
            SoundManager.Instance.SFXPlay(SfxType.UseHammer);
            interactionText.text = "";
            FadeManager.Instance.Fade(1.5f);
            SoundManager.Instance.SFXPlay(SfxType.BrokenOscarDoor);
            SoundManager.Instance.BgmSoundPlay(BgmType.Oscar);

            // 오스카 컷씬

            // 피터 시점
            PeterView();
        }
    }

    // 망치 들기
    protected override void On()
    {
        // 들기
        PickUp();

        isInter = true;
        SoundManager.Instance.SFXPlay(SfxType.PickUpHammer);
    }

    // 망치 놓기
    protected override void Off()
    {
        // 놓기
        Drop();

        isInter = false;
        interactionText.text = "";
        SoundManager.Instance.SFXPlay(SfxType.DropHammer);
    }

    // 인터페이스 구현
    public void PickUp()
    {
        // 플레이어 손을 부모로 설정하고 로컬위치 초기화
        transform.SetParent(PlayerHand.transform);
        transform.localPosition = Vector3.zero + new Vector3(0.7f, 1.4f, 1.2f);
        transform.localRotation = Quaternion.Euler(-230, 50, -140);

        // 물리 비활성화
        Rigid.isKinematic = true;

        // 콜라이더 비활성화
        Coll.enabled = false;
    }

    // 인터페이스 구현
    public void Drop()
    {
        // 플레이어 손을 부모에서 해제
        transform.SetParent(null);

        // 물리 활성화
        Rigid.isKinematic = false;

        // 콜라이더 활성화
        Coll.enabled = true;
    }

    // 피터 시점
    private void PeterView()
    {
        FirstPlayer firstPlayer = PlayerHand.GetComponentInParent<FirstPlayer>();
        firstPlayer.transform.position = new Vector3(24f, 0f, 0f);
        firstPlayer.transform.rotation = quaternion.Euler(0f, -90f, 0f);
        firstPlayer.tag = "Peter";
    }
}
