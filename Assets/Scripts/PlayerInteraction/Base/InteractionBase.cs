using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 상속받는 각 오브젝트에서 상호작용 오버라이딩
public class InteractionBase : MonoBehaviour
{
    protected bool isNear = false; // 플레이어와 가까운지 체크
    protected bool isInter = false; // 플레이어가 오브젝트와 상호작용을 한번 한 상태인지 체크
    protected GameObject interactionPanel; // 상호작용 패널
    protected TMP_Text interactionText; // 상호작용 텍스트
    [SerializeField] protected string interText; // 상호작용 텍스트
    [SerializeField] protected KeyCode interKey; // 상호작용 키

    // 플레이어가 근처에 있음
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = interText;
        }
    }

    // 플레이어가 근처에 없음
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            interactionText.text = "";
        }
    }

    // 초기화
    protected virtual void Awake()
    {
        interactionPanel = GameObject.FindGameObjectWithTag("InteractionPanel");
        interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
    }

    // 상호작용 실행
    protected virtual void Update()
    {
        if (Input.GetKeyDown(interKey)) // 상호작용 키를 눌렀을 때
        {
            if(!isNear) return; // 플레이어가 가까운게 아니면 리턴
            Interaction();
        }
    }

    // 상호작용
    protected virtual void Interaction()
    {
        if (!isInter) // 아직 상호작용하지 않은 상태면 On
        {
            On();
            return;
        }
        
        Off(); // 이미 상호작용 한 상태면 Off
    }

    protected virtual void On() {}
    protected virtual void Off() {}
}
