using UnityEngine;
using Cursor = UnityEngine.Cursor;
using UnityEngine.SceneManagement;

public class CreditPanel : MonoBehaviour
{
    //public Collider selectTrigger;
    private void OnEnable()
    {
        SoundManager.Instance.BgmSoundPlay(BgmType.CreditBGM);
    }

    public void GoToMenuScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SoundManager.Instance.BgmSoundPlay(BgmType.Lab);
        GameManager.Instance.GoMenuScene();
    }
    public void GoToPeterView()
    {
        SoundManager.Instance.BgmSoundPlay(BgmType.Oscar);
        SceneManager.LoadScene("PeterViewScene");
    }
}
