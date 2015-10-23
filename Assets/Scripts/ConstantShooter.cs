using UnityEngine;
using System.Collections;

public class ConstantShooter : MonoBehaviour
{

    public Transform bullet;
    public float spawnInFront = 0;
    public float timeBetweenShots = 1;
    float timer;

    void Shoot()
    {
        var b = Instantiate(bullet, transform.position + transform.forward * spawnInFront, transform.rotation) as Transform;

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenShots) {
            timer = 0;
            Shoot();

        }

    }
}
