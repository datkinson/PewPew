using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button backButton;
    public Button playButton;
    public Button controlsButton;
    public Button controlsBox;
    public Button descriptionBox;

    private void Start()
    {
        backButton.gameObject.SetActive(false);
        controlsBox.gameObject.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Spaaaaace");
    }

    public void Controls()
    {
        descriptionBox.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        controlsButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        controlsBox.gameObject.SetActive(true);
    }

    public void Back()
    {
        descriptionBox.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        controlsBox.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("mudkipz", 5);
    }
}
