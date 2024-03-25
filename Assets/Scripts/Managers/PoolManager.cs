using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 생성할 프리팹 타입 -> 키로 사용
public enum ObjType
{
    문여는소리,
    문닫는소리
}

public class PoolManager : MonoBehaviour
{
    // 싱글톤
    public static PoolManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            // 풀링매니저 할당
            instance = this;

            // 씬전환시 파괴되지 않게
            DontDestroyOnLoad(instance);

            // (타입, 프리팹) 맵핑
            Map();

            // (타입, 큐) 맵핑
            Gen();
        }
        else
        {
            // 씬전환시 이미 있으면 파괴
            Destroy(gameObject);
        }
    }

    // 생성할 프리팹 -> 인스펙터에서 할당
    public GameObject openDoorSound;
    public GameObject closeDoorSound;

    // (타입, 프리팹) 맵핑
    public Dictionary<ObjType, GameObject> genPref = new Dictionary<ObjType, GameObject>();

    // (타입, 큐) 맵핑
    private Dictionary<ObjType, Queue<GameObject>> poolPref = new Dictionary<ObjType, Queue<GameObject>>();

    // (타입, 프리팹) 맵핑
    private void Map()
    {
        genPref.Add(ObjType.문여는소리, openDoorSound);
        genPref.Add(ObjType.문닫는소리, closeDoorSound);
    }

    // (타입, 큐) 맵핑
    private void Gen(int count = 50)
    {
        // 정의된 타입을 하나씩 가져와서
        foreach (ObjType type in Enum.GetValues(typeof(ObjType)))
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            GameObject prefab = genPref[type];

            // count 개 생성하고 비활성화 후 큐에 저장
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            // (타입, 큐) 맵핑
            poolPref.Add(type, queue);
        }
    }

    // 풀에서 꺼냄
    public GameObject GetObj(ObjType type)
    {
        // 키가 존재하고 큐에 오브젝트가 있으면 꺼냄
        if (poolPref.ContainsKey(type) && poolPref[type].Count > 0)
        {
            GameObject obj = poolPref[type].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // 사용 가능한 오브젝트가 없는 경우
        return null;
    }

    // 풀에 다시 반환함
    public void ReturnObj(GameObject obj, ObjType type)
    {
        // 비활성화
        obj.SetActive(false);

        // 해당 타입의 풀로 반환
        poolPref[type].Enqueue(obj);
    }
}
