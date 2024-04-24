using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStep : MonoBehaviour
{
    // 플레이어
    [SerializeField] private FirstPlayer player;

    // 걷는 소리 및 뛰는 소리
    public GameObject playerWalkSound, playerRunSound;

    // 플레이어 스텝 소리
	private void Update() { SFXStep(); }
 
	private void SFXStep()
    {
		// 플레이어가 걷는 상태일 때
		playerWalkSound.SetActive(player.isWalk && !player.isFade);
        playerWalkSound.GetComponent<AudioSource>().volume = SoundManager.Instance.sfxVolume;

		// 플레이어가 뛰는 상태일 때
		playerRunSound.SetActive(player.isRun && !player.isFade);
        playerRunSound.GetComponent<AudioSource>().volume = SoundManager.Instance.sfxVolume;  
    }   
}
