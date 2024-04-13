using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;



public class PhoneEvent : MonoBehaviour
{
    public Image firstEvent;
    public Image secondEvent;
    public Image thirdEvent;
    // Start is called before the first frame update
    private void OnEnable()
    {
        PhoneEventTrig().Forget();
    }

    private async UniTaskVoid PhoneEventTrig()
    {
        
    }
}
