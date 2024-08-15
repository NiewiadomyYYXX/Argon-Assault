using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [Tooltip("How much score will player get after hiting that enemy")]
    [SerializeField] int score;
    [Tooltip("Hp of enemy")]
    [SerializeField] int hp;
    [Tooltip("Dmg of player")]
    [SerializeField] int dmg;


    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindAnyObjectByType<ScoreBoard>();    
    }

    void OnParticleCollision(GameObject other)
    {
        ProccessHit();
        if (hp <= 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    void ProccessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        scoreBoard.IncreaseScore(score);
        hp -= dmg;
    }
}
