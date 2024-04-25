using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : InteractionBase
{
    [SerializeField] private GameObject flashLight; // 손전등 빛
    [HideInInspector] public bool isFlashLight; // 손전등을 가지고있는지 체크

    // 상호작용 실행
    protected override void Update()
    {
        if (Input.GetKeyDown(interKey))
        {
            if(!isFlashLight) return; // 플레이어가 손전등을 갖고 있지 않으면 리턴
            Interaction();
        }
    }

    // 손전등 켜기
    protected override void On()
    {
        flashLight.gameObject.SetActive(true);
        isInter = true;
        SoundManager.Instance.SFXPlay(SfxType.OnOffLight);
    }

    // 손전등 끄기
    protected override void Off()
    {
        flashLight.gameObject.SetActive(false);
        isInter = false;
        SoundManager.Instance.SFXPlay(SfxType.OnOffLight);
    }
}
