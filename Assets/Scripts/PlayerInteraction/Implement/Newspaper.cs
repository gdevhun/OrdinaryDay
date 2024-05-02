using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Newspaper : InteractionBase
{
    [SerializeField] private GameObject newspaperTextPanel; // 뉴스 패널
    [SerializeField] [TextArea(3, 5)] private string newpaperText; // 뉴스 텍스트

    // 플레이어가 근처에 없음
    // 신문은 Exit 할때 내려놓음
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            if(isInter) NewsOff();
            interactionText.text = "";
        }
    }

    // 신문 읽기
    protected override void On()
    {
        newspaperTextPanel.gameObject.SetActive(true);
        newspaperTextPanel.GetComponentInChildren<TMP_Text>().text = newpaperText;
        if(!isInter) SoundManager.Instance.SFXPlay(SfxType.CheckClipBoard);
        isInter = true;
        interactionText.text = "";
    }

    // 신문 내려놓기
    private void NewsOff()
    {
        isInter = false;
        newspaperTextPanel.gameObject.SetActive(false);
        SoundManager.Instance.SFXPlay(SfxType.PutClipBoard);
    }
}
