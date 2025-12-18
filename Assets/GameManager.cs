using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public GameObject retryButton;
    private bool isGameOver=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        return;
    }
    public void GameOver()
    {
        Debug.Log("gameoveråƒÇŒÇÍÇΩ");
        gameoverText.SetActive(true);
        retryButton.SetActive(true);
        isGameOver = true;
    }
    public void Retry()
    {
        Debug.Log("ÉäÉgÉâÉCÅI");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
