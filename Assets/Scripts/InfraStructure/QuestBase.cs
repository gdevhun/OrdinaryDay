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
        // 초기 하이라키창의 Quest1-Trigger1을 활성화시켜야 정상작동함. 나머지는 비활성화상태
        children[1].gameObject.SetActive(true);
        Debug.Log("whatthebug");
        for (int i = 2; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
        }
    }
}
