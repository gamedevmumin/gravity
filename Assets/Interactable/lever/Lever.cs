using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool _isPulledLeft = true;

    private bool _isInRange;

    private Animator _animator;

    private static readonly int Pulled = Animator.StringToHash("pulled");

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isInRange || !Input.GetKeyDown(KeyCode.E)) return;
        _animator.SetTrigger(Pulled);
        _isPulledLeft = !_isPulledLeft;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }
}
