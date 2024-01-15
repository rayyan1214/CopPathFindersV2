using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class MoneyAndScore : MonoBehaviour
{
    public static int score = 0;
    public static double money = 0;
    public static int capacity = 0;
    public static int carryingCapacity = 2250;
    public static bool keyFound = false;
    [SerializeField] public TextMeshProUGUI scoreTxt, capacityTxt;
    [SerializeField] public AudioSource audioSource; 
    [SerializeField] public AudioClip audioClip;


    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = audioClip;

    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = "Score: " + score;
        capacityTxt.text = "Capacity: " + capacity + "/" + carryingCapacity;
        
    }
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("1000bill") && capacity < carryingCapacity)
        {
            collider.gameObject.SetActive(false);
            score += 10;
            money += 1000;
            capacity += 10;
            audioSource.Play();
        }
        if (collider.gameObject.CompareTag("1kbill") && capacity < carryingCapacity)
        {
            collider.gameObject.SetActive(false);
            score += 15;
            money += 10000;
            capacity += 50;
            audioSource.Play();
        }
        if (collider.gameObject.CompareTag("MoneyBag") && capacity < carryingCapacity) {
            collider.gameObject.SetActive(false);
            score += 25;
            money += 100000;
            capacity += 100;
            audioSource.Play();
        }
        if (collider.gameObject.CompareTag("gold") && capacity < carryingCapacity) {
            collider.gameObject.SetActive(false);
            score += 75;
            money += 500000;
            capacity += 250;
            audioSource.Play();
        }
        if (collider.gameObject.CompareTag("key"))
        {
            collider.gameObject.SetActive(false);
            keyFound = true;
            score += 175;
            carryingCapacity += 950;
            audioSource.Play();
        }
        if (collider.gameObject.CompareTag("door") && keyFound) {
            collider.gameObject.SetActive(false);
            audioSource.Play();
        }
        if (collider.gameObject.CompareTag("exit"))
        {
            SceneManager.LoadScene("MazeScene");
        }
        
    }
}
