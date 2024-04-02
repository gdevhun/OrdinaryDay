using System;
using UnityEditor;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
	
	private string selectedQuestName;
	public QuestData questData;
	void Start()
    {
		OnActiveQuestNum("QuestData1");
    }
	public void OnActiveQuestNum(string stageName)
	{   //����Ʈ���� �ѹ� �ҷ�����
		selectedQuestName = stageName;
		LoadSelectedStage();
	}

	private void LoadSelectedStage()
	{
		questData = Resources.Load<QuestData>(selectedQuestName);

		if (questData != null)
		{
			// �ε� ����
			Debug.Log("QuestData loaded successfully.");
		}
		else
		{
			// �ε� ����
			Debug.LogError($"Failed to load QuestData for {selectedQuestName}.");
		}
	}



}
