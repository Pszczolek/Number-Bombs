using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> laserSounds;
    [SerializeField]
    List<AudioClip> explosionSounds;
    public float volume = 0.5f;

    public static SFXPlayer Instance;

    [SerializeField]
    GameSettings gameSettings;
    private Vector3 _cameraPos;

    private void Awake()
    {
        Instance = this;
        _cameraPos = Camera.main.transform.position;
        volume = gameSettings.volume;
        gameSettings.OnVolumeChanged += SetVolume;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        gameSettings.OnVolumeChanged -= SetVolume;
    }

    public void SetVolume(float value)
    {
        volume = value;
    }

    public void PlayExplosionAtLocation(Vector3 position)
    {
        //Vector3 playPos = new Vector3(position.x, position.y, _cameraPos.z);
        Vector3 playPos = Vector3.Normalize(position - transform.position) * 2;
        var randomSound = explosionSounds[Random.Range(0, explosionSounds.Count)];
        //Debug.Log(playPos);
        AudioSource.PlayClipAtPoint(randomSound, playPos, volume);
    }

    public void PlayLaserAtLocation(Vector3 position)
    {
        Vector3 playPos = new Vector3(position.x, position.y, _cameraPos.z);
        var randomSound = laserSounds[Random.Range(0, laserSounds.Count)];
        AudioSource.PlayClipAtPoint(randomSound, playPos, volume);
    }
}
