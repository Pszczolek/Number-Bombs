using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        Player.Instance.OnHealthChanged += UpdateDisplay;
    }
    public void UpdateDisplay(int health)
    {
        text.text = health.ToString();
    }
}
