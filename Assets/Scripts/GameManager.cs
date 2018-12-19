using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { set; get; }

    private bool isGameStarted = false;
    private PlayerMotor motor;

    // interfaz y campos de la interfaz
    public Text scoreText, coinText, modifierText;
    public float score, coinScore, modifierScore;

    private void Awake()
    {
        Instance = this;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
 //       UpdateScores();
    }

    // Update is called once per frame
    void Update()
    {
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartGame();
        }
    }

    void UpdateScores()
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
        modifierText.text = modifierScore.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
