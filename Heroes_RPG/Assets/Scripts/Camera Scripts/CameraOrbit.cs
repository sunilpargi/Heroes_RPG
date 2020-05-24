using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : Orbit
{
    public Vector3 target_Offset = new Vector3(0, 2, 0);
    public Vector3 camera_Position_Zoom = new Vector3(-0.5f, 0, 0);
    public float camera_Length = -10f;
    public float camera_Length_Zoom = -5f;
    public Vector2 orbit_Speed = new Vector2(0.01f, 0.01f);
    public Vector2 orbit_Offset = new Vector2(0, -08f);
    public Vector2 angle_Offset = new Vector2(0, -0.25f);

    private float zoomvalue;
    private Vector3 camera_Position_Temp;
    private Vector3 camera_Position;

    private Transform playerTarget;
    private Camera mainCamera;





    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        spherical_Vector_Data.Length = camera_Length;
        spherical_Vector_Data.Azimuth = angle_Offset.x;
        spherical_Vector_Data.Zenith = angle_Offset.y;

        mainCamera = Camera.main;

        camera_Position_Temp = mainCamera.transform.localPosition;
        camera_Position = camera_Position_Temp;

        MouseLock.Mouselocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget)
        {
            HandleCamera();
            handleMouseLocking();
        }
        
    }

    void HandleCamera()
    {
        if(MouseLock.Mouselocked)
        {
            spherical_Vector_Data.Azimuth += Input.GetAxis("Mouse X") * orbit_Speed.x;
            spherical_Vector_Data.Zenith += Input.GetAxis("Mouse Y") * orbit_Speed.y;
        }
    

        spherical_Vector_Data.Zenith = Mathf.Clamp(spherical_Vector_Data.Zenith + orbit_Offset.x,
            orbit_Offset.y, 0f);

        float distance_ToObject = zoomvalue;
        float delta_Distance = Mathf.Clamp(zoomvalue, distance_ToObject, -distance_ToObject);
        spherical_Vector_Data.Length += (delta_Distance - spherical_Vector_Data.Length);

        Vector3 lookAt = target_Offset;

        lookAt += playerTarget.position;

        base.Update();

        transform.position += lookAt;
        transform.LookAt(lookAt);

        if(zoomvalue == camera_Length_Zoom)
        {
            Quaternion targetRotation = transform.rotation;
            targetRotation.x = 0f;
            targetRotation.z = 0f;
            playerTarget.rotation = targetRotation;
        }

        camera_Position = camera_Position_Temp;
        zoomvalue = camera_Length;
    }

    void handleMouseLocking()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(MouseLock.Mouselocked)
            {
                MouseLock.Mouselocked = false;
            }
            else
            {
                MouseLock.Mouselocked = true;
            }
        }
    }
}
