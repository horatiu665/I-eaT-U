using UnityEngine;
using System.Collections;

public class AddForceOnStart : MonoBehaviour
{

    public float force = 1;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(force * transform.forward);
    }

}
