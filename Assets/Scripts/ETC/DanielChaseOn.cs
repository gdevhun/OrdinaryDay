using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DanielChaseOn : MonoBehaviour
{
    private Transform _thisTrans;
    private readonly float _duration = 2f; 
    private readonly Vector3 _targetPosition = new Vector3(-430.9f, -208.3f, -3.5f);
    private readonly Quaternion _targetRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    private void Awake()
    {
        {
            _thisTrans = GetComponent<Transform>();
        } 
    }

    public void OnChase()
    {
        _thisTrans.rotation = _targetRotation;
        Debug.Log(_thisTrans.rotation);
        MoveOverTime(_targetPosition,_duration).Forget();
    }
    private async UniTaskVoid MoveOverTime(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = _thisTrans.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 현재 시간에 대한 비율을 계산하여 이동
            _thisTrans.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 한 프레임 대기
            await UniTask.Yield();
        }

        // 최종 위치 설정
        _thisTrans.position = targetPosition;
    }
}
