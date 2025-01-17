using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Recycled script from older project.
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]

    public float acceleration;
    public float maxSpeed;
    public float drag;
    public float linearDrag;

    public float fallSpeed;
    public float rotateSpeed;


    [Header("Input")]

    public Vector2 inputMove;


    [Header("Fighting")]

    public float health = 5;
    public float maxHealth = 5;


    [Header("Values")]

    public Vector3 velocity;
    public Vector3 groundNormal;
    Vector3 lastPhysicsPosition;
    bool grounded;

    int invincibilityFrames;


    [Header("References")]

    CharacterController control;
    CameraHandler cam;

    public Transform interpModel;

    LayerMask layersGround;


    [Header("Collision")]

    RaycastHit rayGround;


    void Awake()
    {
        control = GetComponent<CharacterController>();

        cam = Camera.main.GetComponent<CameraHandler>();
        cam.player = interpModel;

        layersGround = LayerMask.GetMask("Ground");

        interpModel.parent = null;
        lastPhysicsPosition = transform.position;
    }

    void FixedUpdate()
    {
        lastPhysicsPosition = transform.position;

        Move();

        DetectRamp();

        Invincibility();
    }

    void Update()
    {
        //Interpolation, as CharacterController itself does not offer it. (and I am picky about locked framerates).
        //Why does unity not have a physics fraction method like godot??
        float inbetweenPhysics = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        interpModel.position = Vector3.Lerp(lastPhysicsPosition, transform.position, inbetweenPhysics);

        Vector3 newDirection = Vector3.RotateTowards(interpModel.forward, new Vector3(control.velocity.x, 0, control.velocity.z), rotateSpeed * Time.deltaTime, 0.0f);
        interpModel.rotation = Quaternion.LookRotation(newDirection);
    }

    void Move()
    {
        Vector2 input = inputMove * acceleration;
        Vector2 vel2D = new Vector2(velocity.x, velocity.z);

        vel2D *= drag;

        vel2D += input;

        Vector2 velNorm = vel2D.normalized;

        if (vel2D.sqrMagnitude > maxSpeed * maxSpeed)
        {
            vel2D = velNorm * maxSpeed;
        }

        if (vel2D.sqrMagnitude > linearDrag * linearDrag)
        {
            vel2D -= velNorm * linearDrag;
        }
        else
        {
            vel2D = Vector2.zero;
        }

        velocity = new Vector3(vel2D.x, velocity.y, vel2D.y);
        control.Move(velocity * Time.fixedDeltaTime);
    }

    void DetectRamp()
    {
        Ray groundRay = new Ray(transform.position, -transform.up * (control.height / 2 + .3f));
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.blue);

        grounded = Physics.Raycast(groundRay, out rayGround, 100, layersGround);
        if (grounded)
        {
            groundNormal = rayGround.normal;
            transform.position = rayGround.point + transform.up * (control.height / 2);
        }
    }

    void Invincibility()
    {
        //Ensures you dont get hit multiple times and slows time down.
        if (invincibilityFrames > 0)
        {
            invincibilityFrames--;

            Time.timeScale = ((51 - (float)invincibilityFrames) / 51 * .9f) + .1f;
        }
    }

    public void DamagePlayer(float damage)
    {
        if (invincibilityFrames > 0) return;

        //1 second in ticks.
        invincibilityFrames = 50;

        health -= damage;

        if (health <= 0)
        {
            health = 0;

            Death();
        }
    }

    void Death()
    {
        enabled = false;

        StartCoroutine(DeathTimer(2));
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Glue"))
        {
            maxSpeed = 2;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Glue"))
        {
            maxSpeed = 4;
        }
    }

    IEnumerator DeathTimer(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
