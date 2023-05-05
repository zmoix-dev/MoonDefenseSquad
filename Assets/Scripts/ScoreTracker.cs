using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    TMP_Text text;
    int score;

    void Start() {
        text = GetComponent<TMP_Text>();
        score = 0;
    }

    public void UpdateScore(int increment) {
        score += increment;
        text.text = score.ToString();
    }
}
