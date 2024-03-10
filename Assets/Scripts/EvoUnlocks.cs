using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EvoUnlocks : MonoBehaviour
{
    public string fileName = "integer.txt";

    void Start()
    {    
        string initialFilePath = Path.Combine(Application.dataPath, fileName);
        Debug.Log("data path: " + initialFilePath);

        if (!File.Exists(initialFilePath))
        {
            Debug.Log("File already exists" + initialFilePath);
        }

    }   

}
