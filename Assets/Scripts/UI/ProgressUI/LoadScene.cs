using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // GameScene으로 이동하는 코루틴
    private Coroutine _movingNextSceneCoroutine;

    private void Start()
    {
        // 이전에 실행 중인 코루틴이 있으면 중단
        if (_movingNextSceneCoroutine != null)
        {
            Debug.Log("실행 중인 코루틴 있음");
            StopCoroutine(_movingNextSceneCoroutine);
        }

        Debug.Log("코루틴 실행");
        _movingNextSceneCoroutine = StartCoroutine(MovingNextScene());
    }

    IEnumerator MovingNextScene()
    {
        Debug.Log("MovingNextScene");

        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("GameScene");

        Debug.Log("코루틴 초기화");
        //  씬 로드 후 코루틴 참조를 초기화
        _movingNextSceneCoroutine = null;
    }
}