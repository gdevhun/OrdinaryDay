using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
	private void Start()
	{
		OnLoadGameScene().Forget();
	}
	
	private async UniTaskVoid OnLoadGameScene()
	{
		await UniTask.Yield();
		//진짜 로딩
		AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("GameScene");
		loadSceneAsync.allowSceneActivation = false;

		while (!loadSceneAsync.isDone)
		{   //페이크 로딩
			await UniTask.Yield();
			/*if (loadSceneAsync.progress < 0.95f) // 로딩 진행률이 0.95 미만인 경우
			{
				//continue; //기존은 프로그레스바 게이지를 채움.
			}
			else // 로딩 진행률이 0.95 이상인 경우
			{
				// 로딩이 거의 완료된 상태를 2.5초간 유지
				await UniTask.Delay(TimeSpan.FromSeconds(2.5));

				// 로딩 활성화
				loadSceneAsync.allowSceneActivation = true;
			}*/
			if (loadSceneAsync.progress >= 0.9f) // 로딩 진행률이 0.9 이상인 경우
			{
				// 로딩이 거의 완료된 상태를 2.5초간 유지
				await UniTask.Delay(TimeSpan.FromSeconds(2.5));

				// 로딩 활성화
				loadSceneAsync.allowSceneActivation = true;
			}
		}
	}
}