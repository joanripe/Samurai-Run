using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private const int COIN_SCORE_AMOUNT = 5;

    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }
    private bool isGameStarted = false;
    private PlayerMotor motor;

    // interfaz y campos de la interfaz
    public Text scoreText, coinText, modifierText;
    public float score, coinScore, modifierScore;
    private int lastScore;

    // menu de muerte
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        UpdateScores();
        coinText.text = coinScore.ToString("0");
        scoreText.text = score.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartGame();
            FindObjectOfType<SidePropsSpawner>().IsScrolling = true;
        }

        if (isGameStarted && !IsDead)
        {
            //aumento de la puntuacion
            score += (Time.deltaTime * modifierScore);
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
            
        }
    }

    public void GetCoin()
    {
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");
    }

    void UpdateScores()
    {
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void UpdateModifier (float _num)
    {
        modifierScore = 1.0f + _num;
        UpdateScores();
    }

    public void OnPlayBtn()
    {
        Debug.Log("error");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
    public void OnDeath()
    {
        IsDead = true;
        FindObjectOfType<SidePropsSpawner>().IsScrolling = false;
        deadScoreText.text = score.ToString("0");
        deadCoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Death");
    }
}
