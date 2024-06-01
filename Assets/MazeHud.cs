using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class MazeHud : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreTxt, MoneyText, TimerText, healthText;
    [SerializeField] public PlayableDirector pd, pd1;
    public float timer = 150.0f;
    [SerializeField] public int health = 100;
    public float powerupTimer = 0f;
    public FirstPersonMovement fpm;
    public bool powerState = false;
    public AudioSource asrc;
    public bool finished = false;

    void Start()
    {
        asrc = this.GetComponent<AudioSource>();

    }

    void Update()
    {

        if (finished != true)
        {
            scoreTxt.text = "Score: " + MoneyAndScore.score;
            MoneyText.text = "Money: " + MoneyAndScore.money;
            timer -= Time.deltaTime;
            healthText.text = "Health: " + health;
            TimerText.text = "Time Left: " + ((int)timer);
        }

        if (powerState)
        {
            fpm.runSpeed = 13;
            fpm.speed = 9;
        }
        else {
            fpm.runSpeed = 9;
            fpm.speed = 5;
        }
        if (timer <= 0.0f)
        {
            timerEnded();
        }
        if (health <= 0) {
            timerEnded();
        }
        if (powerupTimer >= 0) { 
            powerupTimer -= Time.deltaTime;
            powerState = true;
        }
        if (powerupTimer <= 0) { 
            powerState = false;
        }

    }
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("cop") && finished != true)
        {
            health -= 20;
            MoneyAndScore.score -= 100;
            MoneyAndScore.money = MoneyAndScore.money * 0.8;
        }
        if (collider.gameObject.CompareTag("stateLine"))
        {
            finished = true;
            scoreTxt.text = "";
            MoneyText.text = "";
            TimerText.text = "";
            healthText.text = "";
            TimerText.text = "";
            asrc.Stop();
            StartCoroutine(Cutscene());

        }
        if (collider.gameObject.CompareTag("speed")) {
            powerupTimer = 30f;
            collider.gameObject.SetActive(false);
        }

    }
    IEnumerator Cutscene()
    {
        pd.Play();
        yield return new WaitForPlayableDirector(pd);
        StartCoroutine(Cutscene2());
    }

    IEnumerator Cutscene2()
    {
        pd1.Play();
        yield return new WaitForPlayableDirector(pd1);
        SceneManager.LoadScene("Office");

    }

    
    void timerEnded()
    {
        TimerText.text = "Time Left: 0";
        SceneManager.LoadScene("GameOverScene");
    }
}
public class WaitForPlayableDirector : CustomYieldInstruction
{
    private PlayableDirector _playableDirector;

    public WaitForPlayableDirector(PlayableDirector playableDirector)
    {
        _playableDirector = playableDirector;
    }

    public override bool keepWaiting
    {
        get { return _playableDirector.state == PlayState.Playing; }
    }
}
