using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    //SE���ĂԎ���
    //AudioManager.Instance.PlaySE(SEName.�Ăт���SE�̖��O);�ŌĂׂ܂�
    //BGM�����l��
    //AudioManager.Instance.PlayBGM(BGMName.�Ăт���SE�̖��O);�ŌĂׂ܂�

    [SerializeField]
    private AudioSource bgmAudioSource;

    [SerializeField]
    private AudioSource seAudioSource;

    private List<AudioSource> subSESources = new List<AudioSource>();

    //public float MasterVolume = 1;
    //public float BGMVolume = 0.5f;
    //public float SEVolume = 1;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {

    }

    public void PlayBGM(BGMName bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        //bgmAudioSource.volume = data.volume * BGMVolume * MasterVolume;
        bgmAudioSource.Play();
    }


    public void PlaySE(SEName se)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);

        seAudioSource.clip = data.audioClip;
        //seAudioSource.volume = data.volume * SEVolume * MasterVolume;
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
    //[Range(0, 1)]
    //public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public SEName se;
    public AudioClip audioClip;
    //[Range(0, 1)]
    //public float volume = 1;
}

//BGM,SE���ǉ�����΂�����TitleBGM�ȂǓ���Ă����Ƃ��̕�������BGM,SE�������܂�
public enum SEName
{
    Bom,
    GameClear,
    GameOver,
    Button,
    Shild,
    Get,
    Test,
    Shoot,
    BreakDebris,
}

public enum BGMName
{
    MainBgm,
    Test,
}
