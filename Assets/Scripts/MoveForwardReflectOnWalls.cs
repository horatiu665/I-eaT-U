using UnityEngine;
using System.Collections;

public class MoveForwardReflectOnWalls : MonoBehaviour
{
    public int playerNumber;
    public float speed;

    public Transform explosionPrefab;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        var maxDistanceThisFrame = speed * Time.deltaTime;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistanceThisFrame)) {
            var toPoint = hit.point - transform.position;
            hit.transform.SendMessage("BulletCollision", this, SendMessageOptions.DontRequireReceiver);
            // reflect bullet and change direction
            var reflected = Vector3.Reflect(toPoint, hit.normal);
            transform.forward = reflected;
            var travelledBeforePoint = toPoint.magnitude;
            transform.position = hit.point + reflected * (1 - travelledBeforePoint / maxDistanceThisFrame);

        } else {

            var deltaThisFrame = transform.forward * maxDistanceThisFrame;
            transform.position += deltaThisFrame;

        }
    }
}
