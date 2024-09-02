using System; 
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    [Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip audioClip; 
    }

    [Header("BGM Settings")]
    public Sound[] bgmList;
    private AudioSource bgmSource;
    
    [Header("SFX Settings")]
    public Sound[] sfxList;
    public int channels;  // 다량의 효과음을 낼 수 있도록 채널 개수 설정
    private AudioSource[] sfxSources;  // SFX용 AudioSource 풀
    private int channelIndex = 0;  // 현재 채널 인덱스

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        InitializeSound();
    }

    private void InitializeSound()
    {
        // BGM 플레이어 초기화
        GameObject bgmObject = new GameObject("BGM Player");
        bgmObject.transform.parent = transform;
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        // SFX 플레이어 풀 초기화
        GameObject sfxObject = new GameObject("SFX Player");
        sfxObject.transform.parent = transform;
        sfxSources = new AudioSource[channels];

        for (int i = 0; i < sfxList.Length; i++)
        {
            sfxSources[i] = sfxObject.AddComponent<AudioSource>();
            sfxSources[i].playOnAwake = false;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopBGM();

        foreach (var bgm in bgmList)
        {
            if (scene.name == bgm.soundName)
            {
                PlayBGM(bgm.soundName);
                break;
            }
        }
    }

    public void PlayBGM(string name)
    {
        Sound bgmSound = Array.Find(bgmList, x => x.soundName == name);

        if (bgmSound != null)
        {
            bgmSource.clip = bgmSound.audioClip;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning(name + " 이름의 BGM을 찾을 수 없습니다.");
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    public void PlaySFX(string name)
    {
        Sound sfxSound = Array.Find(sfxList, x => x.soundName == name);

        if (sfxSound == null)
        {
            Debug.LogWarning(name + " 이름의 SFX를 찾을 수 없습니다.");
        }
        else
        {
            // 사용 가능한 AudioSource 풀 중 하나를 사용하여 SFX 재생
            sfxSources[channelIndex].clip = sfxSound.audioClip;
            sfxSources[channelIndex].Play();

            // 다음 채널로 인덱스를 이동, 풀의 끝에 도달하면 처음으로 되돌아감
            channelIndex = (channelIndex + 1) % channels;
        }
    }

    public void StopAllSFX()
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.Stop();
        }
    }
}