using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    public Score scoreControls;
    public WordManager wordManager;

    public Text scoreText;
    public Text livesText;

    public static string json;

    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private AudioSource myAudio;

    public void Awake()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
            toggle.isOn = true;
            myAudio.enabled = true;
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                myAudio.enabled = false;
                toggle.isOn = false;
            }
            else
            {
                myAudio.enabled = true;
                toggle.isOn = true;
            }
        }
    }

    public void ToggleMusic()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("music", 1);
            myAudio.enabled = true;
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            myAudio.enabled = false;
        }
        PlayerPrefs.Save();
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        GameObject.FindGameObjectsWithTag("deleter");
        foreach (GameObject targetWord in GameObject.FindGameObjectsWithTag("deleter"))
        {
            save.theWords.Add(targetWord.GetComponent<WordDisplay>().text.text);

            save.wordPositionX.Add(targetWord.transform.position.x);
            save.wordPositionY.Add(targetWord.transform.position.y);
        }

        save.score = Score.score;
        save.lives = Score.lives;
        save.speed = MenuManager.gameSpeed;
        save.name = MenuManager.userName;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            MenuManager.wpx.Clear();
            MenuManager.wpy.Clear();
            MenuManager.tw.Clear();
            for (int i = 0; i < save.theWords.Count; i++)
            {
                MenuManager.wpx.Add(save.wordPositionX[i]);
                MenuManager.wpy.Add(save.wordPositionY[i]);
                MenuManager.tw.Add(save.theWords[i]);
            }
            Debug.Log("statics loaded");

            MenuManager.savedScore = save.score;
            MenuManager.savedLives = save.lives;
            MenuManager.savedSpeed = save.speed;
            MenuManager.userName = save.name;
            MenuManager.savedData = true;

            Debug.Log("Game Loaded");
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    public void LoadJSON()
    {
        try
        {
            Save save = JsonUtility.FromJson<Save>(json);
            MenuManager.wpx.Clear();
            MenuManager.wpy.Clear();
            MenuManager.tw.Clear();
            for (int i = 0; i < save.theWords.Count; i++)
            {
                MenuManager.wpx.Add(save.wordPositionX[i]);
                MenuManager.wpy.Add(save.wordPositionY[i]);
                MenuManager.tw.Add(save.theWords[i]);
            }
            Debug.Log("statics loaded");

            MenuManager.savedScore = save.score;
            MenuManager.savedLives = save.lives;
            MenuManager.savedSpeed = save.speed;
            MenuManager.userName = save.name;
            MenuManager.savedData = true;

            Debug.Log("Game Loaded");
            SceneManager.LoadScene("Game");
        }
        catch
        {
            Debug.Log("No JSON file detected!");
        }

    }
}
