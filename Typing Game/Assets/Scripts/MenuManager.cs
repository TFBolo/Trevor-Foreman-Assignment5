using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public InputField inputField;
    public Dropdown dropDown;
    public Button newGame;

    public static string userName;
    public static float gameSpeed;

    public static List<float> wpx = new List<float>();
    public static List<float> wpy = new List<float>();
    public static List<string> tw = new List<string>();
    public static bool savedData = false;
    public static float savedSpeed;
    public static int savedScore;
    public static int savedLives;

    private void Start()
    {
        newGame.enabled = false;
        gameSpeed = 1f;
    }

    public void NameChange()
    {
        userName = inputField.text;
        newGame.enabled = true;
    }

    public void SpeedChange()
    {
        if (dropDown.value == 0)
        {
            gameSpeed = 0.5f;
        }
        else if (dropDown.value == 1)
        {
            gameSpeed = 0.75f;
        }
        else if (dropDown.value == 2)
        {
            gameSpeed = 1f;
        }
        else if (dropDown.value == 3)
        {
            gameSpeed = 1.25f;
        }
        else if (dropDown.value == 4)
        {
            gameSpeed = 1.5f;
        }
        Debug.Log(gameSpeed);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }
}
