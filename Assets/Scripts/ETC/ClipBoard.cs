using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipBoard : InteractionBase
{
    public GameObject clipBoard; // 클립보드

    // 플레이어가 근처에 있음
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = "E키로 근무일지를 확인할 수 있다.";
        }
    }

    // 플레이어가 근처에 없음
    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            interactionText.text = "";

            // 클립보드 내려놓기
            clipBoard.gameObject.SetActive(false);
            isInter = false;
        }
    }

    // 상호작용
    protected override void Interaction()
    {
        if (!isInter) // 확인하지않은상태면 확인함
        {
            CheckBoard();

            return;
        }

        // 확인한상태면 내려놓음
        PutBoard();
    }

    // 클립보드 확인하기
    private void CheckBoard()
    {
        clipBoard.gameObject.SetActive(true);

        isInter = true;
    }

    // 클립보드 내려놓기
    private void PutBoard()
    {
        clipBoard.gameObject.SetActive(false);

        isInter = false;
    }
}
