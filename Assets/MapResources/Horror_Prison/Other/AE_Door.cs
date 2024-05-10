using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AE_Door : MonoBehaviour
{

    bool trig, open;//trig-проверка входа выхода в триггер(игрок должен быть с тегом Player) open-закрыть и открыть дверь
    public float smooth = 2.0f;//скорость вращения
    public float DoorOpenAngle = 87.0f;//угол вращения 
    private Vector3 defaulRot;
    private Vector3 openRot;
    public Text txt;//text 
    public TMP_Text interactionText; // 상호작용 텍스트
    public bool isControl; // ControlRoomDoor인지 체크
    public bool isOscar; // OscarRoomDoor인지 체크
    [SerializeField] private bool isIronCage; // 철창문인지 체크
    // Start is called before the first frame update
    void Start()
    {
        defaulRot = transform.eulerAngles;
        openRot = new Vector3(defaulRot.x, defaulRot.y + DoorOpenAngle, defaulRot.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (open)//открыть
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
        }
        else//закрыть
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaulRot, Time.deltaTime * smooth);
        }
        if (Input.GetKeyDown(KeyCode.E) && trig && !isOscar)
        {
            open = !open;

            if(open) SoundManager.Instance.SFXPlay(isIronCage ? SfxType.IronCageDoorOpen : SfxType.IronDoorOpen);
            else SoundManager.Instance.SFXPlay(isIronCage ? SfxType.IronCageDoorClose : SfxType.IronDoorOpen);
        }
        if (trig)
        {
            if (open)
            {
                txt.text = "Close E";
            }
            else
            {
                txt.text = "Open E";
            }
        }
    }
    private void OnTriggerEnter(Collider coll)//вход и выход в\из  триггера 
    {
        if (coll.tag == "Player")
        {
            if (!open)
            {
                txt.text = "Close E ";
                // 텍스트
                interactionText.text = "E키로 문과 상호작용이 가능하다.";

                if(isControl)
                {
                    interactionText.text = "열쇠가 필요하다.";
                    SoundManager.Instance.SFXPlay(SfxType.DoorLock);
                }

                if(isOscar)
                {
                    interactionText.text = "부술 수 있는게 필요하다.";
                    SoundManager.Instance.SFXPlay(SfxType.DoorLock);
                }
            }
            else
            {
                txt.text = "Open E";
            }
            trig = true;
        }
    }
    private void OnTriggerExit(Collider coll)//вход и выход в\из  триггера 
    {
        if (coll.tag == "Player")
        {
            txt.text = " ";
            trig = false;
            
            // 텍스트
            interactionText.text = "";
        }
    }
}
