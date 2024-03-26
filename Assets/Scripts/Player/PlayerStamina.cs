using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 스태미너 관리
public class PlayerStamina : MonoBehaviour
{
    // 스태미너
	private readonly float maxStamina = 100f;
	public float curStamina;

	// 플레이어
	private FirstPlayer player;

	private void Awake()
	{
        //현재스테미나 100으로 초기화
		curStamina = maxStamina;

		// 플레이어
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPlayer>();
	}

	private void Update()
	{
        // 스프린트를 하고 있다면 UI 업데이트
        if (player.isRun)
        {
            DecreaseStamina();
        }
        else
        {
            AutoIncreaseStamina();
        }
	}

	private void DecreaseStamina() //스프린트할때 감소하는 함수
	{
		if (curStamina >= 0 && (player.hAxis != 0 || player.vAxis != 0))
		{
			curStamina -= Time.deltaTime * 10; // 10초에 걸쳐서 스테미너가 0이 됨
		}
		else
		{
			StartCoroutine(InactiveSprint()); //스태미너가 0이됨. 달릴수없음.
		}
	}
	
	private void AutoIncreaseStamina()  //스프린트를 안할때 자동 회복함수
	{
		if (curStamina <= 100)
		{
			curStamina += Time.deltaTime * 10; // 10초에 걸쳐서 스테미너 회복
		}
	}

	private IEnumerator InactiveSprint()
	{   //일정시간동한 달리기를 못하게 되는 코루틴
		
		if (TryGetComponent(out FirstPlayer firstPlayer))
		{
			if (firstPlayer.runSpeed != 7f)
			{   //이 if문에들어온거라면 코루틴이 이미실행되고있음. 예외로 빠져나가줌.
				//이 코루틴은 1번만 실행되어야함.
				yield break;
			}
			firstPlayer.runSpeed = firstPlayer.walkSpeed;
			//스프린트스피트를 잠시 무브스피드와 같게 만들어놈. (shift를 눌러도 무브스피드로 이동하게됨)
		}
		yield return new WaitForSeconds(3f);
		//3초가 지나면 스프린트스피드를 원래속도로 복구시킴.(7f)
		firstPlayer.runSpeed = 7f;
	}
}
