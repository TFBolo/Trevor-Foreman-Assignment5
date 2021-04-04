using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour
{
    public List<Word> words;

    public WordSpawner wordSpawner;
    public Score scoreControl;

    private bool hasActiveWord = false;
    private Word activeWord;

    public Text scoreText;
    public Text livesText;

    private void Start()
    {
        scoreControl.UnPause();
        if (MenuManager.savedData)
        {
            for (int i = 0; i < MenuManager.wpx.Count; i++)
            {
                wordSpawner.SpawnSavedWord(MenuManager.wpx[i], MenuManager.wpy[i], MenuManager.tw[i]);
            }
            Score.score = MenuManager.savedScore;
            Score.lives = MenuManager.savedLives;
            scoreText.text = "Score: " + Score.score;
            livesText.text = "Lives: " + Score.lives;
            MenuManager.gameSpeed = MenuManager.savedSpeed;
            MenuManager.savedData = false;
        }
    }

    public void AddWord()
    {
        Word word = new Word(WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
        Debug.Log(word.word);

        words.Add(word);
    }

    public void PopulateWord(string savedWord, WordDisplay savedDisplay)
    {
        Word word = new Word(savedWord, savedDisplay);
        words.Add(word);
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
        }
        else
        {
            foreach(Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }
            }
        }
        if (hasActiveWord && activeWord.WordTyped())
        {
            hasActiveWord = false;
            Score.score++;
            scoreText.text = "Score: " + Score.score;
            words.Remove(activeWord);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "deleter")
        {
            if (hasActiveWord)
            {
                if (collision.gameObject.GetComponent<WordDisplay>().originalWord == activeWord.word)
                {
                    hasActiveWord = false;
                }
            }
            words.Remove(words.Find(x => x.word == collision.gameObject.GetComponent<WordDisplay>().originalWord));
            Destroy(collision.gameObject);
            if (Score.lives <= 1)
            {
                SceneManager.LoadScene("Exit");
            }
            Score.lives--;
            livesText.text = "Lives: " + Score.lives;
        }
    }
}
