using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class Player : NetworkBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3();
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float groundDistance;
    [SerializeField] private float gravity = -9.81f;
    public LayerMask groundMask;
    private bool isGrounded;
    public CharacterController cc;
    public Camera playerCam;
    public GameObject cube;
    public Transform cubeSpawn;
    public Transform groundCheck;
    Vector3 velocity;
    NetworkManagerPlanetWars networkM;

    [Client]
 
    private void Update()
    {
        if (!hasAuthority) { return; }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SpawnObj();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }

    public void SpawnObj()
    {
        if (!hasAuthority) { return; }
        if (Input.GetKeyDown(KeyCode.E))
        {

            cmdSpawnObj();
            
           
        }
    }
    public override void OnStartAuthority()
    {
        playerCam.gameObject.SetActive(true);
    }

    [Command]

    void cmdSpawnObj()
    {
        GameObject cube1 =   Instantiate(cube, cubeSpawn.position, cubeSpawn.rotation);
        NetworkServer.Spawn(cube1);
    }
}
