using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetAnim : MonoBehaviour
{
	public List<Sprite> SoundImages = new();
	public Image curImage;
	void Start()
	{
		curImage.GetComponent<Image>().sprite = SoundImages[3];
	}

	public void OnSoundValueChanged(string str)
	{
		float coef = 1;

		if(str.Equals("bgm"))  
		{
			coef = 10;
		}

		//����ȭ�� ���ؼ� getcomponet ��� tryget �̺�Ʈȣ�ⷯ�� �ڵ� �ۼ�
		if (!TryGetComponent(out Slider slider))
		{
			return;  //����ó��
		}

		switch (slider.value)  //value���� ���� �̹��� ����
		{
			case 0:
				curImage.sprite = SoundImages[0];
				break;
			case float value when value > 0 && value < (0.25f / coef):
				curImage.sprite = SoundImages[1];
				break;
			case float value when value >= (0.25f / coef) && value < (0.5f / coef):
				curImage.sprite = SoundImages[2];
				break;
			case float value when value >= (0.9f / coef):
				curImage.sprite = SoundImages[3];
				break;
		}

	}
	
}
