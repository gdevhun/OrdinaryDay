using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private FirstPlayer FirstPlayer;
    public int questNumber;

    private void Start()
    {
        string firstName = "QuestData";  
        string secondName = questNumber.ToString();
        string lastName = firstName + secondName;
        QuestManager.Instance.OnActiveQuestNum(lastName);
        Debug.Log(QuestManager.Instance.questData.QuestNumber);
        // 처음에는 모두 비활성화
        //활성화되면 -> start에서 퀘스트정보 얻어오고

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            

            FirstPlayer.isFade = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //FirstPlayer.isFade = false;  => 대화끝났을때로 옮겨야함.
            gameObject.SetActive(false);
        }
    }
}
