using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    static ScoreScript instance;
    public int score;
    public int highScore;
    public TextMeshProUGUI scoreCard;
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(transform.parent.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Score(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Score(int p){
        score+=p;
        if(score>highScore){
            highScore=score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        scoreCard.text="Score: "+score+"\nHigh Score: "+highScore;

    }
    public void ResetScore(){
        
        score=0;
        scoreCard.text="Score: "+score+"\nHigh Score: "+highScore;
    }
}
