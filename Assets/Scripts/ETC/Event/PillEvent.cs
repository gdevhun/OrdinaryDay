
using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PillEvent : MonoBehaviour
{
    public GameObject playerCam; //비활성화할 플레이어 가상캠
    public GameObject player;
    private PlayableDirector _cutSceneDirector;
    public GameObject cutSceneObj; //플레이할 컷신
    public GameObject nextTrigger; //대화트리거

    
    public bool isCutSceneOver;
    private async UniTask OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            FadeManager.Instance.Fade(1.5f);
            player.GetComponent<FirstPlayer>().isFade = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            OnCutScene();
            await UniTask.WaitUntil(() => _cutSceneDirector.state != PlayState.Playing);
            FadeManager.Instance.Fade(2.5f);
            OffCutScene();
            //await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        }
    }
    private void OnCutScene()
    {
        playerCam.SetActive(false);
        cutSceneObj.SetActive(true);
        _cutSceneDirector = cutSceneObj.GetComponent<PlayableDirector>();
        _cutSceneDirector.Play(); // 컷씬 재생 시작
    }

    private void OffCutScene()
    {
        player.transform.position = new Vector3(-8.79f, 0.68f, 5.83f);
        player.transform.rotation = Quaternion.Euler(0f, -130f, 0f);
        cutSceneObj.SetActive(false);
        playerCam.SetActive(true);
        player.GetComponent<FirstPlayer>().isFade = false;
        ActiveTrigger().Forget();
    }

    private async UniTask ActiveTrigger()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2.5));
        nextTrigger.SetActive(true);
    }
}
