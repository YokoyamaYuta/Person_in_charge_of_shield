using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //呼び出し方等
    //呼び出す際、該当コードにusing UnityEngine.SceneManagementは入れなくても普通に作動します。
    //Sceneを呼び出すときは
    //SceneLoader.Instance.LoadScene("シーンの名前");で呼べます。
    private static SceneLoader instance;
    public List<string> sceneNames = new List<string>();

    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SceneManager");
                    instance = singletonObject.AddComponent<SceneLoader>();
                }
            }
            return instance;
        }
    }

    //シーンを跨いでも機能を残しつつ、同じものが遷移先にあったら消去
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    //シーンのリスト
    public void LoadScene(string sceneName)
    {
        if (sceneNames.Contains(sceneName))
        {
           SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene '" + sceneName + "' is not in the sceneNames list.");
        }
    }
}
