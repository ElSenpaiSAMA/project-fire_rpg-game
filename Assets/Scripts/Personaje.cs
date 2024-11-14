using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private Rigidbody2D rig;
    private Animator anim;

    private void Awake()
    {
        rig=GetComponent<Rigidbody2D>();
        anim=GetComponentInChildren <Animator>();
    }

    private void FixedUpdate()
    {
        float horizontal =Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rig.velocity= new Vector2(horizontal, vertical) * velocidad;
        anim.SetFloat("CaminarAbajo", Mathf.Abs (rig.velocity.magnitude));
    }

}
