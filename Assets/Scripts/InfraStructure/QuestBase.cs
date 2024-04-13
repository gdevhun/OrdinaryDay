using UnityEngine;
public class QuestBase : MonoBehaviour
{
    
    //첫번째 자식만 활성화하고 나머지자식들(trigger들)은 비활성화해놓는 퀘스트베이스코드
    //Quest 오브젝트마다 스크립트를 할당시켜줘야함.
    void OnEnable()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        // 첫 번째 자식을 제외한 나머지 자식들을 비활성화
        // i=0 부모, i=1 첫번째자식... i=2 부터.
        for (int i = 2; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
        }
    }

 
}
