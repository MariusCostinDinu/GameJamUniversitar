using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class EvolutionController : MonoBehaviour
{
    private string fileName = "integer.txt";
    private int numberFromFile;

    ShowHidden ABC, ABD, ACD, BCD;

    void Start()
    {
        GameObject ABCObj = GameObject.Find("ABC");
        ABC = ABCObj.GetComponent<ShowHidden>();

        GameObject ABDObj = GameObject.Find("ABD");
        ABD = ABDObj.GetComponent<ShowHidden>();

        GameObject ACDObj = GameObject.Find("ACD");
        ACD = ACDObj.GetComponent<ShowHidden>();

        GameObject BCDObj = GameObject.Find("BCD");
        BCD = BCDObj.GetComponent<ShowHidden>();


        ReadFromFile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadFromFile()
    {
        string initialFilePath = Path.Combine(Application.dataPath, fileName);

        try
        {
            // Create a StreamReader to read from the file in persistent data path
            using (StreamReader reader = new StreamReader(initialFilePath))
            {
                // Read the integer from the file
                string line = reader.ReadLine();
                numberFromFile = int.Parse(line);
                Debug.Log("Integer read from file: " + numberFromFile);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error reading integer from file: " + ex.Message);
        }

        UnlockImg(numberFromFile);
    }


    void UnlockImg(int unlock)
    {

        switch (unlock)
        {
            //ABC
            case 111:
                // Mutated Buff Lots of eyes
                ABC.ChangeSprite(1);
                break;
            //ABD
            case 1011:
                //Mutated Buff Gigabrain
                ABD.ChangeSprite(1);
                break;
            //ACD
            case 1101:
                ACD.ChangeSprite(1);
                //Buff Big Eyes Big brain
                break;
            //BCD
            case 1110:
                BCD.ChangeSprite(1);
                //Mutated, Lots of Eyes, Gigabrain
                break;

           /* case 1111:
                ABC.ChangeSprite(1);
                //Mutated, Lots of Eyes, Gigabrain
                break;
                //ABCD?*/
        }
    }
}
