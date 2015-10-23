using UnityEngine;
using System.Collections;
using System;

public class SlideableWall : MonoBehaviour
{

    Transform playerCam;
    public float maxRaycastDistance = 5;

    bool sliding;

    // Use this for initialization
    void Start()
    {
        playerCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sliding) {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, maxRaycastDistance)) {
                if (hit.transform == transform) {
                    var slideDir = 0;
                    if (Input.GetKeyDown(KeyCode.E)) {
                        slideDir = 1;

                    } else if (Input.GetKeyDown(KeyCode.Q)) {
                        slideDir = -1;
                    }
                    if (slideDir != 0) {
                        Slide(slideDir);
                    }
                }
            }
        }
    }

    public float slideDuration;

    public Vector3 relativeSlidePos;
    AnimationCurve easeCurve;

    void SetupEaseCurve()
    {
        easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }

    private void Slide(int slideDir)
    {
        if (easeCurve == null)
            SetupEaseCurve();

        sliding = true;
        var initPos = transform.position;
        StartCoroutine(pTween.To(slideDuration, 0, 1, t => {
            transform.position = Vector3.Lerp(initPos, initPos + transform.TransformDirection(relativeSlidePos) * slideDir, easeCurve.Evaluate(t));
            if (t == 1) {
                sliding = false;
            }
        }));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(relativeSlidePos));

    }
}
