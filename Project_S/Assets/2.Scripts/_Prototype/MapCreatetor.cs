using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapCreatetor : MonoBehaviour
{
    private GameObject _mapRoot;

    [SerializeField]
    private List<GameObject> _mapBlocks = new List<GameObject>();

    private float _left, _right, _forward, _back;

    [SerializeField]
    private int _index;
    private Vector3 _point;
    private Vector3 _weightPoint;

    [SerializeField]
    private int _quadrant;

    // �ݽð� �������� ȸ���ϸ鼭 ��� ����
    //�� 2���� ȸ������ 2�ߺ� ����

    private void Start()
    {
        Init();
        CreateMap();
    }

    private void Init()
    {
        _left = -5;
         _right = 5;
        _back = -8.7f;
        _forward = 8.7f;

        _point.x = _right;
        _point.y = 0.75f;
        _point.z = 0;

        _weightPoint.x = -0.5f;
        _weightPoint.y = 0;
        _weightPoint.z = 0.87f;

        _quadrant = 1;

        _mapRoot = GameObject.Find("Environment");
        _index = 0;

        _point += _weightPoint;
    }


    private void CreateMap()
    {
        for (int i = 1; i <= 40; i++)
        {
            if(i % 10 == 0)
            {
                SetDir();
            }
        
            Spwan();
            _point += _weightPoint;
        }
    }

    private void SetDir()
    {
        if (_quadrant == 1)
        {
            _point.x = 0;
            _point.z = _forward;
            _weightPoint.z = -0.87f;
            _quadrant++;
        }
        else if (_quadrant == 2)
        {
            _point.x = _left;
            _point.z = 0;
            _weightPoint.x = 0.5f;
            _quadrant++;
        }
        else if (_quadrant == 3)
        {
            _point.x = 0;
            _point.z = _back;
            _weightPoint.z = 0.87f;
            _quadrant++;
        }
        else if (_quadrant == 4)
        {
            _point.x = _right;
            _point.z = 0;
            _weightPoint.x = -0.5f;
            _quadrant = -1;
        }
    }

    private void Spwan()
    {
        GameObject _object = Instantiate(_mapBlocks[_index], _point, Quaternion.identity);
        _object.transform.SetParent(_mapRoot.transform);


        Vector3 dir = Vector3.zero;
        if (_weightPoint.x > 0)
        {
            if (_weightPoint.z > 0)
            {
                dir.x = 1;
            }
            else
            {
                dir.x = -1;
            }
        }
        else
        {
            if (_weightPoint.z > 0)
            {
                dir.x = 1;
            }
            else
            {
                dir.x = -1;
            }
        }

        for(int i = 1; i < 5; i++)
        {
            GameObject _object2 = Instantiate(_mapBlocks[_index], _point + (dir * i), Quaternion.identity);
            _object2.transform.SetParent(_mapRoot.transform);
        }

        _index = (_index + 1) % _mapBlocks.Count;

        Debug.Log(_point + "\n" + _weightPoint);
    }
}
