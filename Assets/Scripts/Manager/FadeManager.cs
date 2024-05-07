using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 페이드 인/아웃
public class FadeManager : DestroySingleton<FadeManager>
{
	// 페이드 인/아웃 이미지
	public GameObject fadeInOutImage;

	public bool isFadeOver;
	// 진행시간
	float time = 0f;
	
	protected override void Awake()
	{
        base.Awake();
		fadeInOutImage.SetActive(false);
	}

	private void Start()
	{
        Fade(1); //게임신 들어오면 자동 페이드
	}
	
    // 페이드 인/아웃 코루틴 실행
    public void Fade(float sec)
    {
	    isFadeOver = false;
        GoFadeInOut(sec).Forget();
    }

    // 페이드 인/아웃 코루틴
    private async UniTaskVoid GoFadeInOut(float sec)
    {
        // 페이드 인/아웃 이미지 활성화
        fadeInOutImage.gameObject.SetActive(true);

        // 진행시간 초기화
        time = 0f;

        // 알파값을 조절해서 페이드 인/아웃 이미지 컬러에 대입
        Color alpha = fadeInOutImage.GetComponent<Image>().color;

        // 알파값 최대
        alpha.a = 1f;

        // 알파값 대입
        fadeInOutImage.GetComponent<Image>().color = alpha;

        // 1초동안 알파값 최대 유지(흑색)
        await UniTask.Delay(TimeSpan.FromSeconds(sec));

        // 페이드 아웃
        // 알파값이 0 초과일때
        while (alpha.a > 0f)
        {
            // 진행시간 증가
            time += Time.deltaTime;

            // 알파값 감소
            alpha.a = Mathf.Lerp(1, 0, time);

            // 알파값 대입
            fadeInOutImage.GetComponent<Image>().color = alpha;

            await UniTask.Yield();
        }

        // 페이드 인/아웃 이미지 비활성화
        fadeInOutImage.gameObject.SetActive(false);

        await UniTask.Yield();
        isFadeOver = true;
    }
}
