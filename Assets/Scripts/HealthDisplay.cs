using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    TMP_Text text;
    [SerializeField] GameObject livesGrid;
    [SerializeField] GameObject livesSilhouettesGrid;
    [SerializeField] GameObject iconPrefab;
    [SerializeField] GameObject silhouettePrefab;
    bool isInit;

    List<GameObject> icons = new List<GameObject>();
    List<GameObject> silhouettes = new List<GameObject>();

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
        if(isInit == false)
        {
            Initialize(health);
            return;
        }

        for(int i = 0; i < health; i++)
        {
            icons[i].SetActive(true);
        }
        for(int i = health; i < icons.Count; i++)
        {
            icons[i].SetActive(false);
        }

    }

    private void Initialize(int health)
    {
        for (int i = 0; i < health; i++)
        {
            var newIcon = Instantiate(iconPrefab, livesGrid.transform);
            icons.Add(newIcon);
            var newSilhouette = Instantiate(silhouettePrefab, livesSilhouettesGrid.transform);
            silhouettes.Add(newSilhouette);
        }
        isInit = true;
    }
}
