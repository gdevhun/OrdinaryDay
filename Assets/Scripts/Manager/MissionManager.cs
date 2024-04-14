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
        missionTextPanel.SetActive(false);
    }

    
}

