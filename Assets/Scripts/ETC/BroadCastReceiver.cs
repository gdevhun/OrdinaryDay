using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadCastReceiver : MonoBehaviour
{
    // BroadCast를 받으면 호출될 메서드
    public void OnBroadcastReceived()
    {
        // 자식 오브젝트에서 모든 리지드바디 컴포넌트를 찾아서 isKinematic을 false로 설정
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
    }
}
