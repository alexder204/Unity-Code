using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10.0f;

    [SerializeField] private Transform camera = null;
    [SerializeField] private Transform cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private PlayerMovement _controller;

    // Start is called before the first frame update
    private void Awake()
    {
        controller = GetComponent<PlayerMovement>();
        startPos = camera.localPosition;
    }

    // Update is called once per frame
    private Vector()
    {
        
    }
}
