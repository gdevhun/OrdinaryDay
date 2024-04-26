using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private FirstPlayer firstPlayer;
    public ScriptableObject scriptableObject;
    public string scriptableObjectName;
    public GameObject nextTriggerObj;
    public bool isOverCurText;
    private bool _isOverQuestTrig;
    public bool isPassWordEvent;
    private void OnEnable()
    {
        QuestManager.Instance.LoadQuestData(scriptableObjectName);
        // 처음에는 모두 비활성화
        //활성화되면 -> start에서 퀘스트정보 얻어오고
    }

    private void Update()
    {
       
        /*if (Input.GetKeyUp(KeyCode.Return) && !TextManager.Instance.isOverTextRoutine)
        {   // 엔터, 텍스 출력코루틴이 끝나지않았다면
            TextManager.Instance.DisplayTextInstantly(); //함수호출로 텍스트 바로 렌더링
            //isOverTextRoutine true됨.
        }*/

        if (Input.GetKeyUp(KeyCode.Return) && TextManager.Instance.isOverTextRoutine)
        {
            isOverCurText = true;
        }

        else if (_isOverQuestTrig)
        {
            if(Input.GetKeyUp(KeyCode.Return))
            {
                TextManager.Instance.isOverTextRoutine = false;
                TextManager.Instance.talkTextUI.SetActive(false);  // 비활성화
                firstPlayer.isFade = false;
                gameObject.SetActive(false);
                nextTriggerObj.SetActive(true);
                if (isPassWordEvent)
                {
                    if (TryGetComponent(out PasswordEvent pe))
                    {
                        pe.StartCutScene();
                    }
                }
            }
        }
    }

   
    private async UniTask OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstPlayer.isFade = true;
            if (QuestManager.Instance.isLoadedData)
            {
                for (int textNum = 0; textNum < QuestManager.Instance.totalTextCnt; textNum++)
                {
                    isOverCurText = false;
                    TextManager.Instance.DisplayTextSlowly(QuestManager.Instance.questData.talkText[textNum]);
                    await UniTask.WaitUntil(()=> isOverCurText == true);
                    
                    TextManager.Instance.isOverTextRoutine = false;
                }

                _isOverQuestTrig = true;
                if (QuestManager.Instance.questData.isMissionAssigned) //미션업데이트
                {
                    MissionManager.Instance.DisplayMissonText(QuestManager.Instance.questData.missionName); 
                    //미션 HUD 출력
                }
            }
            
        }
    }
   
    
}
