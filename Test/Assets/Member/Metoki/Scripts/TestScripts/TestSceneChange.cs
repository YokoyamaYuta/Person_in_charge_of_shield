using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneChange : MonoBehaviour
{
    private void Start()
    {
        //開始時にBGMを鳴らすやつ(タイトル画面用意)
        AudioManager.Instance.PlayBGM(BGMName.Test);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneLoader.Instance.LoadScene("GameScene");  // ゲームシーンをロード
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            AudioManager.Instance.PlaySE(SEName.Test);
        }
    }
}
