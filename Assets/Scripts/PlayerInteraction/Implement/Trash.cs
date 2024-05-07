using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : InteractionBase, IHandPickable
{
    // 인터페이스 변수
    public GameObject PlayerHand { get; set; } // 플레이어 손
    public Rigidbody Rigid { get; set; } // 물리
    public Collider Coll { get; set; } // 콜라이더

    // 기타 변수
    private bool isTrashBinNear; // 쓰레기통이 가까이있는지 체크
    [SerializeField] private List<GameObject> trashList = new List<GameObject>(); // 쓰레기 놓으면 활성화 시켜줄 쓰레기
    [SerializeField] private ClipBoard clipBoard; // 클립보드

    // 플레이어가 근처에 있음
    protected override void OnTriggerEnter(Collider other)
    {
        if(!clipBoard.isClipBoard) return; // 아직 클립보드를 확인하지 않았으면 리턴
        base.OnTriggerEnter(other);
        if(other.CompareTag("TrashBin")) isTrashBinNear = true;
    }

    // 플레이어가 근처에 없음
    protected override void OnTriggerExit(Collider other)
    {
        if(!clipBoard.isClipBoard) return; // 아직 클립보드를 확인하지 않았으면 리턴
        base.OnTriggerExit(other);
        if(other.CompareTag("TrashBin")) isTrashBinNear = false;
    }

    // 초기화
    protected override void Awake()
    {
        base.Awake();
        PlayerHand = GameObject.FindGameObjectWithTag("PlayerHand");
        Rigid = GetComponent<Rigidbody>();
        Coll = GetComponent<MeshCollider>();
    }

    // 쓰레기 들기
    protected override void On()
    {
        if(!clipBoard.isClipBoard) return; // 아직 클립보드를 확인하지 않았으면 리턴

        // 들기
        PickUp();

        isInter = true;
        SoundManager.Instance.SFXPlay(SfxType.PickUpThing);
    }

    // 쓰레기 놓기
    protected override void Off()
    {
        if(!clipBoard.isClipBoard) return; // 아직 클립보드를 확인하지 않았으면 리턴
        
        // 놓기
        Drop();

        isInter = false;
        SoundManager.Instance.SFXPlay(SfxType.DropThing);
        interactionText.text = "";

        // 쓰레기통에 놓은거면
        if(isTrashBinNear)
        {
            // 쓰레기 비활성화
            gameObject.SetActive(false);

            // 쓰레기통 쓰레기 활성화 및 개수 카운팅
            trashList[MissionManager.Instance.curCnt++].gameObject.SetActive(true);

            // 쓰레기 미션 체크
            TrashCheck();
        }
    }

    // 인터페이스 구현
    public void PickUp()
    {
        // 플레이어 손을 부모로 설정하고 로컬위치 초기화
        transform.SetParent(PlayerHand.transform);
        transform.localPosition = Vector3.zero + new Vector3(0, 0.5f, 0.5f);

        // 물리 비활성화
        Rigid.isKinematic = true;

        // 콜라이더 비활성화
        Coll.enabled = false;

        // 쓰레기를 플레이어 손 위치로 이동
        Rigid.position = PlayerHand.transform.position;
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

    // 쓰레기 미션 체크
    private void TrashCheck()
    {
        string tempString;
        switch (MissionManager.Instance.curCnt)
        {
            case 1: tempString = MissionManager.Instance.missionTextList[1]; MissionManager.Instance.DisplayMissonText(tempString);
                break;
            case 2:  tempString = MissionManager.Instance.missionTextList[2]; MissionManager.Instance.DisplayMissonText(tempString);
                break;
            case 3:  tempString = MissionManager.Instance.missionTextList[3]; MissionManager.Instance.DisplayMissonText(tempString);
                break;
            case 4:  tempString = MissionManager.Instance.missionTextList[4]; MissionManager.Instance.DisplayMissonText(tempString);
                break;
            case 5:  MissionManager.Instance.HideMissionText(); MissionManager.Instance.CheckTrashMission();
                break;
        }
    }
}
