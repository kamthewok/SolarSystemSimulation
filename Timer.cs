using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Rigidbody orbitingObject;
    public Transform centerObject;

    private bool isTiming = false;
    private float startTime = 0f;
    private float orbitTime = 0f;
    private int orbitCount = -1;

    void Start()
    {
        isTiming = true;
        startTime = Time.time;
        //Debug.Log("Czas START");
    }

    void FixedUpdate()
    {
        if (isTiming)
        {
            //Debug.Log("Czas1");
           // Debug.Log("Obieg: " + orbitCount);
            Vector3 directionToCenter = centerObject.position - orbitingObject.position;
            Quaternion rotationToCenter = Quaternion.LookRotation(directionToCenter, Vector3.up);
            Quaternion currentRotation = orbitingObject.rotation;

            float angle = Quaternion.Angle(rotationToCenter, currentRotation);

            if (angle <= 1f)
            {
                orbitCount++;
                //Debug.Log("Obieg if: " + orbitCount);


                if (orbitCount == 2)
                {
                    
                    orbitTime = Time.time - startTime;
                    Debug.Log("Czas obiegu: " + orbitTime.ToString("F2") + "s");

                    isTiming = false;
                }
            }
        }
    }
}
