using System;
using UnityEditor;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public static QuestManager instance;
	private void Awake()
	{
		if (instance == null)
		{
			// �ؽ�Ʈ �޴��� �̱���
			instance = this;
			// �ı�X
			DontDestroyOnLoad(instance);
		}
		else
		{
			// ����ó��
			Destroy(gameObject);
		}
	}
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
			Debug.Log(questData.talkText1);
		}
		else
		{
			// �ε� ����
			Debug.LogError($"Failed to load QuestData for {selectedQuestName}.");
		}
	}



}
