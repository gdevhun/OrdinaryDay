using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Text;
using System.Threading; // StringBuilder를 사용하기 위한 네임스페이스 추가

public class TextManager : DestroySingleton<TextManager>
{
	public GameObject talkTextUI; 
	public TextMeshProUGUI talkText; 
	private StringBuilder _tempTextBuilder;
	private string _totalText;
	private CancellationTokenSource _tempSource = new CancellationTokenSource();
	public bool isOverTextRoutine = false; //텍스트출력이 되고있는지의 bool변수 

	void Start()
	{
		talkTextUI.SetActive(false); //텍스트는 초기에 비활성화.
		_tempTextBuilder = new StringBuilder();
	}
	
	public void DisplayTextSlowly(string text) //기본적인 텍스트 출력함수
	{
		talkTextUI.SetActive(true);
		DisplayText(text).Forget();
	}

	private async UniTaskVoid DisplayText(string message) // 텍스트출력 코루틴
	{
		_totalText = message;
		_tempTextBuilder.Clear(); //텍스트 초기화(비워주고)
		_tempSource = new CancellationTokenSource();
		
		for (int i = 0; i < message.Length; i++)
		{
			_tempTextBuilder.Append(message[i]); // 문자 하나씩 StringBuilder에 추가
			talkText.text = _tempTextBuilder.ToString(); // 현재까지의 내용을 출력
			await UniTask.Delay(TimeSpan.FromSeconds(0.06),cancellationToken: _tempSource.Token); // 0.1f에 맞춰 텍스트 출력
		}
		isOverTextRoutine = true; //다출력됨.
	}

}