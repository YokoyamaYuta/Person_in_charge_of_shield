using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TestPrassScore : MonoBehaviour
{
    public int scoreValue = 10;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        int score = TestScore.score;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            TestScore.score += scoreValue;
            scoreText.text = "Score" + TestScore.score.ToString();
        }
        
    }
}
