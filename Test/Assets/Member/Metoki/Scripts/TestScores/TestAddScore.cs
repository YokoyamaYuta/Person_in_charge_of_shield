using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAddScore : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    public void Start()
    {
        int score = TestScore.score;
        scoreText.text = "Score" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
