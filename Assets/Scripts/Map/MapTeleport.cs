using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTeleport : MonoBehaviour
{
    //맵 이동 할 위치 -> 인스펙터에서 설정
    [Header ("이동 할 위치")] [Space (10f)]
    [SerializeField] private GameObject mapTeleportPos;

    //비활성화 할 맵 -> 인스펙터에서 설정
    [Header ("비활성화 맵")] [Space (10f)]
    [SerializeField] private GameObject deActiveMap;

    //활성화 할 맵 -> 인스펙터에서 설정
    [Header ("활성화 맵")] [Space (10f)]
    [SerializeField] private GameObject activeMap;
    
    // 플레이어
    [Header ("플레이어")] [Space (10f)]
    [SerializeField] private GameObject player;

    // 퍼스트 플레이어
    [SerializeField] private FirstPlayer firstPlayer;

    //플레이어 스텝
    [SerializeField] private PlayerStep playerStep;

    // DarkBG
    [Tooltip ("Prison 가림막")] [SerializeField] private GameObject darkBG;

    // 맵 텔레포트
    public void Teleport() { StartCoroutine(TeleportCoroutine()); }
    
    // 맵 텔레포트 코루틴
    private IEnumerator TeleportCoroutine()
    {
        // 이동할 맵 활성화
        activeMap.SetActive(true);

        // 플레이어 위치
        player.transform.position = mapTeleportPos.transform.position;

        // 플레이어 회전
        player.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

        // 플레이어 스텝 비활성화
        playerStep.playerRunSound.SetActive(false);
        playerStep.playerWalkSound.SetActive(false);

        // DarkBG 셋팅
        darkBG.SetActive(activeMap.name.Equals("Prison"));
        
        // 배경음 재생
        if(activeMap.name.Equals("Prison")) SoundManager.Instance.BgmSoundPlay(BgmType.PrisonEnter);

        // 페이드 인/아웃, 플레이어 못 움직임
        FadeManager.Instance.Fade(4f);
        firstPlayer.isFade = true;

        // 1초 후에 문 여는소리
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.SFXPlay(SfxType.ToPrisonDoorOpen);

        // 1초 후에 움직일수있음
         yield return new WaitForSeconds(3f);
        firstPlayer.isFade = false;

        // 3초 후에
        yield return new WaitForSeconds(0.5f);
        deActiveMap.SetActive(false);
    }
}
