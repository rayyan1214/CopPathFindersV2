using UnityEngine;
using UnityEngine.SceneManagement;


public class Avatar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collider)
    {

        if (collider.gameObject.CompareTag("win"))
        {
            Debug.Log("This works");
            SceneManager.LoadScene("VictoryScene");
        }

    }
}
