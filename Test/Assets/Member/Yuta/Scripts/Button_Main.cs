using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Main : MonoBehaviour
{
    [SerializeField] GameObject SettingsCanvas;

    public void Title_Button()
    {
        // ゲームシーンをロード
        SceneLoader.Instance.LoadScene("SynopsisScene");
        AudioManager.Instance.PlaySE(SEName.Button);
    }

    public void SettingsButton()
    {
        SettingsCanvas.SetActive(true);
        AudioManager.Instance.PlaySE(SEName.Button);
    }

    public void BatuButton()
    {
        SettingsCanvas.SetActive(false);
        AudioManager.Instance.PlaySE(SEName.Button);
    }

    public void Synopsis_Button()
    {
        SceneLoader.Instance.LoadScene("TutorialScene");
        AudioManager.Instance.PlaySE(SEName.Button);
    }

    public void Tutorial_Button()
    {
        SceneLoader.Instance.LoadScene("GameScene");
        AudioManager.Instance.PlaySE(SEName.Button);
    }

    public void Clear_GameOver_Button()
    {
        SceneLoader.Instance.LoadScene("TitleScene");
        AudioManager.Instance.PlaySE(SEName.Button);
    }
}
