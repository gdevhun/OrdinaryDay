using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
	WaitForSeconds _waitsec=new WaitForSeconds(1.5f);
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

			if (op.progress >= 0.9f)
			{
				yield return _waitsec;
				op.allowSceneActivation = true;
				yield break;
			}
		}
	}
}