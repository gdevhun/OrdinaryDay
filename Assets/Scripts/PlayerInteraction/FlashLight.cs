using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : InteractionBase
{
    public GameObject playerFlashLightHand; // 손전등 손
    public TurnOnLight turnOnLight; // 플레이어 손전등 상호작용

    // 플레이어가 근처에 있음
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = "E키로 손전등을 들 수 있다.";
        }
    }

    // 상호작용
    protected override void Interaction()
    {
        if (!isInter) GetFlashLight(); // 아직 얻지않은 상태면 얻음
    }

    // 손전등 얻기
    private void GetFlashLight()
    {
        // 손전등 얻었다고 체크
        turnOnLight.isFlashLight = true;
        interactionText.text = "";

        // 손전등 손 활성화
        playerFlashLightHand.gameObject.SetActive(true);

        // 사운드
        SoundManager.Instance.SFXPlay(SfxType.GetFlashLight);

        // F키 사용 설명
        StartCoroutine(DisplayInstruction());
    }

    // F키 사용 설명
    private IEnumerator DisplayInstruction()
    {
        // 사용 설명
        yield return new WaitForSeconds(1f);
        interactionText.text = "F키로 손전등을 켜고 끌 수 있습니다.";
        yield return new WaitForSeconds(5f);
        interactionText.text = "";
        
        // 손전등 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
