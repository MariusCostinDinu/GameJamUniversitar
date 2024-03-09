using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Evolution : MonoBehaviour
{
    private float timer = 0f;
    private bool hasReachedEvo1 = true;
    private bool hasReachedEvo2 = true;
    private bool hasReachedEvo3 = true;


    private int evolutionValue2, evolutionValue3;
    private string m_max1, m_max2, m_max3;
    private int m_steroids, m_mutagen, m_carrots, m_nuts;


    HealthManager_SCPT healthManager;
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
            EvoTwoReached();
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
       
    }

    void EvoTwoReached()
    {
        m_steroids = healthManager.data.SteroidLvl;
        m_mutagen = healthManager.data.RadsLvl;
        m_carrots = healthManager.data.CarrotsLvl;
        m_nuts = healthManager.data.NutsLvl;

        CheckHealth();
        Evolution2(m_max1,m_max2);

    }

    void EvoThreeReached()
    {
        m_steroids = healthManager.data.SteroidLvl;
        m_mutagen = healthManager.data.RadsLvl;
        m_carrots = healthManager.data.CarrotsLvl;
        m_nuts = healthManager.data.NutsLvl;

        CheckHealth();
        Evolution3(m_max1, m_max2,m_max3);

    }

    void Evolution1(string evolution1)
    {
        switch (evolution1)
        {
            case "m_steroids":
                healthManager.HungerMaxLevel += evoHunger + evoAValue;                break;
            case "m_mutagen":
                healthManager.HungerMaxLevel += evoHunger + evoBValue;
                healthManager.RadPoinsoningMaxLevel += evoValueAll;
                healthManager.SteroidsMaxLevel += evoValueAll;
                break;
            case "m_carrots":
                healthManager.HungerMaxLevel += evoHunger + evoCValue;                break;
            case "m_nuts":
                healthManager.HungerMaxLevel += evoHunger + evoDValue;                break;
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
                break;
            //AC
            case 10:
                healthManager.HungerMaxLevel += evoHunger + evoACValue; break;
            //AD
            case 1001:
                healthManager.HungerMaxLevel += evoHunger + evoADValue; break;
            //BC
            case 110:
                healthManager.HungerMaxLevel += evoHunger + evoBCValue;
                healthManager.RadPoinsoningMaxLevel += evoValueAll;
                healthManager.SteroidsMaxLevel += evoValueAll;
                break;
            //BD
            case 1010:
                healthManager.HungerMaxLevel += evoHunger + evoBDValue;
                healthManager.RadPoinsoningMaxLevel += evoValueAll;
                healthManager.SteroidsMaxLevel += evoValueAll; 
                break;
            //CD
            case 1100:
                healthManager.HungerMaxLevel += evoHunger + evoCDValue; break;
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
                break;
            //ACD
            case 1011:
                //Mutated Buff Gigabrain
                break;
            //ACD
            case 1101:
                //Buff Big Eyes Big brain
                break;
            //BCD
            case 11100:
                //Mutated, Lots of Eyes, Gigabrain
                break;
            //ABCD?
        }
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