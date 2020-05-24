using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoom_Sensitivity = 15f;
    public float zoom_Speed = 20f;
    public float zoom_Min = 30f;
    public float zoom_Max = 70f;

    private float z;

    private Camera maincamera;
   
    void Start()
    {
        maincamera = Camera.main;
        z = maincamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        z -= Input.GetAxis("Mouse ScrollWheel") * zoom_Sensitivity;
        z = Mathf.Clamp(z, zoom_Min, zoom_Max);
    }

    private void LateUpdate()
    {
        maincamera.fieldOfView = Mathf.Lerp(maincamera.fieldOfView, z, Time.deltaTime * zoom_Speed);
    }
}
