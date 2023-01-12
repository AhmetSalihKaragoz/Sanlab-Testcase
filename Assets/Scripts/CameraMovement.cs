using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float maxZoomOutDistance;
    [SerializeField] private float maxZoomInDistance;
    [SerializeField] private float zoomPerSlide;

    [SerializeField] private float offsetDiv;

    private Vector3 _cameraStartingPos;

    private Vector3 _cacheMousePos;

    private void Start()
    {
        _cameraStartingPos = transform.position;
    }

    private void LateUpdate()
    {
        ZoomIn();
        ZoomOut();
        Rotate();
    }

    void ZoomIn()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            var goalPos = new Vector3(transform.position.x, transform.position.y,
                transform.position.z + zoomPerSlide);
            var lerp = Vector3.Lerp(transform.position, goalPos, 0.5f*Time.deltaTime);
            transform.position = lerp;
        }
    }

    void ZoomOut()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            var goalPos = new Vector3(transform.position.x, transform.position.y,
                transform.position.z - zoomPerSlide);
            var lerp = Vector3.Lerp(transform.position, goalPos, 0.5f*Time.deltaTime);
            transform.position = lerp;
        }
    }

    void Rotate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _cacheMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            var offset = (Input.mousePosition + _cacheMousePos)/offsetDiv;
            var newCamRot = new Vector3(transform.rotation.x + offset.y, transform.rotation.y + offset.x);
            transform.rotation = quaternion.Euler(newCamRot);
        }
        // if (Input.GetMouseButtonUp(1))
        // {
        //     _cacheMousePos = Vector3.zero;
        // }
        
    }

}
