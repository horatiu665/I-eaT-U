using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public string moveForwardAxis = "Vertical";
    public float movementSpeed = 5f;
    public string rotateAxis = "Horizontal";
    public float rotateAnglePerSecond = 180;

    void UpdateRotation()
    {
        float rotationInput = 0;
        try {
            rotationInput = Input.GetAxis(rotateAxis);
        }
        catch {

        }

        rotationInput *= rotateAnglePerSecond * Time.deltaTime;
        transform.Rotate(0, rotationInput, 0);

    }

    void UpdatePosition()
    {
        float movementInput = 0;
        try {
            movementInput = Input.GetAxis(moveForwardAxis);
        }
        catch {

        }

        movementInput *= movementSpeed * Time.deltaTime;
        
        GetComponent<Rigidbody>().velocity += (transform.forward * movementInput);
        
        //transform.Translate(0, 0, movementInput);

    }

    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }
}
