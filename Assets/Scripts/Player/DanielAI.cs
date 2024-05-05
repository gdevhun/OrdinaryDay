using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

public class DanielAI : MonoBehaviour
{
    public NavMeshAgent nav; // 네비메쉬
    [SerializeField] private FirstPlayer firstPlayer; // 플레이어
    [SerializeField] private float killDis; // 플레이어를 죽일 수 있는 거리
    private Vector3 _playerDir; // 플레이어로부터의 방향
    private float _playerDis; // 플레이어와 거리
    public GameObject danielRunSound; // 다니엘 발 소리
    [SerializeField] private Animator _animator; // 애니메이터
    private bool isDead; // 플레이어가 죽었는지 체크
    [SerializeField] private GameObject killerView; // 킬러뷰
    [SerializeField] private Transform lookPos; // 죽을 때 볼 위치
    public GameObject chasedSFX; // 긴장감 있는 효과음

    // 플레이어로부터의 방향과 거리 계산 후 추적
    private void Update()
    {
        // 플레이어가 죽었는지 체크
        if(isDead) return;

        // 죽일 수 있는 거리보다 멀면 쫓아옴
        if(CalcDis())
        {
            nav.SetDestination(firstPlayer.transform.position);
            _animator.SetBool("isChase", true);
            return;
        }

        // 죽일 수 있는 거리가 되면
        // 1.플레이어 죽고 페이드
        // 2.다니엘 : 서 있는 상태, 발 소리 비활성화
        // 3.플레이어 : 누운 상태, 못 움직임, 발 소리 비활성화, 다니엘 바라보기
        // 4.다니엘이 톱으로 플레이어 찌르기
        // 5.킬러뷰 활성화
        // 6.플레이어 쓰러짐
        // 7.페이드 후 베드엔딩
        PlayerDead().Forget();
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

    // 플레이어 죽음
    private async UniTaskVoid PlayerDead()
    {
        // 1.플레이어 죽고 페이드
        isDead = true;
        FadeManager.Instance.Fade(2f);

        // 2.다니엘 : 서 있는 상태, 발 소리 비활성화
        nav.SetDestination(transform.position);
        _animator.SetBool("isChase", false);
        danielRunSound.SetActive(false);
        
        // 3.플레이어 : 다니엘 바라보기, 못 움직임, 발 소리 비활성화
        firstPlayer.transform.LookAt(lookPos);
        firstPlayer.isFade = true;
        firstPlayer.GetComponent<PlayerStep>().playerRunSound.SetActive(false);
        firstPlayer.GetComponent<PlayerStep>().playerWalkSound.SetActive(false);

        // 페이드 끝날 때 까지 대기
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));

        // 4.다니엘이 톱으로 플레이어 찌르기
        _animator.SetTrigger("DoAttack");

        // 5.킬러뷰 활성화
        killerView.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        SoundManager.Instance.SFXPlay(SfxType.DanielAtk1);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        SoundManager.Instance.SFXPlay(SfxType.PeterHit1);

        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));

        _animator.SetTrigger("DoAttack");
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        SoundManager.Instance.SFXPlay(SfxType.DanielAtk1);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        SoundManager.Instance.SFXPlay(SfxType.PeterHit2); 

        // 6.플레이어 쓰러짐
        float elapsed = 0f;
        float duration = 2f;
        Quaternion startRot = firstPlayer.transform.rotation;
        Quaternion endRot = Quaternion.Euler(-90f, firstPlayer.transform.rotation.eulerAngles.y, firstPlayer.transform.rotation.eulerAngles.z);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float time = Mathf.Clamp01(elapsed / duration);
            firstPlayer.transform.rotation = Quaternion.Slerp(startRot, endRot, time);
            await UniTask.Yield();
        }

        chasedSFX.SetActive(false);

        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        SoundManager.Instance.SFXPlay(SfxType.PeterHit1);

        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        SoundManager.Instance.SFXPlay(SfxType.PeterHit2);

        // 7.페이드 후 베드엔딩
        FadeManager.Instance.Fade(2f);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
    }
}
