using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private TextMeshProUGUI score;
    private TextMeshProUGUI health;

    private void Awake()
    {
        health = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        score = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(int value)
    {
        score.text = value.ToString();
    }

    public void SetHealth(int value)
    {
        health.text = value.ToString();
    }
}
