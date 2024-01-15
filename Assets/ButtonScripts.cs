using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    [SerializeField] public Button startBtn, exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void game_exit()
    {
        Application.Quit();
    }
    public void gameRs()
    {
        MoneyAndScore.score = 0;
        MoneyAndScore.money = 0;
        SceneManager.LoadScene("GameScene");
    }
}
