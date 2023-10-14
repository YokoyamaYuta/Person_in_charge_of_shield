using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneChange : MonoBehaviour
{
    private void Start()
    {
        //�J�n����BGM��炷���(�^�C�g����ʗp��)
        AudioManager.Instance.PlayBGM(BGMName.Test);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneLoader.Instance.LoadScene("GameScene");  // �Q�[���V�[�������[�h
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            AudioManager.Instance.PlaySE(SEName.Test);
        }
    }
}
