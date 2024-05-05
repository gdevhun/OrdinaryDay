using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : Singleton<MissionManager>
{
    public GameObject normalEndingCredit;
    public GameObject trueEndingCredit;
    public GameObject badEndingCredit;
    
    
    public List<string> missionTextList;
    public GameObject missionTextPanel;
    public TextMeshProUGUI missionText;
    public GameObject waterLevelMissionTrigger;
    public GameObject pillMissionTrigger;
    
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
            waterLevelMissionTrigger.gameObject.SetActive(true);
        }
    }

    public void CheckWaterLevelMission()
    {
        if (curCnt == completeCnt)
        {
            pillMissionTrigger.gameObject.SetActive(true);
        }
    }
}

