using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectBase
{
    private float _moveSpeed;
    private float _horizontal, _vertical;
    private Vector3 _dir, _rotate;

    private Transform _model;

    private void Start()
    {
        _moveSpeed = 10f;
        _model = transform.Find("Model");
    }

    private void Update()
    {
        InputKey();
        SetDir();
        RotateModel();

        if(IsEmptySpace())
        {
            Move();
        }
    }

    private void InputKey()
    {
        _vertical = Input.GetAxisRaw("Vertical");
        _horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void SetDir()
    {
        _dir.x = _horizontal;
        _dir.z = _vertical;
        _dir = _dir.normalized;
    }

    private bool IsEmptySpace()
    {
        return true;
    }

    private void Move()
    {
        transform.position += _dir * _moveSpeed * Time.deltaTime;
    }

    private void RotateModel()
    {
        if(_horizontal == 0 && _vertical == 0)
        {
            return;
        }
        _rotate.y = 90 - (Mathf.Atan2(_vertical, _horizontal) * Mathf.Rad2Deg);
        _model.rotation = Quaternion.Euler(_rotate);
    }
}
