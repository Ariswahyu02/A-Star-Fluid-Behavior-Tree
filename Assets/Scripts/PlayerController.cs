using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerControls playerControls;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Camera tpp;
    public float speed = 5f;
    Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        ReadInput();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tpp.gameObject.SetActive(!tpp.gameObject.activeSelf);
            cam.gameObject.SetActive(!cam.gameObject.activeSelf);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(input.x * speed, 0f, input.y * speed);
        rb.velocity = movement;


    }


    private void ReadInput()
    {
        input = playerControls.Movement.Move.ReadValue<Vector2>();
    }
}
