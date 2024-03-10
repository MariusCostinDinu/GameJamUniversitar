using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.IO;


public class Evolution : MonoBehaviour
{
    private string fileName = "integer.txt";
    private float timer = 0f;
    private int nrTime, stage = 0;
    private bool hasReachedEvo1 = true;
    private bool hasReachedEvo2 = true;
    private bool hasReachedEvo3 = true;

    public TMP_Text winText;
    public TMP_Text StageText;
    public TMP_Text CDText;

    private int evolutionValue2, evolutionValue3;
    private string m_max1, m_max2, m_max3;
    private int m_steroids, m_mutagen, m_carrots, m_nuts;


    HealthManager_SCPT healthManager;
    EvolutionForms m_EvolutionForms;

    [SerializeField] float firstEvoTime = 100.0f;
    [SerializeField] float secondEvoTime = 200.0f;
    [SerializeField] float thridEvoTime = 300.0f;

    [Header("Evolution 1 Values")]
    [SerializeField] int evoHunger = 25;
    [SerializeField] int evoValueAll = 10;

    [SerializeField] int evoAValue = 30;
    [SerializeField] int evoBValue = 35;

    [SerializeField] int evoCValue = 50;
    [SerializeField] int evoDValue = 40;

    [Header("Evolution 2 Values")]
    [SerializeField] int evoABValue = 35;
    [SerializeField] int evoACValue = 30;
    [SerializeField] int evoADValue = 20;
    [SerializeField] int evoBCValue = 60;
    [SerializeField] int evoBDValue = 50;
    [SerializeField] int evoCDValue = 55;

    void Start()
    {
        GameObject healthManagerObj = GameObject.Find("Bunny");
        healthManager = healthManagerObj.GetComponent<HealthManager_SCPT>();

        GameObject m_EvolutionFormsObj = GameObject.Find("BunnySprite");
        m_EvolutionForms = m_EvolutionFormsObj.GetComponent<EvolutionForms>();
    }


    void CheckHealth()
    {
        Dictionary<string, int> variableDict = new Dictionary<string, int>();
        variableDict.Add("m_steroids", m_steroids);
        variableDict.Add("m_mutagen", m_mutagen);
        variableDict.Add("m_carrots", m_carrots);
        variableDict.Add("m_nuts", m_nuts);

        var sortedDict = variableDict.OrderByDescending(x => x.Value).ToList();

        KeyValuePair<string, int> maxPair = sortedDict[0];
        KeyValuePair<string, int> maxSecPair = sortedDict[1];
        KeyValuePair<string, int> maxThirdPair = sortedDict[2];

        m_max1 = maxPair.Key;
        m_max2 = maxSecPair.Key;
        m_max3 = maxThirdPair.Key;
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (stage)
        {
            case 0:
                nrTime = (int)firstEvoTime - (int)timer;
                CDText.text = nrTime.ToString();                
                break;
            case 1:
                nrTime = (int)secondEvoTime - (int)timer;
                CDText.text = nrTime.ToString();
                break;
            case 2:
                nrTime = (int)thridEvoTime - (int)timer;
                CDText.text = nrTime.ToString();
                break;
            case 3:
                CDText.text = " ";
                break;

        }
    
        


        if (timer >= firstEvoTime && hasReachedEvo1)
        {
           EvoOneReached();
            Debug.Log("EVOLUTION 1 REACHED");
            
            hasReachedEvo1 = false;           
        }

        if (timer >= secondEvoTime && hasReachedEvo2)
        {
            EvoTwoReached();
            Debug.Log("EVOLUTION 2 REACHED");
            hasReachedEvo2 = false;            
        }

        if (timer >= thridEvoTime && hasReachedEvo3)
        {
            EvoThreeReached();
            Debug.Log("EVOLUTION 3 REACHED");
            hasReachedEvo3 = false;            
        }
    }

    void EvoOneReached()
    {
        m_steroids = healthManager.data.SteroidLvl;
        m_mutagen = healthManager.data.RadsLvl;
        m_carrots = healthManager.data.CarrotsLvl;
        m_nuts = healthManager.data.NutsLvl;

        CheckHealth();
        Evolution1(m_max1);
        stage = 1;
        StageText.text = "Stage 1";
    }

    void EvoTwoReached()
    {
        m_steroids = healthManager.data.SteroidLvl;
        m_mutagen = healthManager.data.RadsLvl;
        m_carrots = healthManager.data.CarrotsLvl;
        m_nuts = healthManager.data.NutsLvl;

        CheckHealth();
        Evolution2(m_max1,m_max2);
        stage = 2;
        StageText.text = "Stage 2";

    }

    void EvoThreeReached()
    {
        m_steroids = healthManager.data.SteroidLvl;
        m_mutagen = healthManager.data.RadsLvl;
        m_carrots = healthManager.data.CarrotsLvl;
        m_nuts = healthManager.data.NutsLvl;

        CheckHealth();
        Evolution3(m_max1, m_max2,m_max3);
        stage = 3;
        StageText.text = " ";

    }

