using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<float> wordPositionX = new List<float>();
    public List<float> wordPositionY = new List<float>();
    public List<string> theWords = new List<string>();

    public int score = 0;
    public int lives = 0;
    public float speed = 0f;
    public string name = "";
}
