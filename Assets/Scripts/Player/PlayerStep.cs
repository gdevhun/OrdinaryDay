using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStep : MonoBehaviour
{
    // 플레이어
    private FirstPlayer player;

    // 플레이어 스태미너
    private PlayerStamina playerStamina;

    // 걷는 소리 및 뛰는 소리
    public GameObject playerWalkSound, playerRunSound;

    // 플레이어 뛰는 숨소리
    public GameObject playerRunBreathSound;

    private void Awake()
    {
        // 플레이어
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPlayer>();

        // 플레이어 스태미너
        playerStamina = player.GetComponent<PlayerStamina>();

    }
	private void Update()
	{
		SFXStep(); // 스텝 소리
	}
 
	private void SFXStep()
    {
		// 플레이어가 걷는 상태일 때
		playerWalkSound.SetActive(player.isWalk);

		// 플레이어가 뛰는 상태일 때
		playerRunSound.SetActive(player.isRun);
		playerRunBreathSound.SetActive(playerRunSound.activeSelf && playerStamina.curStamina <= 15 && playerStamina.curStamina > 0);
    }
}
