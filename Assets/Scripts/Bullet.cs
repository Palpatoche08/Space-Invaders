using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Bullet_Forward_Force = 50f;
    public int bossLives = 5;
    private PlayerController playerController;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * Bullet_Forward_Force, ForceMode.Impulse);

        playerController = FindObjectOfType<PlayerController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EvilBullet"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
        }
        else if (collision.gameObject.CompareTag("FrontAlien"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
            playerController.IncreaseScore(100); 
        }
        else if (collision.gameObject.CompareTag("Alien"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
            playerController.IncreaseScore(200); 
        }
        else if (collision.gameObject.CompareTag("BackAlien"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
            playerController.IncreaseScore(300); 
        }
        else if (collision.gameObject.CompareTag("BossAlien"))
        {
            bossLives = bossLives - 1;
            if (bossLives <= 0)
            {
                playerController.IncreaseScore(1000); 
                Destroy(collision.gameObject); 
            }

            Destroy(gameObject); 

        }
    
    }
}
