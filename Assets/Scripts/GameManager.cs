using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    bool won = false;
    bool lose = false;
    public static int deadCount = 0;
    public static int gameRounds = 0;
    public static List<int> BasicList = new List<int>();
    public static List<int> SpecialList = new List<int>();
    public GameObject player;
    private PlayerController playerController;
    public GameObject endGameMenuUI;
    public GameObject winText;
    public GameObject loseText;

    public void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        endGameMenuUI.SetActive(false);
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    public void WinGame()
    {
        if (!won)
        {
            won = true;
            BasicList.Insert(gameRounds, playerController.getBasicCount());
            SpecialList.Insert(gameRounds, playerController.getSpecialCount());
            endGameMenuUI.SetActive(true);
            winText.SetActive(true);
            Time.timeScale = 0f;
            ++gameRounds;
        }
    }

    public void LoseGame()
    {
        if (!lose)
        {
            lose = true;
            BasicList.Insert(gameRounds, playerController.getBasicCount());
            SpecialList.Insert(gameRounds, playerController.getSpecialCount());
            endGameMenuUI.SetActive(true);
            loseText.SetActive(true);
            Time.timeScale = 0f;
            ++deadCount;
            ++gameRounds;
        }
    }

    public int getTotalDeaths()
    {
        return deadCount;
    }

    public int getTotalRounds()
    {
        return gameRounds;
    }

    public int getBasicAt(int i)
    {
        return BasicList[i];
    }

    public int getSpecialAt(int i)
    {
        return SpecialList[i];
    }
}
