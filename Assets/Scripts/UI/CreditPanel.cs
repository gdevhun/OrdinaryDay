using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CreditPanel : MonoBehaviour
{
    private void OnEnable()
    {
        SoundManager.Instance.BgmSoundPlay(BgmType.CreditBGM);
    }

    public void GoToMenuScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SoundManager.Instance.BgmSoundPlay(BgmType.Lab);
        GameManager.Instance.GoMenuScene();
    }
}
