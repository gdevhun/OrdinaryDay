using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CreditPanel : MonoBehaviour
{
    public void GoToMenuScene()
    {
        GameManager.Instance.GoMenuScene();
        Cursor.lockState = CursorLockMode.None;
    }
}
