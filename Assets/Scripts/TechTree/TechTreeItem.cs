using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechTreeItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Item Components")]
    public Sprite itemIcon;
    public GameObject item;
    public bool isT1;
    public bool isLocked;
    [Header("Inventory Slots")]
    public GameObject invSlot1;
    public GameObject invSlot2;
    public GameObject invSlot3;

    private bool mouseOver = false;

    private void Awake()
    {
    }
    void Start()
    {
        if (isT1)
        {
            isLocked = false;
        }
        else
        {
            isLocked = true;
        }
    }

    void Update()
    {
        if (!isLocked) 
        { 
            if (mouseOver)
            {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                invSlot1.GetComponent<Image>().sprite = itemIcon;
                invSlot1.GetComponent<InvSlot>().item = item;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                invSlot2.GetComponent<Image>().sprite = itemIcon;
                invSlot2.GetComponent<InvSlot>().item = item;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {

            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {

            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {

            }
        }
    }
    }

    private void OnMouseOver()
    {
        Debug.Log("MouseOver");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            invSlot1.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("MouseOver");
        mouseOver = true;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
