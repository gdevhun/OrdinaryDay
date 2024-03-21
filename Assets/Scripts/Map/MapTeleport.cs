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

        // 플레이어 위치 이동
        player.transform.position = mapTeleportPos.transform.position;

        // 페이드 인/아웃
        FadeManager.Instance.Fade();
        
        // 문여는소리
        PoolManager.instance.GetObj(ObjType.문여는소리);

        // 1초 후에
         yield return new WaitForSeconds(1f);

        // 문닫는소리
        PoolManager.instance.GetObj(ObjType.문닫는소리);

        // 3초 후에
        yield return new WaitForSeconds(3.0f);

        //이전 맵 비활성화
        deActiveMap.SetActive(false);
    }
}
