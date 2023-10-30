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
            //�J�n����BGM��炷���
            AudioManager.Instance.PlayBGM(BGMName.MainBgm);

            //�X���C�_�[�𓮂��������̏�����o�^
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
        //5�i�K�␳
        value /= 5;
        //-80~0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("BGM", volume);
        Debug.Log($"BGM:{volume}");
    }

    //SE
    public void SetAudioMixerSE(float value)
    {
        //5�i�K�␳
        value /= 5;
        //-80~0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("SE", volume);
        Debug.Log($"SE:{volume}");
    }
}
