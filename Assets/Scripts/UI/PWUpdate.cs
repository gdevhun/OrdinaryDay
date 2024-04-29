using System;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class PWUpdate : MonoBehaviour
{
	private readonly string _completePassword = "* * * * * * * * *";
	public TextMeshProUGUI password;
	private float _timeCalc = 0f;
	private readonly float _waitTime = 0.2f;
	private int _curIndex = 0;
	private bool _isOneSound;

	private void Update()
	{
		_timeCalc += Time.deltaTime;
		if (_timeCalc >= _waitTime && _curIndex < _completePassword.Length)
		{
			password.text += _completePassword[_curIndex++];
			_timeCalc = 0f;
		}

		if (_curIndex == _completePassword.Length && !_isOneSound)
		{
			_isOneSound = true;
			PlayDoorOpenSfx().Forget();
		}
	}

	private async UniTask PlayDoorOpenSfx()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(1.5));
		SoundManager.Instance.SFXPlay(SfxType.SecretDoorOpen);
	}
}