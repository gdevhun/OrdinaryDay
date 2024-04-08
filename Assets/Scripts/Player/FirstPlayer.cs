using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    // 플레이어 이동 관련
    public float walkSpeed = 1f; // 걷는속도
    public float runSpeed = 1.5f; // 뛰는속도
    public float hAxis; // x축 이동값
    public float vAxis; // z축 이동값
    private Vector3 moveDir; // 이동방향
    public bool isWalk; // 걷고있는지 체크
    public bool isRun; // 달리고있는지 체크
    public LayerMask wallMask; // 벽 체크
    public bool isFade; // 페이드중인지 체크 (플레이어입력값안받는불변수)

    // 마우스 회전 관련
    public float mouseSensitivity = 100f; // 마우스 감도
    private float xRot = 0f; // x축 회전값
    private float mouseX; // 마우스 좌우 축값
    private float mouseY; // 마우스 상하 축값
    CinemachineVirtualCamera playerCamera; // 플레이어 카메라

	void Awake()
    {
        // 마우스 커서가 게임화면을 벗어나지않도록 잠금
        Cursor.lockState = CursorLockMode.Locked;

        // 플레이어 카메라
        playerCamera = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // 입력
        GetInput();

        // 플레이어 이동
        Move();

        // 플레이어 회전
        Rot();
    }

    // 입력
    void GetInput()
    {
        // 플레이어 이동
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        isRun = Input.GetKey(KeyCode.LeftShift) && (hAxis != 0 || vAxis != 0); // shift 누른상태에서 이동중이면 뛰는상태 
        isWalk = !isRun && (hAxis != 0 || vAxis != 0); // 뛰는중이 아닌데 이동중이면 걷는상태

        // 플레이어 회전
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }

    // 플레이어 이동
    void Move()
    {
        // 카메라가 바라보는 방향으로 대각선 정규화
        moveDir = (playerCamera.transform.forward * vAxis + playerCamera.transform.right * hAxis).normalized;

        // 벽 체크
        if(WallCheck()) return;

        // 페이드 체크
        if(isFade) return;

        // 플레이어 이동
        transform.position += isRun ? moveDir * runSpeed * Time.deltaTime : moveDir * walkSpeed * Time.deltaTime;
    }

	// 플레이어 회전
	void Rot()
    {
        // 페이드 체크
        if(isFade) return;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    // 벽 체크
    bool WallCheck()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.5f; // 레이 시작지점

        RaycastHit hit; // 레이 충돌정보

        if (Physics.Raycast(rayStart, moveDir, out hit, 1.3f, wallMask))
        {
            // 레이 디버깅용
            Debug.DrawRay(rayStart, moveDir * hit.distance, Color.red);

            return true;
        }

        // 레이 디버깅용
        Debug.DrawRay(rayStart, moveDir * 1.3f, Color.green);

        return false;
    }
}
