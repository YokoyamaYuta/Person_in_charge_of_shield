using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bgm_Se_Main : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            //開始時にBGMを鳴らすやつ
            AudioManager.Instance.PlayBGM(BGMName.MainBgm);

            //スライダーを動かした時の処理を登録
            bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
            seSlider.onValueChanged.AddListener(SetAudioMixerSE);
        }
        if (SceneManager.GetActiveScene().name == "GameOverscene")
        {
            AudioManager.Instance.PlaySE(SEName.GameOver);
        }
    }

    //BGM
    public void SetAudioMixerBGM(float value)
    {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("BGM", volume);
        Debug.Log($"BGM:{volume}");
    }

    //SE
    public void SetAudioMixerSE(float value)
    {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("SE", volume);
        Debug.Log($"SE:{volume}");
    }
}
