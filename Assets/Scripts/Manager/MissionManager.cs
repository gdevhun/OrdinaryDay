using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : Singleton<MissionManager>
{
    public List<string> missionTextList;
    public GameObject missionTextPanel;
    public TextMeshProUGUI missionText;
    public GameObject waterLevelMissonTrigger;
    public GameObject pillMissonTrigger;
    
    public int curCnt;
    public int completeCnt;
    void Start()
    {
        missionTextPanel.SetActive(false);

    }

    public void DisplayMissonText(string missionTxt)
    {
        missionTextPanel.SetActive(true);
        missionText.text = missionTxt;
    }

    public void HideMissionText()
    {
        missionText.text = "";
        missionTextPanel.SetActive(false);
    }

    public void CheckTrashMission()
    {
        if (curCnt == completeCnt)
        {
            waterLevelMissonTrigger.gameObject.SetActive(true);
        }
    }

    public void CheckWaterLevelMission()
    {
        if (curCnt == completeCnt)
        {
            pillMissonTrigger.gameObject.SetActive(true);
        }
    }
}

