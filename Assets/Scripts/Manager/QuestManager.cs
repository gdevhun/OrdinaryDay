using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
	
	private string _selectedQuestName;
	public QuestData questData;
	void Start()
    {
		OnActiveQuestNum("QuestData1");
    }
	public void OnActiveQuestNum(string stageName)
	{   //퀘스트에셋 넘버 불러오기
		_selectedQuestName = stageName;
		LoadSelectedStage();
	}

	private void LoadSelectedStage()
	{
		questData = Resources.Load<QuestData>(_selectedQuestName);

		if (questData != null)
		{
			// 로드 성공
			Debug.Log("QuestData loaded successfully.");
		}
		else
		{
			// 로드 실패
			Debug.LogError($"Failed to load QuestData for {_selectedQuestName}.");
		}
	}



}
