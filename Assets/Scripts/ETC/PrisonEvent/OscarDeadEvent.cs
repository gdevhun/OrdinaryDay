using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class OscarDeadEvent : PrisonEventBase
{
    [SerializeField] private GameObject player;
    [SerializeField] private Hammer hammer;
    protected override void EventOccur()
    {
        base.EventOccur();
        StartCutScene().Forget();
    }

    public async UniTask StartCutScene()
    {
        FadeManager.Instance.Fade(1f);
        TimeLineManager.Instance.OnCutSceneObj(cutSceneType.OscarDead);
        hammer.GetComponent<Hammer>().PeterView();
        player.gameObject.SetActive(false);
        await UniTask.WaitUntil(() => TimeLineManager.Instance.cutSceneDirector.state != PlayState.Playing);
        FadeManager.Instance.Fade(2f);
        TimeLineManager.Instance.OffCutSceneObj(cutSceneType.OscarDead);
        
        //PETER씬으로 이동하기.
        player.gameObject.SetActive(true);
    }
}
