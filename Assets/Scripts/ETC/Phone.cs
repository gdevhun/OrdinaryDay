using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : InteractionBase
{
    public GameObject phone; //상호작용 폰
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.SFXPlay(SfxType.PhoneFirstBell);
            isNear = true;
            interactionText.text = "E키로 핸드폰을 볼 수 있다.";
        }
    }
    protected override void Interaction()
    {
        phone.gameObject.SetActive(true);
    }
}
