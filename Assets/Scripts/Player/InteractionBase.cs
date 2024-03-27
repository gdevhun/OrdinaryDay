using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 상속받는 각 오브젝트에서 상호작용 오버라이딩
public abstract class InteractionBase : MonoBehaviour
{
    protected bool isNear = false; // 플레이어와 가까운지 체크
    protected bool isInter = false; // 플레이어가 오브젝트와 상호작용을 한번 한 상태인지 체크
    protected GameObject interactionPanel; // 상호작용 패널
    protected TMP_Text interactionText; // 상호작용 텍스트

    // 각 오브젝트에서 선택적 재정의

    // 플레이어가 근처에 있음
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    // 플레이어가 근처에 없음
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }

    // 초기화
    protected virtual void Awake()
    {
        // 상호작용 패널
        interactionPanel = GameObject.FindGameObjectWithTag("InteractionPanel");

        // 상호작용 텍스트
        interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
    }

    // 상호작용 실행
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!isNear) return;

            Interaction();
        }
    }

    // 각 오브젝트에서 필수 구현

    // 상호작용
    protected abstract void Interaction();
}
