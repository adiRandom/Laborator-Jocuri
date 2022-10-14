using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private Vector3 _offset;

    public Transform sphereTransform;
    public Transform planeTransform;
    // Start is called before the first frame update
    void Start()
    {
        // Look at the sphere
        transform.LookAt(sphereTransform);
        _offset = this.transform.position - sphereTransform.position;
    }

    private void Update()
    {
        // When pressing O or P move the camera closer or further away from the sphere
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (_offset.magnitude > 1)
            {
                _offset = _offset * 0.9f;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_offset.magnitude <= 6)
            {
                _offset = _offset * 1.1f;
            }
        }
      
        // When clicking get the point on the plane that was clicked and move the sphere to that point
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == planeTransform)
                {
                    sphereTransform.position = hit.point;
                }
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = sphereTransform.position + _offset;
    }
}
