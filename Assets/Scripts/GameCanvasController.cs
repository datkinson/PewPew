using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    public GameObject gameOverView;
    public GameObject gateView;
    public GameObject mineView;
    public Text shipPartsText;
    public Text healthText;
    public Text shieldsText;
    public GameObject player;
    public Text fuseTimerText;
    public Text detectionRangeText;
    private string gateText;
    // Start is called before the first frame update
    void Start()
    {
        gameOverView.SetActive(false);
        gateView.SetActive(false);
        mineView.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        fuseTimerText.text = "Fuse Timer: " + player.GetComponent<WeaponControl>().fuseTimer;
        detectionRangeText.text = "Detection Range: " + player.GetComponent<WeaponControl>().detectionRange;
        shipPartsText.text = "Ship Parts: " + player.GetComponent<InventoryController>().shipParts;
        healthText.text = "Health: " + player.GetComponent<DamageManager>().hitPoints;
        shieldsText.text = "Shields: " + player.GetComponent<DamageManager>().shieldPoints;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Cancel") > 0)
        {
            mineView.SetActive(true);
            Time.timeScale = 0;
        }

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
        mineView.SetActive(false);
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
