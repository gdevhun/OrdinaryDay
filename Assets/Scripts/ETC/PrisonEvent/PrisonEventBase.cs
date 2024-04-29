using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrisonEventBase : MonoBehaviour
{
    protected bool isTriggerEntered;
    protected Vector3 targetPos;
    protected float moveDuration;
    protected float elapsedTime = 0f;
    protected virtual void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        Trans = GetComponent<Transform>();
        Coll = GetComponent<Collider>();
    }
    protected Rigidbody Rigid { get; set; }
    protected Transform Trans{ get; set; }
    protected Collider Coll { get; set; }

    private void Update()
    {
        if (isTriggerEntered)
        {
            EventOccur();
        }
    }

    protected virtual void EventOccur()
    {
        isTriggerEntered = false;
        Coll.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isTriggerEntered = true;
        };
    }
}
