using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodData_SCPT : MonoBehaviour
{

    [SerializeField] string foodtype;
    //public HealthManager_SCPT healthManager;
    HealthManager_SCPT healthManager;

    void Start()
    {
        GameObject healthManagerObj = GameObject.Find("Bunny");
        healthManager = healthManagerObj.GetComponent<HealthManager_SCPT>();

    }

    private void OnMouseDown()
    {
        healthManager.feedBunny(foodtype);
        Destroy(gameObject);
    }


}
