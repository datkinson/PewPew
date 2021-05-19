using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public GameObject view;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        view.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            view.SetActive(true);
        }
        
    }

    public void Retry()
    {
        SceneManager.LoadScene("Spaaaaace");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
