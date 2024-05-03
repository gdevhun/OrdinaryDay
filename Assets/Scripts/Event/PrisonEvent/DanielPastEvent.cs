using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class DanielPastEvent : PrisonEventBase
{
    protected override void EventOccur()
    {
        base.EventOccur();
        StartCutScene().Forget();
    }

    public async UniTask StartCutScene()
    {
        FadeManager.Instance.Fade(1f);
        TimeLineManager.Instance.OnCutSceneObj(cutSceneType.DanielPastView);
        await UniTask.WaitUntil(() => TimeLineManager.Instance.cutSceneDirector.state != PlayState.Playing);
        FadeManager.Instance.Fade(2f);
        TimeLineManager.Instance.OffCutSceneObj(cutSceneType.DanielPastView);
    }
}
