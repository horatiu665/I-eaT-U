using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour {

    public Transform shootStuff;
    public float shootForce;
    public float delay = 1f;
	
	// Update is called once per frame
	void Start () {
        if (delay <= 0) delay = 0.001f;
        InvokeRepeating("Shoot", delay, delay);

	}

    void Shoot()
    {
        var s = Instantiate(shootStuff, transform.position, transform.rotation) as Transform;
        s.GetComponent<Rigidbody>().AddForce(shootForce * s.forward, ForceMode.Impulse);

    }
}
