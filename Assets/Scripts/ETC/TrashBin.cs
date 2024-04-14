using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : InteractionBase
{
    private string _curMissionState;
    private int _totalTrashCleaned;
    public GameObject[] trashList;
    protected override void Awake()
    {
        base.Awake();
        
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            interactionText.text = "R키로 쓰레기통과 상호작용이 가능하다.";
        }

        if (other.CompareTag("Trash"))
        {
            foreach (var trash in trashList)
            {
                if (other.gameObject == trash)
                {
                    _totalTrashCleaned++;
                    break; 
                }
                    
            }
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        foreach (var trash in trashList)
        {
            if (other.gameObject == trash)
            {
                _totalTrashCleaned--;
                break;
            }
        }
    }

    protected override void Update()
    {
        
    }

    protected override void Interaction()
    {
        
    }
}
