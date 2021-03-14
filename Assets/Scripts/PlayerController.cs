using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed;
    private Vector3 moveDir;
    public Rigidbody rb;
    public Camera playerCam;
    public GameObject cube;
    public Transform cubeSpawn;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isGrounded;
    private float jumpYes = 0;

    [Header("Build System")]
    public GameObject minerPreview;

    public Camera cam;
    public LayerMask layer;
    public GameObject previewGameObject = null;
    private Preview previewScript = null;
    public Quaternion targetRotation;
    public GameObject networkedObj = null;

    public float stickTolerance = 1.5f;

    public bool isBuilding = false;
    public bool pauseBuilding = false;


    [Client]


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        
        if (!hasAuthority) { return; }
        SpawnObj();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isBuilding = true;
            NewBuild(minerPreview);
        }
        if (Input.GetMouseButtonDown(0) && isBuilding) //build
        {
            BuildIt();
        }
        if (isBuilding)
        {
            if (pauseBuilding)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                //if mouse is moved enough, will unstick
                if (Mathf.Abs(mouseX) >= stickTolerance || Mathf.Abs(mouseY) >= stickTolerance)
                {
                    pauseBuilding = false;
                }
            }
            else
            {
                DoBuildRay();
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpYes = 1;
        }
        else
        {
            jumpYes = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!hasAuthority) { return; }
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        rb.AddForce(transform.up * (jumpYes * 5000f) * Time.deltaTime, ForceMode.Impulse);
    }

    public override void OnStartAuthority()
    {
        playerCam.gameObject.SetActive(true);
    }
    public void SpawnObj()
    {
        if (!hasAuthority) { return; }
        if (Input.GetKeyDown(KeyCode.E))
        {
            cmdSpawnObj();
        }
    }
    public void NewBuild(GameObject go)
    {
        previewGameObject = Instantiate(go, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(previewGameObject);
        previewScript = previewGameObject.GetComponent<Preview>();
        isBuilding = true;
    }


    [Command]
    public void cmdSpawnObj()
    {
        GameObject cube1 = Instantiate(cube, cubeSpawn.position, cubeSpawn.rotation);
        NetworkServer.Spawn(cube1, connectionToClient);
    }

    private void CancelBuild()
    {
        Destroy(previewGameObject);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
    }

    
    private void BuildIt()
    {
        previewScript.Place(targetRotation);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
        //GetComponent<PlayerInput>().isBuilding = false;
    }

    public void PauseBuild(bool value)
    {
        pauseBuilding = value;
    }
    private void DoBuildRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000f, layer))
        {

            Transform planet = hit.transform.GetComponent<Transform>();

            Vector3 gravup = (previewGameObject.transform.position - planet.transform.position).normalized;
            targetRotation = Quaternion.FromToRotation(previewGameObject.transform.up, gravup) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);


            previewGameObject.transform.position = hit.point;
            previewGameObject.transform.rotation = targetRotation;
        }
    }
}
