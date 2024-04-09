using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "QuestData", order = 1)]
public class QuestData : ScriptableObject
{
	//����Ʈ����Ÿ ��ũ���ͺ������Ʈ
	public string questInfo;  //����Ʈ����
	public int questNumber;  //����Ʈ ����
	public List<string> talkText;
	//public GameObject triggerObject; Ʈ���ſ�����Ʈ�� ��Ÿ�ӿ����� �Ҵ�.
}


