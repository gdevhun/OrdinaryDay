using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : InteractionBase
{
    public GameObject flashLight; // 손전등 빛
    public bool isFlashLight; // 손전등을 가지고있는지 체크

    // 상호작용 실행
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(!isFlashLight) return;

            Interaction();
        }
    }

    // 상호작용
    protected override void Interaction()
    {
        if (!isInter) // 꺼진상태면 켬
        {
            TurnOn();

            return;
        }

        // 켜진상태면 끔
        TurnOff();
    }

    // 손전등 켜기
    private void TurnOn()
    {
        flashLight.gameObject.SetActive(true);

        isInter = true;
        
        SoundManager.Instance.SFXPlay(SfxType.OnOffLight);
    }

    // 손전등 끄기
    private void TurnOff()
    {
        flashLight.gameObject.SetActive(false);

        isInter = false;

        SoundManager.Instance.SFXPlay(SfxType.OnOffLight);
    }
}
