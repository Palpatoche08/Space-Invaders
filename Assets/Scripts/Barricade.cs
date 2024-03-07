using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public int hitsToDestroy = 5;
    public float reductionAmount = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("EvilBullet"))
        {
            ReduceSize();
            Destroy(collision.gameObject); 
            CheckDestroy();
        }
    }

    private void ReduceSize()
    {
        transform.localScale -= new Vector3(reductionAmount, 0, reductionAmount);
    }

    private void CheckDestroy()
    {
        hitsToDestroy--;

        if (hitsToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
