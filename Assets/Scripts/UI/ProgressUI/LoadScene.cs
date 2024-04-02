using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // GameScene���� �̵��ϴ� �ڷ�ƾ
    private Coroutine _movingNextSceneCoroutine;

    private void Start()
    {
        // ������ ���� ���� �ڷ�ƾ�� ������ �ߴ�
        if (_movingNextSceneCoroutine != null)
        {
            Debug.Log("���� ���� �ڷ�ƾ ����");
            StopCoroutine(_movingNextSceneCoroutine);
        }

        Debug.Log("�ڷ�ƾ ����");
        _movingNextSceneCoroutine = StartCoroutine(MovingNextScene());
    }

    IEnumerator MovingNextScene()
    {
        Debug.Log("MovingNextScene");

        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("GameScene");

        Debug.Log("�ڷ�ƾ �ʱ�ȭ");
        //  �� �ε� �� �ڷ�ƾ ������ �ʱ�ȭ
        _movingNextSceneCoroutine = null;
    }
}