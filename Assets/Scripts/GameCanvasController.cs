using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    public GameObject gameOverView;
    public GameObject gateView;
    public GameObject player;
    private string gateText;
    // Start is called before the first frame update
    void Start()
    {
        gameOverView.SetActive(false);
        gateView.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
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

    public void EnterGate(float points)
    {
        gateView.SetActive(true);
        if (string.IsNullOrEmpty(gateText))
        {
            gateText = GameObject.Find("Gate Text").GetComponentInChildren<Text>().text;
        }
        GameObject.Find("Gate Text").GetComponentInChildren<Text>().text = gateText + "\nParts collected: " + points;
        Time.timeScale = 0;
    }
}
