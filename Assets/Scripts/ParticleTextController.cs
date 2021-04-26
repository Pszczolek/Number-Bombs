using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class ParticleTextController : MonoBehaviour
{
    [SerializeField] CFXR_ParticleText bombHitTextPrefab;
    [SerializeField] CFXR_ParticleText bombLostTextPrefab;
    [SerializeField] float textZOffset = 5;
    [SerializeField] float textLostYOffset = 1;

    public static ParticleTextController Instance;

    private Vector3 _cameraPos;

    private void Awake()
    {
        Instance = this;
        _cameraPos = Camera.main.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void BombHit(Bomb bomb)
    {
        var position = bomb.transform.position;
        var newText = Instantiate(bombHitTextPrefab, position, Quaternion.identity);
        var distance = Vector3.Distance(position, _cameraPos);
        newText.transform.position = Vector3.MoveTowards(position, _cameraPos, distance - 10);
        newText.text = bomb.number.ToString();
        newText.GenerateText();
    }

    public void BombLost(Bomb bomb)
    {
        var position = bomb.transform.position + new Vector3(0,textLostYOffset, 0);
        var newText = Instantiate(bombLostTextPrefab, position, Quaternion.identity);
        var distance = Vector3.Distance(position, _cameraPos);
        newText.transform.position = Vector3.MoveTowards(position, _cameraPos, distance - 10);
        newText.text = bomb.number.ToString();
        newText.GenerateText();
    }

}
