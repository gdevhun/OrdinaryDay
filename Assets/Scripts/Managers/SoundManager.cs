using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    OpenDoor,
    CloseDoor
}

public class SoundManager : MonoBehaviour
{
    // 싱글톤
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            // 사운드매니저 할당
            instance = this;

            // 씬전환시 파괴되지 않게
            DontDestroyOnLoad(instance);

            // 볼륨 초기화
            bgmVolume = 0.1f;
            sfxVolume = 1f;

            // (타입, 배경음) 맵핑
            Map();

            // 배경음 재생
            BgmSoundPlay(BgmType.Curious);
        }
        else
        {
            // 씬전환시 이미 있으면 파괴
            Destroy(gameObject);
        }
    }

    public AudioSource bgmSound; // 배경음 오디오
    public AudioClip[] bgmList; // 배경음 리스트
    public AudioClip[] sfxList; // 효과음 리스트
    public Dictionary<BgmType, AudioClip> mapBgm = new Dictionary<BgmType, AudioClip>(); // (타입, 배경음) 맵핑
    public Dictionary<SfxType, AudioClip> mapSfx = new Dictionary<SfxType, AudioClip>(); // (타입, 효과음) 맵핑
    private float bgmVolume; // 배경음 볼륨값 저장
    private float sfxVolume; // 효과음 볼륨값 저장
    
    // 배경음 및 효과음 맵핑
    private void Map()
    {
        // (타입, 배경음) 맵핑
        mapBgm.Add(BgmType.Curious, bgmList[0]);
        mapBgm.Add(BgmType.DarkHouse, bgmList[1]);
        mapBgm.Add(BgmType.Woods, bgmList[2]);

        // (타입, 효과음) 맵핑
        mapSfx.Add(SfxType.OpenDoor, sfxList[0]);
        mapSfx.Add(SfxType.CloseDoor, sfxList[1]);
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

    // 효과음
    public void SFXPlay(SfxType type)
    {
        // 맵핑된 효과음 재생
        if (mapSfx.TryGetValue(type, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, sfxVolume);
        }
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
