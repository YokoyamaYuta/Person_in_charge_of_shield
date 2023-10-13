using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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
