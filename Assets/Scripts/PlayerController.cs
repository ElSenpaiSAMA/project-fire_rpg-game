using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private Sprite spriteW; // Sprite para moverse arriba
    [SerializeField] private Sprite spriteS; // Sprite para moverse abajo
    [SerializeField] private Sprite spriteA; // Sprite para moverse a la izquierda
    [SerializeField] private Sprite spriteD; // Sprite para moverse a la derecha

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
        ChangeSpriteBasedOnMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void ChangeSpriteBasedOnMovement()
    {
        // Cambiar sprite basado en la dirección del movimiento
        if (movement.y > 0)
        {
            spriteRenderer.sprite = spriteW; // Movimiento hacia arriba
        }
        else if (movement.y < 0)
        {
            spriteRenderer.sprite = spriteS; // Movimiento hacia abajo
        }
        else if (movement.x < 0)
        {
            spriteRenderer.sprite = spriteA; // Movimiento hacia la izquierda
        }
        else if (movement.x > 0)
        {
            spriteRenderer.sprite = spriteD; // Movimiento hacia la derecha
        }
    }
}


