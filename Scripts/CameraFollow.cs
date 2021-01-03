using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);
    public float dampingTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    private void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);

        if (smooth)
        {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampingTime);
        }
        else
        {
            transform.position = destination;
        }
    }
}
