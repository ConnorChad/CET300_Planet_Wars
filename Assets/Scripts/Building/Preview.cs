using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Preview : NetworkBehaviour
{
    public GameObject prefab;
    public GameObject player;

    private MeshRenderer myRend;
    public Material goodMat;
    public Material badMat;
    public NetworkIdentity nwI;

    private BuildSystem buildSystem;

    private bool isSnapped = false;
    public bool isFoundation = false;  //allows to be placed anywhere

    public List<string> tagsISnapTo = new List<string>();
    private void Start()
    {
        //buildSystem = GameObject.FindObjectOfType<BuildSystem>();
        myRend = GetComponent<MeshRenderer>();
        ChangeColour();
    }
    private void ChangeColour()
    {
        if (isSnapped)
        {
            myRend.material = goodMat;
        }
        else
        {
            myRend.material = badMat;
        }
        if (isFoundation)
        {
            myRend.material = goodMat;
            isSnapped = true;
        }
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
    }
    public bool GetSnapped()
    {
        return isSnapped;
    }

    public void Place(Quaternion targetRot)
    {
        CmdPlace(targetRot);
    }

    [Command]
    public void CmdPlace(Quaternion targetRot)
    {
        GameObject prefabtospawn = Instantiate(prefab, transform.position, targetRot);
        NetworkServer.Spawn(prefabtospawn);
        Destroy(gameObject);
    }
}
