using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DanielAI : MonoBehaviour
{
    public NavMeshAgent nav; // 네비메쉬
    [SerializeField] private FirstPlayer firstPlayer; // 플레이어
    [SerializeField] private float killDis; // 플레이어를 죽일 수 있는 거리
    private Vector3 _playerDir; // 플레이어로부터의 방향
    private float _playerDis; // 플레이어와 거리
    public GameObject danielRunSound; // 다니엘 발 소리
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // 플레이어로부터의 방향과 거리 계산 후 추적
    private void Update()
    {
        nav.SetDestination(CalcDis() ? firstPlayer.transform.position : transform.position);
        if (CalcDis())
        {
            _animator.SetBool("isAttack",true);
        }
    }

    // 플레이어로부터의 방향과 거리 계산
    private bool CalcDis()
    {
        // 방향 거리 계산
        _playerDir = firstPlayer.transform.position - transform.position;
        _playerDis = _playerDir.magnitude;

        // 죽일 수 있는 거리보다 멀면 true
        return _playerDis > killDis ? true : false;
    }
}
