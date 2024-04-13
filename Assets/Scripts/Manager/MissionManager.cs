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

    void Start()
    {
        missionTextPanel.SetActive(false);

    }

    public void InitMissonText(string missionTxt)
    {
        missionTextPanel.SetActive(true);
        missionText.text = missionTxt;
    }

    public void HideMissionText()
    {
        missionTextPanel.SetActive(false);
    }

    
}

