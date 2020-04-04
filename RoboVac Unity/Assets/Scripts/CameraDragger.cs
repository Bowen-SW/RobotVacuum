using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragger : MonoBehaviour
{
    public float dragSpeed = 0.5f;
    private Vector3 dragOrigin;
    
    public float sensitivity = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleZoom();


        /*fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;*/

        /*float scrollWheelChange = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelChange != 0)
        {
            Camera.main.transform.position += Camera.main.transform.forward * scrollWheelChange;
        }*/

    }

    private void HandleMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1))
        {
            return;
        }

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);

        transform.Translate(move, Space.World);
    }

    private void HandleZoom()
    {
        float cameraZoomSpeed = Input.GetAxis("Mouse ScrollWheel") * sensitivity;

        float newSize = Camera.main.orthographicSize + cameraZoomSpeed;

        if (newSize < 1)
        {
            Camera.main.orthographicSize = 1;
        }
        else if (newSize > 20)
        {
            Camera.main.orthographicSize = 20;
        }
        else
        {
            Camera.main.orthographicSize = newSize;
        }
        

    }

}
