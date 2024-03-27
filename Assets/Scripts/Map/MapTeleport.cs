using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTeleport : MonoBehaviour
{
    //맵 이동 할 위치 -> 인스펙터에서 설정
    public GameObject mapTeleportPos;

    //비활성화 할 맵 -> 인스펙터에서 설정
    public GameObject deActiveMap;

    //활성화 할 맵 -> 인스펙터에서 설정
    public GameObject activeMap;

    // 플레이어
    public GameObject player;

    // 퍼스트 플레이어
    public FirstPlayer firstPlayer;

    //플레이어 스텝
    public PlayerStep playerStep;

    // 맵 텔레포트
    public void Teleport()
    {
        StartCoroutine(TeleportCoroutine());
    }
    
    // 맵 텔레포트 코루틴
    IEnumerator TeleportCoroutine()
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
        playerStep.playerRunBreathSound.SetActive(false);

        // 페이드 인/아웃, 플레이어 못 움직임, 문 여는소리
        FadeManager.Instance.Fade();
        firstPlayer.isFade = true;
        SoundManager.instance.SFXPlay(SfxType.OpenDoor);

        // 1초 후에 문 닫는소리
        yield return new WaitForSeconds(1f);
        SoundManager.instance.SFXPlay(SfxType.CloseDoor);

        // 1초 후에 움직일수있음
         yield return new WaitForSeconds(1.5f);
        firstPlayer.isFade = false;

        // 3초 후에
        yield return new WaitForSeconds(0.5f);
        deActiveMap.SetActive(false);
    }
}
