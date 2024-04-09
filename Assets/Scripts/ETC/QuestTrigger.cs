using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private FirstPlayer FirstPlayer;
    public ScriptableObject scriptableObject;
   
    private void Start()
    {
        string totalName = scriptableObject.ToString();
        QuestManager.Instance.LoadQuestData(totalName);
        Debug.Log(QuestManager.Instance.questData.questNumber);
        // 처음에는 모두 비활성화
        //활성화되면 -> start에서 퀘스트정보 얻어오고

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var textNum = QuestManager.Instance.totalTextCnt;
            FirstPlayer.isFade = true;
            while (textNum != 0)
            {
                TextManager.Instance.DisplayTextSlowly(QuestManager.Instance.questData.talkText[textNum]);
                if (Input.GetKeyUp(KeyCode.Return) && !TextManager.Instance.isOverTextRoutine)
                {   // 엔터, 텍스 출력코루틴이 끝나지않았다면
                    TextManager.Instance.DisplayTextInstantly(); //함수호출로 텍스트 바로 렌더링
                }
                else if (Input.GetKeyUp(KeyCode.Return) && TextManager.Instance.isOverTextRoutine)
                {   //엔터, 텍스 출력 코루틴이 끝난 상황이면
                    TextManager.Instance.isOverTextRoutine = false;
                    TextManager.Instance.talkTextUI.SetActive(false);  // 비활성화
                }
                textNum--;
            }
            FirstPlayer.isFade = false;
            gameObject.SetActive(false);
        }
    }

    
}
