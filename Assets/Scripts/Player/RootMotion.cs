using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    public Animator myAnim = null; //animator 연동
    public Transform Root;

    private void OnAnimatorMove()
    {
        Root.position += myAnim.deltaPosition;
        Root.rotation *= myAnim.deltaRotation; //쿼터니언이기 때문에 곱하기 연산
    }
}