using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class QuestManager : SingletonBehaviour<QuestManager>
{
	public List<GameObject> triggerObject; //트리거체크할 씬 오브젝트
	private string _selectedQuestName; 
	public QuestData questData;
	public int totalTextCnt;
	public bool isLoadedData;
	public void LoadQuestData(string stageName)
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
			isLoadedData = true;

			totalTextCnt = questData.talkText.Count; //스크립터블오브젝트 총 대화길이.
		}
		else
		{
			// 로드 실패
			Debug.LogError($"Failed to load QuestData for {_selectedQuestName}.");
			isLoadedData = false;
		}
	}
}
