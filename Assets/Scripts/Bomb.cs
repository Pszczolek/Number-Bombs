using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Flags]
public enum OperationType
{
    None = 0,
    Summation = 1,
    Substraction = 2,
    Multiplication = 4,
    Division = 8
}
public class Bomb : MonoBehaviour
{
    public int number;
    public int scoreValue = 1;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] TMP_Text text;


    public void SetNumber(int num)
    {
        number = num;
        text.text = number.ToString();
    }

    public void SetMovement(Vector3 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void SetOperation(int numA, int numB, OperationType type)
    {
        switch (type)
        {
            case OperationType.Summation:
                text.text = $"{numA} + {numB}";
                number = numA + numB;
                break;
            case OperationType.Substraction:
                text.text = $"{numA} - {numB}";
                number = numA - numB;
                break;
            case OperationType.Multiplication:
                text.text = $"{numA} x {numB}";
                number = numA * numB;
                break;
            case OperationType.Division:
                text.text = $"{numA} / {numB}";
                number = numA / numB;
                break;
        }
        //SetNumber(number);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        DamagePlayer();
    }

    private void DamagePlayer()
    {
        Player.Instance.Hit();
        ParticleTextController.Instance.BombLost(this);
    }

    private void Explode()
    {
        //play some particle effects
        BombSpawner.Instance.BombDestroyed(this);
        SFXPlayer.Instance.PlayExplosionAtLocation(transform.position);
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 5f);

        Destroy(gameObject);
    }

    public void Hit()
    {
        Player.Instance.AddScore(scoreValue);
        ParticleTextController.Instance.BombHit(this);
        Explode();

    }
}
