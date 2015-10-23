using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour {

    public Transform bullet;
    public float spawnInFront = 1f;

    void Shoot()
    {
        var b = Instantiate(bullet, transform.position + transform.forward *spawnInFront , transform.rotation) as Transform;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }
}
