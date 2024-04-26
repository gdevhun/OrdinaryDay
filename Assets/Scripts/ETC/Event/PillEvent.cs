
using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PillEvent : MonoBehaviour
{
    public GameObject player;
    public GameObject nextTrigger; //대화트리거
    
    private async UniTask OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FadeManager.Instance.Fade(1.5f);
            player.GetComponent<FirstPlayer>().isFade = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            OnCutScene();
            await UniTask.WaitUntil(() => TimeLineManager.Instance.cutSceneDirector.state != PlayState.Playing);
            FadeManager.Instance.Fade(2.5f);
            OffCutScene();
        }
    }
    private void OnCutScene()
    {
        TimeLineManager.Instance.OnCutSceneObj(cutSceneType.Pill);
    }

    private void OffCutScene()
    {
        player.transform.position = new Vector3(-8.79f, 0.68f, 5.83f);
        player.transform.rotation = Quaternion.Euler(0f, -130f, 0f);
        TimeLineManager.Instance.OffCutSceneObj(cutSceneType.Pill);
        ActiveTrigger().Forget();
    }

    private async UniTask ActiveTrigger()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2.5));
        gameObject.SetActive(false);
        nextTrigger.SetActive(true);
    }
}
