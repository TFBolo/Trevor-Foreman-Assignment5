using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    private static string[] wordList = {"naive", "pastoral", "ordinary", "nose", "shocking", "automatic", "ruddy", "right", "tacky",
                                        "scare", "brown", "reply", "yell", "interfere", "festive", "ethereal", "tendency", "true",
                                        "hideous", "different", "fly", "terrify", "memory", "cent", "ball", "absurd", "afford",
                                        "crowded", "road", "lyrical", "representative", "probable", "fade", "plantation", "interrupt",
                                        "wasteful", "frightened", "efficient", "hand", "enter", "super", "crazy", "deceive", "wrathful",
                                        "obsolete", "compare", "support", "lethal", "trains", "tough" };
    public static string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        string randomWord = wordList[randomIndex];

        return randomWord;
    }
}
