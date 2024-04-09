using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "QuestData", order = 1)]
public class QuestData : ScriptableObject
{
	//퀘스트데이타 스크립터블오브젯트
	public string questInfo;  //퀘스트정보
	public int questNumber;  //퀘스트 순번
	public List<string> talkText;
	//public GameObject triggerObject; 트리거오브젝트는 런타임에서만 할당.
}


