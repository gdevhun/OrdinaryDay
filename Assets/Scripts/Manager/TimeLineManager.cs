using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public enum cutSceneType
{
    Pill , Password, DanielPastView, OscarDead
}
public class TimeLineManager : Singleton<TimeLineManager>
{
    public List<GameObject> cutSceneList = new List<GameObject>(); //컷신obj리스트
    public GameObject playerCam; //비활성화할 플레이어 가상캠
    public GameObject player;
    public PlayableDirector cutSceneDirector;
    
    public void OnCutSceneObj(cutSceneType cutSceneType) //컷신 활성화
    {
        player.GetComponent<FirstPlayer>().isFade = true;
        playerCam.SetActive(false);
        cutSceneList[(int)cutSceneType].SetActive(true);
        cutSceneDirector = cutSceneList[(int)cutSceneType].GetComponent<PlayableDirector>();
        cutSceneDirector.Play(); // 컷씬 재생 시작
    }
    public void OffCutSceneObj(cutSceneType cutSceneType) //컷신 비활성화
    {
        cutSceneList[(int)cutSceneType].SetActive(false);
        playerCam.SetActive(true);
        player.GetComponent<FirstPlayer>().isFade = false;
    }
}
