using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource musicSource;
    public AudioSource effectSource;

    private string resourcePath = "AudioClips";

    private float musicVolume = 1f;

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = Mathf.Clamp(value, 0, 1);
        }
    }

    private float effectVolume = 1f;
    public float EffectVolume
    {
        get { return effectVolume; }
        set
        {
            effectVolume = Mathf.Clamp(value, 0, 1);
        }
    }

    protected override void Initial()
    {
        //生成播放音频的对象
        GameObject go = new GameObject("SoundManager");

        musicSource = go.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.loop = true;

        effectSource = go.AddComponent<AudioSource>();
        effectSource.playOnAwake = false;
        musicSource.loop = false;

        Object.DontDestroyOnLoad(go);
    }

    public void PlayMusic(string path,bool isLoop = true)
    {
        if (musicSource.clip.name == path)
            return;

        path = resourcePath + "/" + path;
        AudioClip clip = Resources.Load<AudioClip>(path);
        musicSource.clip = clip;
        musicSource.Play();
        musicSource.loop = isLoop;
    }

    public void PlayEffect(string path, bool isLoop = false)
    {
        if (effectSource.clip.name == path)
        {
            effectSource.Play();
            return;
        }

        path = resourcePath + "/" + path;
        AudioClip clip = Resources.Load<AudioClip>(path);
        effectSource.clip = clip;
        effectSource.Play();
        effectSource.loop = isLoop;
    }

    public void PlayOnShot(string path)
    {
        path = resourcePath + "/" + path;
        AudioClip clip = Resources.Load<AudioClip>(path);       //缓存起来 什么时候释放？（Unity内存机制）
        effectSource.PlayOneShot(clip);
    }
}
