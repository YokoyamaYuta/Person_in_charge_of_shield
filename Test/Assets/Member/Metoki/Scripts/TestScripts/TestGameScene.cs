using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameScene : MonoBehaviour
{

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneLoader.Instance.LoadScene("ClearScene");  // �Q�[���V�[�������[�h
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            AudioManager.Instance.PlaySE(SEName.Test);
        }
    }
}
