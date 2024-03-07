using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBullets : MonoBehaviour
{
    public float Bullet_Forward_Force = 10f;
    private PlayerController playerController; 


    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * Bullet_Forward_Force, ForceMode.Impulse);

        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barricade") || (collision.gameObject.CompareTag("Player")) || collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator EnableCollisions(Collider colliderToEnable)
    {
        yield return new WaitForSeconds(0.1f); 
        Physics.IgnoreCollision(colliderToEnable, GameObject.FindWithTag("Alien").GetComponent<Collider>(), false);
    }

}
