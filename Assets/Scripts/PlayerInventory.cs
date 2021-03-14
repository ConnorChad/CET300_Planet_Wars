using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private int resources = 0;
    private int power = 0;
    private float viewRadius = 300f;
    public int totalMiners;
    public int totalSolarPanels;
    public LayerMask minerMask;
    public LayerMask solarMask;
    public int resourceProduction = 0;
    public int totalResources = 100;
    public int totalPower;
    public int remainingPower;
    public int requiredPower;
    public int minerRequiredPower;
    public int minerRequiredPowerTotal;
    public int minerCost = 10;
    public int solarCost = 5;
    [Header("UI")]
    public Sprite defaultSprite;
    public Image[] slots;
    public Text resourcesCount;
    public Text totalPowerCount;
    public GameObject techTree;

    private void Start()
    {
        //InvokeRepeating("AddResources", 0, 10f);
    }
    public void BuildingCheck()
    {
        Collider[] miner = Physics.OverlapSphere(transform.position, viewRadius, minerMask);

        for (int i = 0; i < miner.Length; i++)
        {
           // totalMiners = miner.Length;
           // minerRequiredPowerTotal = minerRequiredPower * miner.Length;

        }

        Collider[] solarPanel = Physics.OverlapSphere(transform.position, viewRadius, solarMask);
        
        for (int i = 0; i < solarPanel.Length; i++)
        {
           // totalSolarPanels = solarPanel.Length;
            //PowerCalculator();
        }
    }

    public void ResourceCalculator()
    {
        resourceProduction = 10 * totalMiners;
    }

    public void PowerCalculator()
    {
        totalPower = 20 * totalSolarPanels;

        requiredPower = minerRequiredPowerTotal;

        remainingPower = totalPower - requiredPower;

        totalPowerCount.text = "Power " + totalPower.ToString() + " Kwh";

    }
    public void AddResources()
    {   
        ResourceCalculator();
        totalResources += resourceProduction;
        resourcesCount.text = "Resources: " + totalResources.ToString();
        
    }
}
