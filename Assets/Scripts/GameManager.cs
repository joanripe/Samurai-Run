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
    public GameObject initialField;

    // interfaz y campos de la interfaz
    public Animator gameCanvas, menuAnim, coinAnim;
    public Text scoreText, coinText, modifierText, hiscoreText;
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

        hiscoreText.text = PlayerPrefs.GetInt("Hiscore").ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartGame();
            FindObjectOfType<SidePropsSpawner>().IsScrolling = true;    // empezar a mover la decoracion lateral
            FindObjectOfType<CameraMotor>().IsMoving = true;    //empezar a mover la camara
            gameCanvas.SetTrigger("Show");  // mostrar el menu del juego
            menuAnim.SetTrigger("Hide");    // ocultar el menu inicial
            initialField.SetActive(false);  // desactivar la decoracion de la pantalla inicial
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
        coinAnim.SetTrigger("Get");
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
        gameCanvas.SetTrigger("Hide");  // ocultar el menu de juego al morir
        FindObjectOfType<SidePropsSpawner>().IsScrolling = false;   // busca los objetos con el componente SidePropsSpawner y pone si variable a falso
        deadScoreText.text = score.ToString("0");   // obtener la puntuacion del gameMenu (int) y convertirla a string para postrarla en el menu de muerte
        deadCoinText.text = coinScore.ToString("0");    // obtener las monedas del gameMenu (int) y convertirlas a string para postrarla en el menu de muerte
        deathMenuAnim.SetTrigger("Death");  // activa la animacion de mostrar del menu de muerte

        // comprobar si hemos conseguido la maxima puntuacion
        if (score > PlayerPrefs.GetInt("Hiscore"))
        {
            float s = Mathf.Ceil(score); 
            PlayerPrefs.SetInt("Hiscore", (int)s);
        }
    }
}
