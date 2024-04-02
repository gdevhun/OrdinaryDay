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

	public void OnSoundValueChanged()
	{
		//최적화를 위해서 getcomponet 대신 tryget 이벤트호출러로 코드 작성
		if (!TryGetComponent(out Slider slider))
		{   
			return;  //예오처리
		}

		switch (slider.value)  //value값에 따른 이미지 변경
		{
			case 0:
				curImage.sprite = SoundImages[0];
				break;
			case float value when value > 0 && value < 0.25f:
				curImage.sprite = SoundImages[1];
				break;
			case float value when value >= 0.25f && value < 0.5f:
				curImage.sprite = SoundImages[2];
				break;
			case 1:
				curImage.sprite = SoundImages[3];
				break;
		}

	}
}
