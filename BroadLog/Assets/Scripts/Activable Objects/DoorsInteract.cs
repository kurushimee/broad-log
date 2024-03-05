using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsInteract : MonoBehaviour, IActivable
{
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    private Animator _animator;

    SpriteRenderer SR;
    BoxCollider2D bColl;
    [SerializeField] Transform player;
    [SerializeField] bool moveUp;
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        bColl = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 distance = player.position - transform.position;
        if (distance.magnitude > 3f)
        {
            _animator.SetBool(IsOpen, false);
            bColl.enabled = true;
        }

    }
    public void ShowMe()
    {
        SR.color = Color.green;
    }

    public void DontShowMe()
    {
        SR.color = Color.white;
    }

    public void Interact()
    {
        _animator.SetBool(IsOpen, true);
    }

    void DoorIsOpen()
    {
        bColl.enabled = false;
    }
}
