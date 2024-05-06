using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerSelectionEvent : MonoBehaviour
{
    [SerializeField] private GameObject playerSelectionPanel;
    [SerializeField] private GameObject player;
    [SerializeField] private DanielAI danielAI; // 다니엘
    [SerializeField] private GameObject block; // 플레이어 이동 막기
    public BoxCollider escapeDoorCol; // 탈출 문
    public bool isSelected;
    private Collider _collider;
    private void Awake()
    {
        playerSelectionPanel.SetActive(false);
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) &&!isSelected)
        {
            isSelected = true;
            playerSelectionPanel.SetActive(false);
            escapeDoorCol.enabled = true;
            StartCutScene().Forget();
            //프리즌으로 들어감. 컷신실행
        }
        else if (Input.GetKeyDown(KeyCode.N) &&!isSelected)
        {
            isSelected = true;
            MissionManager.Instance.normalEndingCredit.SetActive(true);
            //노말엔딩 페널 진행
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerSelectionPanel.SetActive(true);
        player.GetComponent<FirstPlayer>().isFade = true;
    }

    public async UniTask StartCutScene()
    {
        _collider.enabled = false;
        FadeManager.Instance.Fade(1f);
        TimeLineManager.Instance.OnCutSceneObj(cutSceneType.DanielChase);
        await UniTask.WaitUntil(() => TimeLineManager.Instance.cutSceneDirector.state != PlayState.Playing);
        FadeManager.Instance.Fade(2f);
        TimeLineManager.Instance.OffCutSceneObj(cutSceneType.DanielChase);
        player.transform.position=new Vector3(-430.61f,-208.5f,5.34f);
        player.transform.rotation= Quaternion.Euler(0,181.99f,0);
        player.GetComponent<FirstPlayer>().isFade = false;
        //도망가는 상황으로 이동하기.
        //peter위치 이동하기.

        // 2초 후에 다니엘 활성화
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        danielAI.gameObject.SetActive(true);
        danielAI.chasedSFX.SetActive(true);
        block.SetActive(true);
        SoundManager.Instance.BgmSoundPlay(BgmType.DanielChase);
    }
}
