using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDeActive : MonoBehaviour
{
    AudioSource audioSource;

    // 오브젝트 타입
    public ObjType type;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 효과음 재생이 끝나면 풀에 반환
        if (!audioSource.isPlaying)
        {
            PoolManager.instance.ReturnObj(gameObject, type);
        }
    }
}

