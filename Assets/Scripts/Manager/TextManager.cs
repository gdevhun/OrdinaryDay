using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Text;
using System.Threading; // StringBuilder를 사용하기 위한 네임스페이스 추가

public class TextManager : Singleton<TextManager>
{
	
	public GameObject talkTextUI; 
	public TextMeshProUGUI talkText; 
	private StringBuilder _tempTextBuilder;
	private string _totalText;
	private CancellationTokenSource _tempSource = new CancellationTokenSource();
	//private List<CancellationTokenSource> _sourceList = new List<CancellationTokenSource>();
	public bool isOverTextRoutine = false; //텍스트출력이 되고있는지의 bool변수
	

	void Start()
	{
		talkTextUI.SetActive(false); //텍스트는 초기에 비활성화.
		_tempTextBuilder = new StringBuilder();
	}

	/*public void DisplayTextSlowly(string text)
	{
		talkTextUI.SetActive(true);
		
	}*/
	public void DisplayTextSlowly(string text) //기본적인 텍스트 출력함수
	{
		talkTextUI.SetActive(true);
		DisplayText(text).Forget();
	}
	public void DisplayTextInstantly()  //엔터누르면실행->즉시 텍스트 출력 함수
	{
		/*isOverTextRoutine = true;
		foreach (var _tempSource in _sourceList)
		{
			_tempSource.Cancel();
		}*/
		_tempSource.Cancel();
		talkText.text = _totalText; //텍스트 바로출력
		isOverTextRoutine = true;
	}
	private async UniTaskVoid DisplayText(string message) // 텍스트출력 코루틴
	{
		_totalText = message;
		Debug.Log(_totalText);
		_tempTextBuilder.Clear(); //텍스트 초기화(비워주고)
		_tempSource = new CancellationTokenSource();
		//_sourceList.Add(_tempSource);
		
		for (int i = 0; i < message.Length; i++)
		{
			_tempTextBuilder.Append(message[i]); // 문자 하나씩 StringBuilder에 추가
			talkText.text = _tempTextBuilder.ToString(); // 현재까지의 내용을 출력
			//Debug.Log(message[i]);
			await UniTask.Delay(TimeSpan.FromSeconds(0.1),cancellationToken: _tempSource.Token); // 0.1f에 맞춰 텍스트 출력
		}
		isOverTextRoutine = true; //다출력됨.
		Debug.Log("!!!");
	}

}