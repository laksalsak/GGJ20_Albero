using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve curve;
    public Camera cam;
    public float  nume_ooggetti;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        cam.orthographicSize = curve.Evaluate(nume_ooggetti);
    }
}
