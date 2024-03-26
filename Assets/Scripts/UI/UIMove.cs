using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UImove : MonoBehaviour
{
	public Image uiImage;
	public float moveSpeed = 10f;
	private readonly float minY = -360f;
	private readonly float maxY = -330f;

	void Update()
	{
		// ui엔터 스킵 이미지 이동 업뎃
		Vector2 currentPosition = uiImage.rectTransform.anchoredPosition;

		currentPosition.y -= moveSpeed * Time.deltaTime;

		uiImage.rectTransform.anchoredPosition = currentPosition;

		if (currentPosition.y <= minY)
		{
			currentPosition.y = maxY;
			uiImage.rectTransform.anchoredPosition = currentPosition;
		}
	}
}