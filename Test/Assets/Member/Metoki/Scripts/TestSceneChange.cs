using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneChange : MonoBehaviour
{
    public SceneManagement sceneManagement;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            sceneManagement.TitleSceneChange();
        }
    }
}
