using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    //SEを呼ぶ時は
    //AudioManager.Instance.PlaySE(SEName.呼びたいSEの名前);で呼べます
    //BGMも同様に
    //AudioManager.Instance.PlayBGM(BGMName.呼びたいSEの名前);で呼べます

    [SerializeField]
    private AudioSource bgmAudioSource;

    [SerializeField]
    private AudioSource seAudioSource;

    private List<AudioSource> subSESources = new List<AudioSource>();

    public float MasterVolume = 1;
    public float BGMVolume = 0.5f;
    public float SEVolume = 1;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    protected override void Awake()
    {
        base.Awake();
        //基本一つのみなのでシーンを跨げるようにする
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //開始時にBGMを鳴らすやつ(タイトル画面用意)
        //PlayBGM(BGMName.Main);
    }

    public void PlayBGM(BGMName bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * BGMVolume * MasterVolume;
        bgmAudioSource.Play();
    }


    public void PlaySE(SEName se)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);

        seAudioSource.clip = data.audioClip;
        seAudioSource.volume = data.volume * SEVolume * MasterVolume;
        seAudioSource.PlayOneShot(seAudioSource.clip);
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void StopSE()
    {
        seAudioSource.Stop();
    }

    public void BGMFadeOut()
    {
        StartCoroutine(FadeOut(bgmAudioSource));
    }
    public void SEFadeOut()
    {
        StartCoroutine(FadeOut(seAudioSource));
    }

    IEnumerator FadeOut(AudioSource audioSource)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

}

[System.Serializable]
public class BGMSoundData
{
    public BGMName bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public SEName se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}


public enum SEName
{
    Count,
    PushButton,
    Finish,
    Miss,
    StartSentak,
    Hanten,
    Success,
    SukillSE,
    Wind,
    Timer,
    Denger,
    Bomu,
}

public enum BGMName
{
    Main,
    Title,
    Street,
    End,

}
