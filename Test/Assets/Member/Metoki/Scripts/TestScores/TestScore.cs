using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestScore : MonoBehaviour
{
    public static int score = 0;

     void Start()
    {
        score = 0;
    }

    public void AddScore(int points)
    {
        score += points;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
