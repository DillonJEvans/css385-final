using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    public SpawnManager spawnManager;
    public int maxGear;
    public int gearsCollected = 0;

    private float originalTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        startScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        // Pause the game.
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void Easy() 
    {
        startScreen.SetActive(false);
        maxGear = 5;
        spawnManager.StartSpawn();
        Time.timeScale = originalTimeScale;
    }

    public void Hard() 
    {
        startScreen.SetActive(false);
        maxGear = 10;
        spawnManager.StartSpawn();
        Time.timeScale = originalTimeScale;
    }   

    // Update is called once per frame
    void Update()
    {

    }

    public void Win()
    {
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
