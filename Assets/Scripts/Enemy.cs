using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [Tooltip("How much score will player get after killing that enemy")][SerializeField] int score;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindAnyObjectByType<ScoreBoard>();    
    }

    void OnParticleCollision(GameObject other)
    {
        ProccessHit();
        KillEnemy();
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    void ProccessHit()
    {
        scoreBoard.IncreaseScore(score);
    }
}
