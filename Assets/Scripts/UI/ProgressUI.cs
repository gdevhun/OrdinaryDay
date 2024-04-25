using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgessUI : MonoBehaviour
{
    // �� �̹��� �ִϸ��̼�
    public Animator anim;

    // �� �̹���
    [SerializeField]
    private RawImage _uiRawImage;

    private void Awake()
    {
        LoadLoadingImg();
		
	}

    // �� �̹��� �ε�
    public void LoadLoadingImg()
    {
        anim.SetBool("IsRotating", true);
    }
}