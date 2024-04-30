using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FallingLamp : PrisonEventBase
{
    protected override void EventOccur()
    {
        base.EventOccur();
        FallObject().Forget();
    }

    private async UniTask FallObject()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        ActiveRigid();
        SoundManager.Instance.SFXPlay(SfxType.MetalFallSfx);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        SoundManager.Instance.SFXPlay(SfxType.WeirdSfx);
        UnActiveRigid();
    }
}