    void Evolution1(string evolution1)
    {
        switch (evolution1)
        {
            case "m_steroids":
                healthManager.HungerMaxLevel += evoHunger + evoAValue;
                m_EvolutionForms.ChangeSprite(1);
                break;
            case "m_mutagen":
                healthManager.HungerMaxLevel += evoHunger + evoBValue;
                healthManager.RadPoinsoningMaxLevel += evoValueAll;
                healthManager.SteroidsMaxLevel += evoValueAll;
                m_EvolutionForms.ChangeSprite(2);
                break;
            case "m_carrots":
                healthManager.HungerMaxLevel += evoHunger + evoCValue;                
                m_EvolutionForms.ChangeSprite(3); break;
            case "m_nuts":
                healthManager.HungerMaxLevel += evoHunger + evoDValue;               
                m_EvolutionForms.ChangeSprite(4); break;
        }
    }

    void Evolution2(string evolution1, string evolution2)
    {
        evolutionValue2 = EvolutionCheck(evolution1);
        evolutionValue2 += EvolutionCheck(evolution2);

        switch (evolutionValue2) {
            //AB
            case 11:
                healthManager.HungerMaxLevel += evoHunger + evoABValue;
                healthManager.RadPoinsoningMaxLevel += evoValueAll;
                healthManager.SteroidsMaxLevel += evoValueAll;
                m_EvolutionForms.ChangeSprite(5);
                break;
            //AC
            case 101:
                healthManager.HungerMaxLevel += evoHunger + evoACValue;
                m_EvolutionForms.ChangeSprite(6); break;
            //AD
            case 1001:
                healthManager.HungerMaxLevel += evoHunger + evoADValue;
                m_EvolutionForms.ChangeSprite(7); 
                break;
            //BC
            case 110:
                healthManager.HungerMaxLevel += evoHunger + evoBCValue;
                healthManager.RadPoinsoningMaxLevel += evoValueAll;
                healthManager.SteroidsMaxLevel += evoValueAll;
                m_EvolutionForms.ChangeSprite(8);
                break;
            //BD
            case 1010:
                healthManager.HungerMaxLevel += evoHunger + evoBDValue;
                healthManager.RadPoinsoningMaxLevel += evoValueAll;
                healthManager.SteroidsMaxLevel += evoValueAll;
                m_EvolutionForms.ChangeSprite(9);
                break;
            //CD
            case 1100:
                healthManager.HungerMaxLevel += evoHunger + evoCDValue;
                m_EvolutionForms.ChangeSprite(10); break;
        }
    }

    void Evolution3(string evolution1, string evolution2, string evolution3)
    {
        evolutionValue3 = EvolutionCheck(evolution1);
        evolutionValue3 += EvolutionCheck(evolution2);
        evolutionValue3 += EvolutionCheck(evolution3);

        switch (evolutionValue3)
        {
            //ABC
            case 111:
                // Mutated Buff Lots of eyes
                WriteToFile(111);
                m_EvolutionForms.ChangeSprite(11);
                break;
            //ACD
            case 1011:
                //Mutated Buff Gigabrain
                WriteToFile(1011);
                m_EvolutionForms.ChangeSprite(12);
                break;
            //ACD
            case 1101:
                WriteToFile(1101);
                m_EvolutionForms.ChangeSprite(13);
                //Buff Big Eyes Big brain
                break;
            //BCD
            case 1110:
                WriteToFile(1110);
                m_EvolutionForms.ChangeSprite(14);
                //Mutated, Lots of Eyes, Gigabrain
                break;

            case 1111:
                WriteToFile(1111);
                m_EvolutionForms.ChangeSprite(15);
                //Mutated, Lots of Eyes, Gigabrain
                break;
                //ABCD?
        }
        Invoke("MainMenu", 3f);
        winText.text = "EVOLUTION COMPLETE";
    }


    public void WriteToFile(int numberToWrite)
    {
        // Get the persistent data path for the platform
        string initialFilePath = Path.Combine(Application.dataPath, fileName);

        try
        {
            using (StreamWriter writer = new StreamWriter(initialFilePath))
            {
                writer.WriteLine(numberToWrite);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error writing integer to file: " + ex.Message);
        }
    }

    void MainMenu()
    {
        
        SceneManager.LoadScene("Evolutions");
    }
    public int EvolutionCheck(string evoChek) {
        int nrCheck = 0;
        switch (evoChek)
        {
            case "m_steroids":
                nrCheck += 1;               break;
            case "m_mutagen":
                nrCheck += 10;               break;
            case "m_carrots":
                nrCheck += 100;               break;
            case "m_nuts":
                nrCheck += 1000;               break;
        }
        return nrCheck;
    }
}
