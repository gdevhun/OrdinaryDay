using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{

	public GameObject SettingPanel; //세팅페널
	public GameObject menuPanel; // 메뉴페널
    public Slider bgmSlider; // 배경음 슬라이더
    public Slider sfxSlider; // 효과음 슬라이더
	private void Start()
	{
		Screen.SetResolution(1920, 1080, true);
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBgmVolume); // 배경음 조절 이벤트리스너 등록
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSfxVolume); // 효과음 조절 이벤트리스너 등록
	}

	//메뉴씬 UI 컨트롤러 부분
	
	public void StartGame()
	{
		SceneManager.LoadScene("LoadingScene");
	}

	// 사운드 옵션
	public void SettingGame()
	{
		// 설정 활성화
		SettingPanel.gameObject.SetActive(true);

		// 메뉴 비활성화
		menuPanel.gameObject.SetActive(false);
	}

	public void GoMenuScene()
	{
		SceneManager.LoadScene("MenuScene");
	}
	public void ExitGame()
	{
		Application.Quit();
	}
	

	// 옵션에서 메인으로
	public void OptionToMain()
	{
		// 메뉴 활성화
		menuPanel.gameObject.SetActive(true);

		// 설정 비활성화
		SettingPanel.gameObject.SetActive(false);
	}
}
