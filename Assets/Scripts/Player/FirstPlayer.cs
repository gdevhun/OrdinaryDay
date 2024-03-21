using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    // 플레이어 이동 관련
    public float moveSpeed = 5f; // 이동속도
    private float hAxis; // x축 이동값
    private float vAxis; // z축 이동값
    private Vector3 moveDir; // 이동방향

    // 마우스 회전 관련
    public float mouseSensitivity = 100f; // 마우스 감도
    private float xRot = 0f; // x축 회전값
    private float mouseX; // 마우스 회전 축값

    void Awake()
    {
        // 마우스 커서가 게임화면을 벗어나지않도록 잠금
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 입력
        GetInput();

        // 플레이어 이동
        Move();

        // 마우스 회전
        MouseRot();
    }

    // 입력
    void GetInput()
    {
        // 플레이어 이동
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        // 마우스 회전
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    }

    // 플레이어 이동
    void Move()
    {
        // 카메라가 바라보는 방향으로 대각선 정규화
        moveDir = (Camera.main.transform.forward * vAxis + Camera.main.transform.right * hAxis).normalized;

        // 플레이어 이동
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    // 마우스 회전
    void MouseRot()
    {
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
