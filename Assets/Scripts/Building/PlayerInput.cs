using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInput : NetworkBehaviour
{

    public GameObject miner; //preview
    public GameObject solarPanel;
    public GameObject miner2;
    public TechTreeManager techTree;
    public bool isBuilding = false;

    public BuildSystem buildSystem;

    PlayerInventory inv;

    public GameObject[] itemSlots;

    [Client]
    private void Start()
    {
        inv = GetComponent<PlayerInventory>();
    }


    

    void Update()
    {
        
        if (!hasAuthority) { return; }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !buildSystem.isBuilding)
        {
            Debug.Log("TEST2");
            isBuilding = true;
            buildSystem.cmdNewBuild();
            //itemSlots[0].gameObject.GetComponent<InvSlot>().item

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !buildSystem.isBuilding)
        {
            isBuilding = true;
            //inv.slots[1].rectTransform.localScale = new Vector2(1.2f, 1.2f);
           // buildSystem.cmdNewBuild(itemSlots[1].gameObject.GetComponent<InvSlot>().item);
            if (inv.totalResources >= inv.solarCost)
            {
                
                //inv.totalResources -= inv.minerCost;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !buildSystem.isBuilding)
        {
            cmdBuild1();
        }




    }

    public override void OnStartAuthority()
    {
    }

    public void BuildSwitch()
    {

    }

    [Command]

    void cmdBuild1()
    {
        Debug.Log("TEST");
        isBuilding = true;
        buildSystem.cmdNewBuild();
    }
}
