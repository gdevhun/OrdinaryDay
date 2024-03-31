using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	private void Awake()
	{
		if (instance == null)
		{
			// �ؽ�Ʈ �޴��� �̱���
			instance = this;
			// �ı�X
			DontDestroyOnLoad(instance);
		}
		else
		{
			// ����ó��
			Destroy(gameObject);
		}
	}
	public GameObject SettingPanel; //�������ų ��� Ȱ��ȭ�� ������Ʈ.
	private void Start()
	{
		Screen.SetResolution(1920, 1080, true);
	}

	//�޴��� ��Ʈ�ѷ�
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
