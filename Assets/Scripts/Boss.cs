using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAlien : MonoBehaviour
{
    public int bossLives = 5;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HandleBulletHit();
            Destroy(collision.gameObject); 
        }
    }

    void HandleBulletHit()
    {
        bossLives--;

        if (bossLives <= 0)
        {
            playerController.IncreaseScore(1000);
            Destroy(gameObject); 
        }
    }
}