using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : ObjectBase
{   
    private enum ModelMode
    {
        Sprite = 0, Model3D = 1, Texture = 2,
        End
    }
    private Dictionary<ModelMode, string> _modelNames = new Dictionary<ModelMode, string>(){
        { ModelMode.Sprite, "2DSprite" },
        { ModelMode.Model3D, "3DModel" },
        { ModelMode.Texture, "2DTextureModel" }
    };


    private float _moveSpeed;
    private float _horizontal, _vertical;
    private Vector3 _dir, _rotate;

    private Transform _model;

    [SerializeField]
    private ModelMode _modelMode;

    private void Start()
    {
        _moveSpeed = 10f;
        _model = GetModel();
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

    #region Debuging Model Setting System

    private Transform GetModel()
    {
        for(ModelMode index = ModelMode.Sprite; index != ModelMode.End; index++)
        {
            if(index != _modelMode)
                TurnOffModel(_modelNames[index]);
        }
        
        Transform model = transform.Find(_modelNames[_modelMode]);

        IsFailedFindModel(model);

        return model;
    }

    private void TurnOffModel(string name)
    {
        Transform model = transform.Find(name);

        if(IsFailedFindModel(model))
        { return; }

        TurnOffBillboard(model.gameObject);

        model.gameObject.SetActive(false);
    }

    private void TurnOffBillboard(GameObject model)
    {
        Billboard billboard = null;
        model.gameObject.TryGetComponent<Billboard>(out billboard);

        if (billboard)
            billboard.StopBillboard(true);
    }

    private bool IsFailedFindModel(Transform model)
    {
        if (model == null)
        {
            Debug.LogError("Can't Find Model");
            return true;
        }
        return false;
    }

    #endregion

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
        if(_horizontal == 0 && _vertical == 0 || _modelMode != ModelMode.Model3D)
        {
            return;
        }
        _rotate.y = 90 - (Mathf.Atan2(_vertical, _horizontal) * Mathf.Rad2Deg);
        _model.rotation = Quaternion.Euler(_rotate);
    }
}
