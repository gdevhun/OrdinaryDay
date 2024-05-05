using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class CreditPanel : MonoBehaviour
{
    public Collider selectTrigger;

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
        selectTrigger.gameObject.GetComponent<PlayerSelectionEvent>().escapeDoorCol.enabled = false;
        selectTrigger.gameObject.GetComponent<PlayerSelectionEvent>().isSelected=false;
        selectTrigger.gameObject.SetActive(true);
        selectTrigger.enabled = true;
        MissionManager.Instance.badEndingCredit.SetActive(false);
        SoundManager.Instance.BgmSoundPlay(BgmType.Oscar);
        MissionManager.Instance.interactionPanel.SetActive(true);
        FadeManager.Instance.Fade(2);
        FirstPlayer firstPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPlayer>();
        firstPlayer.isFade = false;
        firstPlayer.transform.position = selectTrigger.gameObject.transform.position;
        firstPlayer.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }
}
