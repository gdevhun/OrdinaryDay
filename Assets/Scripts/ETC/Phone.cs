using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : InteractionBase
{
    public GameObject phone; // 핸드폰

    // 플레이어가 근처에 있음
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            SoundManager.Instance.SFXPlay(SfxType.PhoneFirstBell);
            interactionText.text = "E키로 핸드폰을 볼 수 있다.";
        }
    }

    // 상호작용
    protected override void Interaction()
    {
        if (!isInter) LookPhone(); // 핸드폰 보기
    }

    // 핸드폰 보기
    private void LookPhone()
    {
        phone.gameObject.SetActive(true);

        isInter = true;

        interactionText.text = "";

    }
}
