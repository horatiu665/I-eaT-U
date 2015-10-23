using UnityEngine;
using System.Collections;
using System;

public class SlideableWall : MonoBehaviour
{

    Transform playerCam;
    public float maxRaycastDistance = 5;

    bool sliding;


    public float slideDuration;
    public Vector3 relativeSlideDir;
    AnimationCurve easeCurve;
    public LayerMask wallLayerMask;

    // Use this for initialization
    void Start()
    {
        playerCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sliding) {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)) {
                if (relativeSlideDir != Vector3.zero) {
                    RaycastHit hit;
                    if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, maxRaycastDistance)) {
                        if (hit.transform == transform) {

                            var slideDir = 0;
                            if (Input.GetKeyDown(KeyCode.E)) {
                                slideDir = 1;

                            } else if (Input.GetKeyDown(KeyCode.Q)) {
                                slideDir = -1;
                            }

                            if (Mathf.Sign(Vector3.Cross(playerCam.forward, transform.right).y) < 0) {
                                slideDir *= -1;
                            }

                            Vector3 slideDirection = transform.TransformDirection(relativeSlideDir) * slideDir;
                            // if there is free space to the side in which we are moving
                            if (!Physics.Raycast(transform.position, slideDirection, out hit, (transform.localScale.x) + (slideDirection).magnitude, wallLayerMask)) {
                                Debug.DrawRay(transform.position, slideDirection, Color.cyan, 5);
                                Slide(slideDirection);

                            } else {
                                Debug.DrawRay(transform.position, slideDirection, Color.magenta, 5);
                                Debug.Log("Cannot move, there is " + hit.transform.name + " in the way", gameObject);
                            }

                        }
                    }
                }
            }
        }
    }

    void SetupEaseCurve()
    {
        easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }

    private void Slide(Vector3 slideDir)
    {
        if (easeCurve == null)
            SetupEaseCurve();

        sliding = true;
        var initPos = transform.position;
        StartCoroutine(pTween.To(slideDuration, 0, 1, t => {
            transform.position = Vector3.Lerp(initPos, initPos + slideDir, easeCurve.Evaluate(t));
            if (t == 1) {
                sliding = false;
            }
        }));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(relativeSlideDir));

    }
}
