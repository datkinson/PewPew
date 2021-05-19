using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvasController : MonoBehaviour
{
    public GameObject gameOverView;
    public GameObject gateView;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameOverView.SetActive(false);
        gateView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            gameOverView.SetActive(true);
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

    public void Continue()
    {
        Time.timeScale = 1;
        gateView.SetActive(false);
    }

    public void EnterGate()
    {
        gateView.SetActive(true);
        Time.timeScale = 0;
    }
}
