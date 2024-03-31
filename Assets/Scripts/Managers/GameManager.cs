using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
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
			// 예외처리
			Destroy(gameObject);
		}
	}
	public GameObject SettingPanel; //세팅페널킬 경우 활성화할 오브젝트.
	private void Start()
	{
		Screen.SetResolution(1920, 1080, true);
	}

	//메뉴신 컨트롤러
	#region
	public void StartGame()
	{
		SceneManager.LoadScene("GameScene");
	}
	public void SettingGame()
	{

	}
	public void ExitGame()
	{
		Application.Quit();
	}
	#endregion 
}
