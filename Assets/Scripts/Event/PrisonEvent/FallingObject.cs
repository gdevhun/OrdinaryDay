using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FallingObject : PrisonEventBase
{
    protected override void EventOccur()
    {
        base.EventOccur();
        FallObject().Forget();
    }

    private async UniTask FallObject()
    {
        ActiveRigid();
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        if (gameObject.CompareTag("Metal"))
        {
            SoundManager.Instance.SFXPlay(SfxType.MetalFallSfx);
        }
        else
        {   //TAG="PLASTIC"
            SoundManager.Instance.SFXPlay(SfxType.FallingBodyBag1);
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            SoundManager.Instance.SFXPlay(SfxType.FallingBodyBag2);
        }
        SoundManager.Instance.SFXPlay(SfxType.WeirdSfx);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        UnActiveRigid();
    }
}
