using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private PlayerMovement _controller;

    // Start is called before the first frame update
    private void Awake()
    {
        _controller = GetComponent<PlayerMovement>();
        _startPos = _camera.localPosition;
    }

    // Update is called once per frame
    private Vector()
    {
        
    }
}
