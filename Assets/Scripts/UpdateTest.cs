using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UpdateTest : MonoBehaviour
{
	private readonly string _completePassword = "* * * * * * * * *";
	public TextMeshProUGUI password;
	private float _timeCalc = 0f;
	private float _waitTime = 0.2f;
	private int _curIndex = 0;
	private void Update()
	{
		_timeCalc += Time.deltaTime;
		if (_timeCalc >= _waitTime && _curIndex < _completePassword.Length)
		{
			password.text += _completePassword[_curIndex++];
			_timeCalc = 0f;
		}
	}
}