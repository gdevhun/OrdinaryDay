using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬 전환 시 파괴 되어야하는 싱글톤
public class DestroySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    protected virtual void Awake() { Instance = this as T; }
}
