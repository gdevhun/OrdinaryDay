using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
	[SerializeField] private AudioSource bgmSound; // 배경음 오디오
	[SerializeField] private AudioClip[] bgmList, sfxList; // 배경음 및 효과음 리스트
	private Dictionary<BgmType, AudioClip> mapBgm = new Dictionary<BgmType, AudioClip>(); // (타입, 배경음) 맵핑
	private Dictionary<SfxType, AudioClip> mapSfx = new Dictionary<SfxType, AudioClip>(); // (타입, 효과음) 맵핑
	[HideInInspector] public float bgmVolume, sfxVolume; // 배경음 볼륨 및 효과음 볼륨
    [SerializeField] private GameObject player; // 플레이어
    
	private void Awake()
    {
         // 볼륨 초기화
         bgmVolume = 0.1f;
         sfxVolume = 1f;

         // (타입, 배경음) 맵핑
         Map();

         // 배경음 재생
         BgmSoundPlay(BgmType.Curious);
    }

    // 테스트
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9)) SFXPlay(SfxType.OpenDoor, new Vector3(10f, 0f, 0f));
        if(Input.GetKeyDown(KeyCode.Alpha8)) SFXPlay(SfxType.OpenDoor);
    }
    
    // 배경음 및 효과음 맵핑
    private void Map()
    {
        // (타입, 배경음) 맵핑
        for(int i = 0; i < bgmList.Length; i++)
        {
            mapBgm.Add((BgmType)i, bgmList[i]);
        }

        // (타입, 효과음) 맵핑
        for(int i = 0; i < sfxList.Length; i++)
        {
            mapSfx.Add((SfxType)i, sfxList[i]);
        }
    }

    // 배경음
    public void BgmSoundPlay(BgmType bgmType)
    {
        // 음원
        AudioClip clip = mapBgm[bgmType];

        // 음원 할당
        bgmSound.clip = clip;

        // 음원 반복
        bgmSound.loop = true;

        // 음원 볼륨
        bgmSound.volume = bgmVolume;

        // 음원 재생
        bgmSound.Play();
    }

    // 효과음 => 거리 영향 X
    public void SFXPlay(SfxType type)
    {
        // 맵핑된 효과음 재생
        if (mapSfx.TryGetValue(type, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, sfxVolume);
        }
    }

    // 효과음 => 거리에 따라
    public void SFXPlay(SfxType type, Vector3 playPos)
    {
        // 맵핑된 효과음 재생
        if (mapSfx.TryGetValue(type, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, CalcPlayerDis(playPos, 20.0f) * sfxVolume);
        }
    }

    // 플레이어와 거리비 계산해서 리턴
    // maxDis : 사운드가 들릴 수 있는 최대거리
    private float CalcPlayerDis(Vector3 playPos, float maxDis)
    {
        return Mathf.Clamp(1.0f - (Vector3.Distance(playPos, player.transform.position) / maxDis), 0.0f, 1.0f);
    }

    // 배경음 볼륨 조절
    public void SetBgmVolume(float volume)
    {
        // 슬라이더 값에따라 볼륨 적용
        bgmSound.volume = volume;

        // 슬라이더 값을 변수에 저장해서 배경음악을 실행할때마다 볼륨을 지정
        bgmVolume = volume;
    }

    // 효과음 볼륨 조절
    public void SetSfxVolume(float volume)
    {
        // 슬라이더 값을 변수에 저장해서 효과음을 실행할때마다 볼륨을 지정
        sfxVolume = volume;
    }
}

// 재생할 배경음 타입 -> 키로 사용
public enum BgmType
{
	Curious,
	DarkHouse,
	Woods
}

// 재생할 효과음 타입 -> 키로 사용
public enum SfxType
{
	OpenDoor, CloseDoor,
	PickUpThing, DropThing,
    CheckClipBoard, PutClipBoard,
    PhoneFirstBell, PhoneSecondBell,
    GetFlashLight, OnOffLight,
    WaterSwitch,
    PickUpDropKey, UseKey, DoorLock,
    PickUpHammer, DropHammer, UseHammer, BrokenOscarDoor,
    SecretDoorOpen
}