using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : InteractionBase
{
    [SerializeField] private GameObject phone; // 핸드폰

    // 플레이어가 근처에 있음
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.SFXPlay(SfxType.PhoneFirstBell);
            isNear = true;
            interactionText.text = "E키로 핸드폰을 볼 수 있다.";
        }
    }

    // 핸드폰 보기
    protected override void On()
    {
        phone.gameObject.SetActive(true);
        isInter = true;
        interactionText.text = "";
    }
}
