using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 페이드 인/아웃
public class FadeManager : MonoBehaviour
{
    // 싱글톤
    private static FadeManager instance;
    public static FadeManager Instance => instance;
    public FadeManager()
    {
        instance = this;
    }

    public void Awake()
    {
        // 페이드 인/아웃 이미지 할당
        Invoke("SetFadeInOutImage", 0.5f);
    }

    public void SetFadeInOutImage()
    {
        // 페이드 인/아웃 이미지 할당
        fadeInOutImage = GameObject.FindGameObjectWithTag("FadeInOutImage");

        // 페이드 인/아웃 이미지 비활성화
        fadeInOutImage.SetActive(false);
    }

    // 페이드 인/아웃 이미지
    private GameObject fadeInOutImage;

    // 진행시간
    float time = 0f;

    // 진행시간 계산용
    float timeCalc = 2f;

    // 페이드 인/아웃 코루틴 실행
    public void Fade()
    {
        StartCoroutine(GoFadeInOut());
    }

    // 페이드 인/아웃 코루틴
    IEnumerator GoFadeInOut()
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
        yield return new WaitForSeconds(1f);

        // 페이드 아웃
        // 알파값이 0 초과일때
        while (alpha.a > 0f)
        {
            // 진행시간 증가
            time += Time.deltaTime / timeCalc;

            // 알파값 감소
            alpha.a = Mathf.Lerp(1, 0, time);

            // 알파값 대입
            fadeInOutImage.GetComponent<Image>().color = alpha;

            yield return null;
        }

        // 페이드 인/아웃 이미지 비활성화
        fadeInOutImage.gameObject.SetActive(false);

        yield return null;
    }
}
