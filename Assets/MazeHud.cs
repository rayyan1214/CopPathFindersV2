using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MazeHud : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreTxt, MoneyText, TimerText, healthText;
    public float timer = 150.0f;
    [SerializeField] public int health = 100;


    void Start()
    {
        

    }

    void Update()
    {
        scoreTxt.text = "Score: " + MoneyAndScore.score;
        MoneyText.text = "Money: " + MoneyAndScore.money;
        timer -= Time.deltaTime;
        healthText.text = "Health: " + health;
        TimerText.text = "Time Left: " + ((int)timer);
        if (timer <= 0.0f)
        {
            timerEnded();
        }
        if (health <= 0) {
            timerEnded();
        }

    }
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("cop"))
        {
            health -= 20;
            MoneyAndScore.score -= 100;
            MoneyAndScore.money = MoneyAndScore.money * 0.8;
        }
        if (collider.gameObject.CompareTag("stateLine"))
        {
            SceneManager.LoadScene("VictoryScene");
        }

    }
    void timerEnded()
    {
        TimerText.text = "Time Left: 0";
        SceneManager.LoadScene("GameOverScene");
    }
}
