using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestData", menuName = "QuestData", order = 1)]
public class QuestData : ScriptableObject
{
	//����Ʈ����Ÿ ��ũ���ͺ������Ʈ
	public string QuestInfo;  //����Ʈ����
	public int QuestNumber;  //����Ʈ ����
	public List<string> talkText;

}


