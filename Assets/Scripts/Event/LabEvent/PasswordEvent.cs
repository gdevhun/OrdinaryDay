using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using Cysharp.Threading.Tasks;
public class PasswordEvent : MonoBehaviour
{
    
    public GameObject comScreen; //바라볼 모니터 obj
    public GameObject nextTrigger; //문트리거
    public GameObject secretDoor;

    private void OnEnable()
    {
        FadeManager.Instance.Fade(0.5f);
        TimeLineManager.Instance.playerCam.transform.LookAt(comScreen.transform);
    }

    public async UniTask StartCutScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        TimeLineManager.Instance.OnCutSceneObj(cutSceneType.Password);
        await UniTask.WaitUntil(() => TimeLineManager.Instance.cutSceneDirector.state != PlayState.Playing);
        FadeManager.Instance.Fade(1.5f);
        OffCutScene();
    }
    private void OffCutScene()
    {
        TimeLineManager.Instance.OffCutSceneObj(cutSceneType.Password);
        nextTrigger.SetActive(true);
        secretDoor.SetActive(false);
        
    }
    
}
