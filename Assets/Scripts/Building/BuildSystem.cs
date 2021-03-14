using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BuildSystem : NetworkBehaviour
{
    public Camera cam;
    public LayerMask layer;
    private GameObject previewGameObject = null;
    private Preview previewScript = null;
    public Quaternion targetRotation;
    public GameObject object1;
    PlayerInventory pI;

    public float stickTolerance = 1.5f;

    public bool isBuilding = false;
    private bool pauseBuilding = false;
    [Client]
    private void Start()
    {
        pI = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (!hasAuthority) { return; }
        if (Input.GetKeyDown(KeyCode.R)) //rotate
        {
            previewGameObject.transform.Rotate(0, 90f, 0);
        }
        if (Input.GetKeyDown(KeyCode.G)) //cancel
        {
            cmdCancelBuild();
        }
        if (Input.GetMouseButtonDown(0) && isBuilding) //build
        {
            if (previewScript.GetSnapped())
            {
                cmdBuildIt();
            }
        }
        if (isBuilding)
        {
            if (pauseBuilding)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                //if mouse is moved enough, will unstick
                if (Mathf.Abs(mouseX) >= stickTolerance || Mathf.Abs(mouseY) >=stickTolerance)
                {
                    pauseBuilding = false;
                }
            }
            else
            {
                cmdDoBuildRay();
            }
        }
    }

    public void NewBuild(GameObject go)
    {
        previewGameObject = Instantiate(go, Vector3.zero, Quaternion.identity);
        previewScript = previewGameObject.GetComponent<Preview>();
        isBuilding = true;
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
        previewScript.CmdPlace(targetRotation);

        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
        pI.BuildingCheck();
        GetComponent<PlayerInput>().isBuilding = false;
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
           // float y = hit.point.y + (previewGameObject.transform.localScale.y / 2); //fixes unity anchour point
            //Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
            //previewGameObject.transform.position = pos;

            Transform planet = hit.transform.GetComponent<Transform>();

            Vector3 gravup = (previewGameObject.transform.position - planet.transform.position).normalized;
            targetRotation = Quaternion.FromToRotation(previewGameObject.transform.up, gravup) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
            

            previewGameObject.transform.position = hit.point;
            previewGameObject.transform.rotation = targetRotation;
        }
    }

    [Command]
    public void cmdNewBuild()
    {
        previewGameObject = Instantiate(object1, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(previewGameObject);
        previewScript = previewGameObject.GetComponent<Preview>();
        
        isBuilding = true;
    }
    private void cmdCancelBuild()
    {
        Destroy(previewGameObject);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
    }

    private void cmdBuildIt()
    {
        previewScript.CmdPlace(targetRotation);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
        pI.BuildingCheck();
        GetComponent<PlayerInput>().isBuilding = false;
    }

    public void cmdPauseBuild(bool value)
    {
        pauseBuilding = value;
    }

    public void cmdDoBuildRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000f, layer))
        {
            // float y = hit.point.y + (previewGameObject.transform.localScale.y / 2); //fixes unity anchour point
            //Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
            //previewGameObject.transform.position = pos;

            Transform planet = hit.transform.GetComponent<Transform>();

            Vector3 gravup = (previewGameObject.transform.position - planet.transform.position).normalized;
            targetRotation = Quaternion.FromToRotation(previewGameObject.transform.up, gravup) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);


            previewGameObject.transform.position = hit.point;
            previewGameObject.transform.rotation = targetRotation;
        }
    }
    
   
}
