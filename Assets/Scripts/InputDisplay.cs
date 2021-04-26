using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputDisplay : MonoBehaviour
{
    private TMP_Text _text;
    // Start is called before the first frame update

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = "0";
    }

    private void Start()
    {
        InputReader.Instance.OnDigitEntered += UpdateDisplay;
    }

    private void OnDestroy()
    {
        InputReader.Instance.OnDigitEntered -= UpdateDisplay;
    }

    public void UpdateDisplay(int number)
    {
        _text.text = number.ToString();
    }
}
