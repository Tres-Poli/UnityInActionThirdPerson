using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private float _rotY;
    private Vector3 _offset;

    public float RotationSpeed = 1.5f;
    

    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = _target.position - transform.position;
    }

    private void LateUpdate()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                _offset *= 1.2f;
            }
            else
            {
                _offset /= 1.2f;
            }
        }

        _rotY += Input.GetAxis("Mouse X") * RotationSpeed * 3;

        var rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = _target.position - (rotation * _offset);
        transform.LookAt(_target);
    }

    void Update()
    {
        
    }
}
