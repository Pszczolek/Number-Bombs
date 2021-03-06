using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombShooter : MonoBehaviour
{

    [SerializeField]
    GameObject turret;
    [SerializeField]
    ParticleSystem[] laserSfx;
    [SerializeField]
    Animator turretAnim;

    // Start is called before the first frame update
    void Start()
    {
        InputReader.Instance.OnNumberFired += TryHit;
    }

    private void TryHit(int number)
    {
        var target = BombSpawner.Instance.spawnedBombs.FirstOrDefault(b => b.number == number);
        if(target != null)
        {
            FaceTarget(target.transform.position);
            target.Hit();
            //Debug.Log("Bomb hit!");
        }
        else
        {
            ShootLaser();
            //Debug.Log("Miss!");
            ParticleTextController.Instance.BombMissed();
        }
    }

    private void FaceTarget(Vector3 position)
    {
        turret.transform.LookAt(position);
        Vector3 look = turret.transform.rotation.eulerAngles;
        //Debug.Log($"Rotation: {look}");
        ShootLaser(position);

    }

    private void ShootLaser(Vector3 target)
    {
        foreach (var laser in laserSfx)
        {
            var main = laser.main;
            main.startSpeed = Vector3.Distance(target, turret.transform.position);
        }
        ShootLaser();
    }

    private void ShootLaser()
    {
        turretAnim.SetTrigger("Fire");
        SFXPlayer.Instance.PlayLaserAtLocation(turret.transform.position);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
