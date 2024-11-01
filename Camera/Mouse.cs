using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    Vector3 moveVector = Vector3.zero;
    [SerializeField] float speed = 2.5f;
    [SerializeField] float rotationSpeed = 1f;
    Vector3 rotation;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    float targetFOV;
    [SerializeField] float minFOV = 35f;
    [SerializeField] float maxFOV = 90f;
    [SerializeField] float zoomSensitivity = 5f;

    // Start is called before the first frame update
    void Start()
    {
        targetFOV = virtualCamera.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        RotateCamera();
        Zoom();
    }

    private void Zoom()
    {
        targetFOV += Input.mouseScrollDelta.y * zoomSensitivity;
        targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
        virtualCamera.m_Lens.FieldOfView = targetFOV;
    }

    private void RotateCamera()
    {
        rotation = Vector3.zero;
        if (Input.GetKey(KeyCode.Q))    
        {
            rotation.y = rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotation.y = -(rotationSpeed * Time.deltaTime);
        }
        transform.eulerAngles += rotation;
    }

    private void CameraMove()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.z = Input.GetAxisRaw("Vertical");

        moveVector = transform.right * moveVector.x + transform.forward * moveVector.z;

        transform.position += moveVector * Time.deltaTime * speed;
    }
}
