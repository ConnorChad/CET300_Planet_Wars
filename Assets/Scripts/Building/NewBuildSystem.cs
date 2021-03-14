using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NewBuildSystem : NetworkBehaviour
{
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

    
    //[Client]
    private void Update()
    {
        if (!hasAuthority) { return; }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isBuilding)
        {
            Debug.Log("1 Pressed");
            isBuilding = true;
            NewBuild();
        }
        BuildIt();
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
    }

    

    public void BuildIt()
    {
        if (!hasAuthority) { return; }
        if (Input.GetMouseButtonDown(0) && isBuilding) //build
        {

            CmdBuildIt();
            Debug.Log("Mouse Pressed");

        }
        
    }

   

    public void NewBuild()
    {
        previewGameObject = Instantiate(minerPreview, Vector3.zero, Quaternion.identity);
        previewScript = previewGameObject.GetComponent<Preview>();

        isBuilding = true;
        //previewGameObject.gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
   
    public void CancelBuild()
    {
        Destroy(previewGameObject);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
    }
   [Command]
    public void CmdBuildIt()
    {
        if (!hasAuthority) { return; }
        GameObject test = Instantiate(previewGameObject, this.transform.position, Quaternion.identity);
        NetworkServer.Spawn(test, connectionToClient);
        //test.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
       // previewScript.placeObj(targetRotation);

        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
    }
    
    public void PauseBuild(bool value)
    {
        pauseBuilding = value;
    }
   
    public void DoBuildRay()
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
