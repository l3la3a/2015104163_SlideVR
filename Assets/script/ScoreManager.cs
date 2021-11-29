using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    int score;

    public static bool getScore;
    public static bool downScore;
    public static bool isFinish;

    public Text scoreText;
    public GameObject FinishObject;

    int count;
    public int timefps;

    void Start()
    {
        score = 0;    // 해킹위험 보안 꼭 거칠 ,,,, 것
        count = 0;
    }

    void Update()
    {

        count++;
        if (count % timefps==0) {
            score++;
        }
        
        if (getScore) {
            score+=2000;
            getScore = false;
        }

        if (downScore) {
            score -= 1000;
            downScore = false;
        }

        if (isFinish) {

            FinishObject.SetActive(true);
            isFinish = false;
        }

        scoreText.text = score.ToString();

    }
}
