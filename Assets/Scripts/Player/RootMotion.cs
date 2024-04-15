using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    public Animator myAnim = null; //animator ����
    public Transform Root;

    private void OnAnimatorMove()
    {
        Root.position += myAnim.deltaPosition;
        Root.rotation *= myAnim.deltaRotation; //���ʹϾ��̱� ������ ���ϱ� ����
    }
}