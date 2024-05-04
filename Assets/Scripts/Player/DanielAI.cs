using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DanielAI : MonoBehaviour
{
    public NavMeshAgent nav; // 네비메쉬
    [SerializeField] private FirstPlayer firstPlayer; // 플레이어
    [SerializeField] private float killDis; // 플레이어를 죽일 수 있는 거리
    private Vector3 playerDir; // 플레이어로부터의 방향
    private float playerDis; // 플레이어와 거리
    public GameObject danielRunSound; // 다니엘 발 소리

    private void Update()
    {
        // 플레이어로부터의 방향과 거리 계산 후 추적
        if(CalcDis()) nav.SetDestination(firstPlayer.transform.position);
        else nav.SetDestination(transform.position);
    }

    // 플레이어로부터의 방향과 거리 계산
    private bool CalcDis()
    {
        // 방향 거리 계산
        playerDir = firstPlayer.transform.position - transform.position;
        playerDis = playerDir.magnitude;

        // 죽일 수 있는 거리보다 멀면 true
        return playerDis > killDis ? true : false;
    }
}
