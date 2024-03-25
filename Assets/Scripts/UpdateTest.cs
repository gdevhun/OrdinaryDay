using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTest : MonoBehaviour
{
	//업데이트 테스트용
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Return) && !TextManager.instance.isOverTextRoutine)
		{   // 엔터, 텍스 출력코루틴이 끝나지않았다면
			TextManager.instance.DisplayTextInstantly(); //함수호출로 텍스트 바로 렌더링
		}
		else if (Input.GetKeyUp(KeyCode.Return) && TextManager.instance.isOverTextRoutine)
		{   //엔터, 텍스 출력 코루틴이 끝난 상황이면
			TextManager.instance.isOverTextRoutine = false;
			TextManager.instance.talkTextUI.SetActive(false);  // 비활성화
		}
	}
}