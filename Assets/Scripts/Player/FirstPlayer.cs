using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    // 플레이어 이동 관련
    [Header ("플레이어 이동")] [Space (10f)]
    [SerializeField] private float walkSpeed; // 걷는속도
    [SerializeField] private float runSpeed; // 뛰는속도
    [HideInInspector] public float hAxis; // x축 이동값
    [HideInInspector] public float vAxis; // z축 이동값
    private Vector3 _moveDir; // 이동방향
    [HideInInspector] public bool isWalk; // 걷고있는지 체크
    [HideInInspector] public bool isRun; // 달리고있는지 체크
    [SerializeField] private LayerMask wallMask; // 벽 체크
    [HideInInspector] public bool isFade; // 페이드중인지 체크 (플레이어입력값안받는불변수)

    // 마우스 회전 관련
    [Header ("플레이어 회전")] [Space (10f)]
    [SerializeField] private float mouseSensitivity; // 마우스 감도
    private float _xRot; // x축 회전값
    private float _mouseX; // 마우스 좌우 축값
    private float _mouseY; // 마우스 상하 축값
    [SerializeField] private CinemachineVirtualCamera _playerCamera; // 플레이어 카메라

    public DanielAI danielAI; // 다니엘

    // 마우스 커서가 게임화면을 벗어나지않도록 잠금
	private void Awake() { Cursor.lockState = CursorLockMode.Locked; }

    private void Update()
    {
        // 입력
        GetInput();

        // 플레이어 이동
        Move();

        // 플레이어 회전
        Rot();
    }

    // 입력
    private void GetInput()
    {
        // 플레이어 이동
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        isRun = Input.GetKey(KeyCode.LeftShift) && (hAxis != 0 || vAxis != 0); // shift 누른상태에서 이동중이면 뛰는상태 
        isWalk = !isRun && (hAxis != 0 || vAxis != 0); // 뛰는중이 아닌데 이동중이면 걷는상태

        // 플레이어 회전
        _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }

    // 플레이어 이동
    private void Move()
    {
        // 카메라가 바라보는 수평 평면 상의 방향으로 설정 => 오브젝트와 충돌 시 아래로 내려가는 이슈 픽스
        _moveDir = (Vector3.ProjectOnPlane(_playerCamera.transform.forward, Vector3.up).normalized * vAxis
        + Vector3.ProjectOnPlane(_playerCamera.transform.right, Vector3.up).normalized * hAxis).normalized;

        // 벽 체크
        if(WallCheck()) return;

        // 페이드 체크
        if(isFade) return;

        // 플레이어 이동
        transform.position += isRun ? _moveDir * runSpeed * Time.deltaTime : _moveDir * walkSpeed * Time.deltaTime;
    }

	// 플레이어 회전
	private void Rot()
    {
        // 페이드 체크
        if(isFade) return;

        _xRot -= _mouseY;
        _xRot = Mathf.Clamp(_xRot, -40f, 40f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
        transform.Rotate(Vector3.up * _mouseX);
    }

    // 벽 체크
    private bool WallCheck()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.5f; // 레이 시작지점

        RaycastHit hit; // 레이 충돌정보

        if (Physics.Raycast(rayStart, _moveDir, out hit, 1.3f, wallMask))
        {
            // 레이 디버깅용
            Debug.DrawRay(rayStart, _moveDir * hit.distance, Color.red);

            return true;
        }

        // 레이 디버깅용
        Debug.DrawRay(rayStart, _moveDir * 2f, Color.green);

        return false;
    }
}
