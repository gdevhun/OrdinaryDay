using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private FirstPlayer FirstPlayer;
    public ScriptableObject scriptableObject;
    private bool _isNextText;
    private void Start()
    {
        Debug.Log(scriptableObject.ToString());
        QuestManager.Instance.LoadQuestData("QuestData1_1");
        // 처음에는 모두 비활성화
        //활성화되면 -> start에서 퀘스트정보 얻어오고

    }

    /*private async UniTaskVoid DisplayNextText()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        _isNextText = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        _isNextText = false;
    }*/
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && !TextManager.Instance.isOverTextRoutine)
        {   // 엔터, 텍스 출력코루틴이 끝나지않았다면
            TextManager.Instance.DisplayTextInstantly(); //함수호출로 텍스트 바로 렌더링
            //isOverTextRoutine true됨.
        }
    }

     private async UniTaskVoid OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPlayer.isFade = true;
            for (int textNum = 0; textNum < QuestManager.Instance.totalTextCnt; textNum++)
            {
                TextManager.Instance.DisplayTextSlowly(QuestManager.Instance.questData.talkText[textNum]);
                await UniTask.WaitUntil(() => Input.GetKeyUp(KeyCode.Return) && TextManager.Instance.isOverTextRoutine);
            }
            if(Input.GetKeyUp(KeyCode.Return))
            {
                TextManager.Instance.isOverTextRoutine = false;
                TextManager.Instance.talkTextUI.SetActive(false);  // 비활성화
                FirstPlayer.isFade = false;
                gameObject.SetActive(false);
            }
        }
    }
}
