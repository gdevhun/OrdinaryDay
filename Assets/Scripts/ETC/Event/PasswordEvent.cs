using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using Cysharp.Threading.Tasks;
public class PasswordEvent : MonoBehaviour
{
    public GameObject comScreen;
    public GameObject playerCam; //비활성화할 플레이어 가상캠
    public GameObject player;
    private PlayableDirector _cutSceneDirector;
    public GameObject cutSceneObj; //플레이할 컷신
    public GameObject nextTrigger; //문트리거
    public GameObject secretDoor;

    private void OnEnable()
    {
        FadeManager.Instance.Fade(0.5f);
        player.transform.LookAt(comScreen.transform);
        
    }

    public async UniTask StartCutScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        playerCam.gameObject.SetActive(false);
        player.GetComponent<FirstPlayer>().isFade = true;
        cutSceneObj.gameObject.SetActive(true);
        _cutSceneDirector = cutSceneObj.GetComponent<PlayableDirector>();
        _cutSceneDirector.Play(); // 컷씬 재생 시작
        await UniTask.WaitUntil(() => _cutSceneDirector.state != PlayState.Playing);
        FadeManager.Instance.Fade(1.5f);
        OffCutScene();
    }
    private void OffCutScene()
    {
        cutSceneObj.SetActive(false);
        playerCam.SetActive(true);
        player.GetComponent<FirstPlayer>().isFade = false;
        nextTrigger.SetActive(true);
        secretDoor.SetActive(false);
    }
    
}
