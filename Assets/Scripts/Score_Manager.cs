using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;  // Use only one variable
    public int score = 0;
    public int highScore;

    void Start()
    {
        if (scoreText == null) 
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();  
        }
        StartCoroutine(Score());
        //highScore=0;
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if(score>highScore)
        {
            highScore=score;
            Debug.Log("HighScore : " + highScore);
        }
    }

    IEnumerator Score()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            score++;
            //Debug.Log("Score : " + score);
        }
    }
}
