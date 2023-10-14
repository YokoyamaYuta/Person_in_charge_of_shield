using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;

    void Awake()
    {
        CheckInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Å´à»â∫â¸ó«ó\íË
    public void TitleSceneChange()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void GameSceneChange()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameOverSceneChange()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void ClearSceneChange()
    {
        SceneManager.LoadScene("ClearScene");
    }

    public void TitleBackSceneChange()
    {
        SceneManager.LoadScene("TitleScene");
    }

    void CheckInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
