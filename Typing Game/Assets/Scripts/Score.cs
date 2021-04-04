using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;
    public static int lives;

    public GameObject panel;

    private void Start()
    {
        score = 0;
        lives = 5;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            panel.transform.SetAsLastSibling();
        }
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }
}
