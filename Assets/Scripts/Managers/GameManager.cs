using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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
	public GameObject menuPanel; // 메뉴
    public Slider bgmSlider; // 배경음 슬라이더
    public Slider sfxSlider; // 효과음 슬라이더
	private void Start()
	{
		Screen.SetResolution(1920, 1080, true);
        bgmSlider.onValueChanged.AddListener(SoundManager.instance.SetBgmVolume); // 배경음 조절 이벤트리스너 등록
        sfxSlider.onValueChanged.AddListener(SoundManager.instance.SetSfxVolume); // 효과음 조절 이벤트리스너 등록
	}

	//�޴��� ��Ʈ�ѷ�
	#region
	public void StartGame()
	{
		SceneManager.LoadScene("GameScene");
	}

	// 사운드 옵션
	public void SettingGame()
	{
		// 설정 활성화
		SettingPanel.gameObject.SetActive(true);

		// 메뉴 비활성화
		menuPanel.gameObject.SetActive(false);
	}
	public void ExitGame()
	{
		Application.Quit();
	}
	#endregion 

	// 옵션에서 메인으로
	public void OptionToMain()
	{
		// 메뉴 활성화
		menuPanel.gameObject.SetActive(true);

		// 설정 비활성화
		SettingPanel.gameObject.SetActive(false);
	}
}
