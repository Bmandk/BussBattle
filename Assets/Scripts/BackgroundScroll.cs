using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _currentScroll;

    private Material _material;

    void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        _currentScroll += _speed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(_currentScroll, 0);
    }
}
