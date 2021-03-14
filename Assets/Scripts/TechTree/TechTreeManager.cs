using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeManager : MonoBehaviour
{
    public GameObject techTreePanel;
    public GameObject myUI;
    public bool techTreeOpen = false;

    public Camera cam;
    public MouseLook mouseLook;

    public Image T1Solar;
    public Image T2Solar;
    public Image T3Solar;
    public int T2SolarCost, T3SolarCost;
    private bool T2SolarUnlocked, T3SolarUnlocked;
    public GameObject UI;

    private void Start()
    {
      
    }

    private void Awake()
    {
        T2SolarUnlocked = false;
        T3SolarUnlocked = false;
       // myUI = Instantiate(UI);
        mouseLook = cam.GetComponent<MouseLook>();
       // techTreePanel = myUI.gameObject.transform.Find("TechTree").gameObject;
        //T1Solar = techTreePanel.gameObject.transform.Find("T1 Solar").GetComponent<Image>();
        //T2Solar = techTreePanel.gameObject.transform.Find("T2 Solar").GetComponent<Image>();
        //T2Solar.GetComponent<Button>().onClick.AddListener(T2Solar_Unlock);
        //T2Solar.GetComponent<Button>().interactable = false;
        //T3Solar = techTreePanel.gameObject.transform.Find("T3 Solar").GetComponent<Image>();

        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            techTreeOpen = !techTreeOpen;
        }

        if (techTreeOpen)
        {
            techTreePanel.SetActive(true);
            mouseLook.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }
        else
        {
            techTreePanel.SetActive(false);
            mouseLook.enabled = true;

        }
    }

    public void T2Solar_Unlock()
    {
        T2Solar.GetComponent<Button>().enabled = false;
        T2Solar.gameObject.transform.Find("Lock").gameObject.SetActive(false);
        T2Solar.gameObject.GetComponent<TechTreeItem>().isLocked = false;
        T2SolarUnlocked = true;
    }
    public void T3Solar_Unlock()
    {
        if (T2SolarUnlocked)
        {
            T3Solar.GetComponent<Button>().enabled = false;
            T3Solar.gameObject.transform.Find("Lock").gameObject.SetActive(false);
            T3Solar.gameObject.GetComponent<TechTreeItem>().isLocked = false;
            T3SolarUnlocked = true;
        }
        
    }

    private void OnMouseOver()
    {
        
    }

}
