using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 손 관련 상호작용에 상속
public interface IHandPickable
{
    GameObject PlayerHand { get; set; } // 플레이어 손
    Rigidbody Rigid { get; set; } // 물리
    Collider Coll { get; set; } // 콜라이더

    void PickUp(); // 들기
    void Drop(); // 놓기
}
