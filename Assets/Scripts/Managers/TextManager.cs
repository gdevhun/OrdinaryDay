using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
	public static TextManager instance; 

	public GameObject talkTextUI; 
	public TextMeshProUGUI talkText; 
	private WaitForSeconds textDelay;
	private string tempText;

	public bool isOverTextRoutine = false; //텍스트출력이 되고있는지의 bool변수


	private void Awake()
	{
		if (instance == null)
		{
			// 텍스트 메니저 싱글톤
			instance = this;
			// 파괴X
			DontDestroyOnLoad(instance);
		}
		else
		{
			// 에외처리
			Destroy(gameObject);
		}
	}
	void Start()
	{
		talkTextUI.SetActive(false); //텍스트는 초기에 비활성화.
		textDelay = new WaitForSeconds(0.1f); //text 출력 딜레이는 0.1초
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			DisplayTextSlowly(QuestManager.instance.questData.talkText[0]);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			DisplayTextSlowly(QuestManager.instance.questData.talkText[1]);
		}
	}
	public void DisplayTextSlowly(string text) //기본적인 텍스트 출력함수
	{
		talkTextUI.SetActive(true);
		StartCoroutine(DisplayText(text));
	}
	public void DisplayTextInstantly()  //엔터누르면실행->즉시 텍스트 출력 함수
	{
		StopAllCoroutines(); // 모든코루틴중지하고
		talkText.text = tempText; //텍스트 바로출력
		isOverTextRoutine = true;
	}
	IEnumerator DisplayText(string message) // 텍스트출력 코루틴
	{
		tempText = message;
		talkText.text = ""; //텍스트 초기화(비워주고)
		for (int i = 0; i < message.Length; i++)
		{
			talkText.text += message[i];
			yield return textDelay; //0.1f에 맞춰 텍스트 출력
		}
		isOverTextRoutine = true; //다출력됨.
	}

}