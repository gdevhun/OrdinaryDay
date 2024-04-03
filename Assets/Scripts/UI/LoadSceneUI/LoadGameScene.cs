using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(OnLoadGameScene());
	}
	
	IEnumerator OnLoadGameScene()
	{
		yield return null;
		AsyncOperation op = SceneManager.LoadSceneAsync("GameScene");
		op.allowSceneActivation = false;

		while (!op.isDone)
		{
			yield return null;

			if (op.progress < 0.9f)
			{
				
			}
			else
			{
				op.allowSceneActivation = true;
				yield break;
				
			}
		}
	}
}