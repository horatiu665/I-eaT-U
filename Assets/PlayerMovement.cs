using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public string moveForwardAxis = "Vertical";
    public float movementSpeed = 5f;
    public string moveSidewaysAxis = "Horizontal";

    public string rotateAxis = "Horizontal";
    public float rotateAnglePerSecond = 180;

    public bool rotateTowardsMovement;

    void UpdateRotation()
    {
        if (rotateTowardsMovement) {

        } else {
            float rotationInput = 0;
            try {
                rotationInput = Input.GetAxis(rotateAxis);
            }
            catch {

            }

            rotationInput *= rotateAnglePerSecond * Time.deltaTime;
            transform.Rotate(0, rotationInput, 0);
        }
    }

    void UpdatePosition()
    {
        Vector3 movementInput = Vector3.zero;
        try {
            movementInput.z = Input.GetAxis(moveForwardAxis);
        }
        catch {

        }

        try {
            movementInput.x = Input.GetAxis(moveSidewaysAxis);
        }
        catch {

        }


        movementInput *= movementSpeed * Time.deltaTime;

        GetComponent<Rigidbody>().velocity += (movementInput);

        //transform.Translate(0, 0, movementInput);
        if (rotateTowardsMovement) {
            transform.LookAt(transform.position + movementInput);
        }
    }

    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }
}
