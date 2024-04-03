using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgessUI : MonoBehaviour
{
    // 원 이미지 애니메이션
    public Animator anim;

    // 원 이미지
    [SerializeField]
    private RawImage _uiRawImage;

    private void Awake()
    {
        LoadLoadingImg();
		
	}

    // 원 이미지 로드
    public void LoadLoadingImg()
    {
        anim.SetBool("IsRotating", true);
    }
}