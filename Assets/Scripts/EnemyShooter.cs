using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject EvilBulletPrefab;
    public float shootInterval = 3f;
    public float evilBulletForce = 10f;

    void Start()
    {
        StartCoroutine(ShootEvilBullet());
    }

    IEnumerator ShootEvilBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            SpawnEvilBullet();
        }
    }

    void SpawnEvilBullet()
    {
        GameObject temporaryBulletHandler;
        temporaryBulletHandler = Instantiate(EvilBulletPrefab, transform.position, Quaternion.identity) as GameObject;
        temporaryBulletHandler.transform.rotation = Quaternion.Euler(Vector3.forward * 180f);

        Rigidbody temporaryRigidBody;
        temporaryRigidBody = temporaryBulletHandler.GetComponent<Rigidbody>();
        temporaryRigidBody.AddForce(Vector3.down * evilBulletForce, ForceMode.Impulse);

        StartCoroutine(EnableCollisions(temporaryBulletHandler.GetComponent<Collider>()));

        Destroy(temporaryBulletHandler, 3f);
    }

    IEnumerator EnableCollisions(Collider colliderToEnable)
    {
        yield return new WaitForSeconds(0.1f);
        Physics.IgnoreCollision(colliderToEnable, GameObject.FindWithTag("Alien").GetComponent<Collider>(), false);
    }
}
