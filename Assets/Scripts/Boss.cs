using System.Collections;
using UnityEngine;

public class BossAlien : MonoBehaviour
{
    public int bossLives = 5;
    public ParticleSystem deathParticles; 
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
            StartCoroutine(ExplodeAndDisable());
        }
    }

    IEnumerator ExplodeAndDisable()
    {
        deathParticles.Play();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        deathParticles.gameObject.SetActive(false);
    }
}
