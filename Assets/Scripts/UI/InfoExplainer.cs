using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InfoExplainer : MonoBehaviour
{
	private static InfoExplainer instance = null;
	public static InfoExplainer Instance
	{   //�̱���
		get
		{
			if (instance == null)
			{
				instance = new InfoExplainer();
			}
			return instance;
		}
	}
	private readonly string infoText1 = "���� E�� ��ȣ�ۿ� �� �� �ֽ��ϴ�.";
	private readonly string infoText2 = "�������� R�� ��ȣ�ۿ� �� �� �ֽ��ϴ�.";

	private bool isInfoText1Over=false;
	private bool isInfoText2Over = false;

	private WaitForSeconds activeSec = new(2.5f);
	public GameObject InteractionPanel;
    public TextMeshProUGUI textGuide;
    
    void Start()
    {
        InteractionPanel.SetActive(false);
    }
    private IEnumerator ActiveText()
    {
		InteractionPanel.SetActive(true);
		textGuide.text = infoText1;
		yield return activeSec;
        
    }
    public void ActiveInfoText1()
    {
		if (!isInfoText1Over)
		{
			StartCoroutine(ActiveText());
			InteractionPanel.SetActive(false);
			isInfoText1Over = true;
		}
		return;
	}
}
