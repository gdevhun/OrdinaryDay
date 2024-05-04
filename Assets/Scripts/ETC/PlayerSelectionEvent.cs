using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerSelectionEvent : MonoBehaviour
{
    [SerializeField] private GameObject PlayerSelectionPanel;
    private bool _isSelected;
    [SerializeField] private GameObject Player;
    [SerializeField] private DanielAI danielAI; // 다니엘
    private void Awake()
    {
        PlayerSelectionPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) &&!_isSelected)
        {
            _isSelected = true;
            PlayerSelectionPanel.SetActive(false);
            StartCutScene().Forget();
            //프리즌으로 들어감. 컷신실행
        }
        else if (Input.GetKeyDown(KeyCode.N) &&!_isSelected)
        {
            _isSelected = true;
            
            //노말엔딩 페널 진행
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerSelectionPanel.SetActive(true);
    }

    public async UniTask StartCutScene()
    {
        FadeManager.Instance.Fade(1f);
        TimeLineManager.Instance.OnCutSceneObj(cutSceneType.DanielChase);
        await UniTask.WaitUntil(() => TimeLineManager.Instance.cutSceneDirector.state != PlayState.Playing);
        FadeManager.Instance.Fade(2f);
        TimeLineManager.Instance.OffCutSceneObj(cutSceneType.DanielChase);
        Player.transform.position=new Vector3(-430.61f,-208.5f,5.34f);
       Player.transform.rotation= Quaternion.Euler(0,181.99f,0);
        //도망가는 상황으로 이동하기.
        //peter위치 이동하기.

        // 2초 후에 다니엘 활성화
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        danielAI.gameObject.SetActive(true);
    }
}
