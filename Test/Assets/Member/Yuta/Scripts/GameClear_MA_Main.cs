using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear_MA_Main : MonoBehaviour
{
    GameObject Score_text;
    Text score_text;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySE(SEName.GameClear);

        //GameManagerでのゲージメーターの値を貰う
        score = GameManger_Main.Finish_Score();

        Score_text = GameObject.FindGameObjectWithTag("Score");
        score_text = Score_text.GetComponent<Text>();

        score_text.text = score.ToString();
    }
}
