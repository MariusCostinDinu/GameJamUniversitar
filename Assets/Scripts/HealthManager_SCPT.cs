using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthManager_SCPT : MonoBehaviour
{
    [SerializeField]
    public HealthData data;
    public List<string> listWithFoodsEaten = new List<string>();

    public int HungerMaxLevel;
    public int HungerMinLevel;
    public int RadPoinsoningMaxLevel;
    public int RadPoinsoningMinLevel;
    public int SteroidsMaxLevel;
    public int SteroidsMinLevel;

    public int HungerModifier;
    public int SteroidsModifier;
    public int RadiationsModifier;

    public TextMeshProUGUI statsUI;
    public Slider HungerSlider;
    public Slider SteroidsSlider;
    public Slider RadiationsSlider;

    private void Awake()
    {
        //Starting Stats
        data.HungerLvl = 35;
        data.RadsLvl = 0;
        data.SteroidLvl = 0;
        data.NutsLvl = 0;
        data.CarrotsLvl = 0;
        data.State = bunnyState.Starving;

        //Decreasing Stats
        InvokeRepeating("bunnyNaturalHunger", 0, UnityEngine.Random.Range(1.0f, 3.0f));
        InvokeRepeating("bunnyNaturalRadsAbsorbtion", 0, UnityEngine.Random.Range(3.0f, 5.0f));
        InvokeRepeating("bunnyNaturalSteroidsAbsortion", 0, UnityEngine.Random.Range(4.0f, 6.0f));
    }

    public void feedBunny(string foodType)
    {
        switch (foodType)
        {
            case "Steroids":
                /* Increase the steroids level */
                if ((data.SteroidLvl + SteroidsModifier) <= SteroidsMaxLevel) data.SteroidLvl += SteroidsModifier;
                else data.SteroidLvl = SteroidsMaxLevel;

                /* Add the eaten food to list */
                listWithFoodsEaten.Add(foodType);
                break;

            case "Mutagen":
                /* Increase the radiation level */
                if (data.State != bunnyState.RadPoinsoning)
                {
                    if ((data.RadsLvl + RadiationsModifier) <= RadPoinsoningMaxLevel)
                    {
                        data.RadsLvl += RadiationsModifier;
                    }
                    else data.RadsLvl = RadPoinsoningMaxLevel;

                    /* Add the eaten food to list */
                    listWithFoodsEaten.Add(foodType);
                }
                break;

            case "Carrots":
                /* Adds Huger, Adds Carrots only if not in the RadPoisoning and Satiated state */
                if (data.State == bunnyState.Starving)
                {
                    if ((data.HungerLvl + HungerModifier) <= HungerMaxLevel) data.HungerLvl += HungerModifier;
                    else data.HungerLvl = HungerMaxLevel;
                    data.CarrotsLvl += 25;

                    /* Add the eaten food to list */
                    listWithFoodsEaten.Add(foodType);
                }
                break;

            case "Nuts":
                /* Adds Huger, Adds Nuts only if not in the RadPoisoning and Satiated state */
                if (data.State == bunnyState.Starving)
                {
                    if ((data.HungerLvl + HungerModifier) <= HungerMaxLevel) data.HungerLvl += HungerModifier;
                    else data.HungerLvl = HungerMaxLevel;
                    data.NutsLvl += 25;

                    /* Add the eaten food to list */
                    listWithFoodsEaten.Add(foodType);
                }
                break;

            case "Laxatives":
                /* Halfs the carrots and nuts eaten */
                data.CarrotsLvl /= 2;
                data.NutsLvl /= 2;
                break;

            case "Ipecac":
                /* Halfs hunger level */
                data.HungerLvl /= 2;
                break;

            case "Soy":
                /* Halfs steroid level */
                data.SteroidLvl /= 2;
                break;

            case "Iod":
                /* Halfs rad poisoning level */
                data.RadsLvl /= 2;
                break;

            default:
                break;
        }
    }

    private void checkBunnyState()
    {
        if(data.HungerLvl >= HungerMaxLevel)
        {
            /* Stop the bunny from eating */
            data.State = bunnyState.Satiated;
        }
        else if(data.HungerLvl <= HungerMinLevel)
        {
            /* Allow the bunny to eat again, if not radiated */
            if(data.State != bunnyState.RadPoinsoning)
            data.State = bunnyState.Starving;
        }

        if (data.RadsLvl >= RadPoinsoningMaxLevel)
        {
            /* Food has no effect */
            data.State = bunnyState.RadPoinsoning;
        }
        else if(data.RadsLvl <= RadPoinsoningMinLevel)
        {
            /* Allows the bunny to eat again, if not already full */
            if (data.HungerLvl >= RadPoinsoningMaxLevel) data.State = bunnyState.Satiated;
            else if (data.HungerLvl <= HungerMinLevel) data.State = bunnyState.Starving;
        }
    }

    private void bunnyNaturalHunger()
    {
        /* Decrease hunger overtime */
        if (data.HungerLvl > 0) data.HungerLvl -= UnityEngine.Random.Range(1, 3);
    }

    private void bunnyNaturalRadsAbsorbtion()
    {
        /* Decrease radiation overtime */
        if(data.RadsLvl > 0) data.RadsLvl -= UnityEngine.Random.Range(1, 2);
    }

    private void bunnyNaturalSteroidsAbsortion()
    {
        /* Decrease steroids overtime */
        if (data.SteroidLvl > 0) data.SteroidLvl -= UnityEngine.Random.Range(1, 2);
    }

    private void updateDisplayedStats()
    {
        string bunnyStateDisplay ="";

        if (data.State == bunnyState.Starving) bunnyStateDisplay = "feeling Hungry";
        else if (data.State == bunnyState.Satiated) bunnyStateDisplay = "feeling Satiated";
        else if (data.State == bunnyState.RadPoinsoning) bunnyStateDisplay = "NOT feeling well";

        statsUI.text = "Bunny is " + bunnyStateDisplay + "!";

        HungerSlider.maxValue = HungerMaxLevel;
        HungerSlider.value = data.HungerLvl;

        SteroidsSlider.maxValue = SteroidsMaxLevel;
        SteroidsSlider.value = data.SteroidLvl;

        RadiationsSlider.maxValue = RadPoinsoningMaxLevel;
        RadiationsSlider.value = data.RadsLvl;
    }

    private void Update()
    {
        /* Check the state of the bunny */
        checkBunnyState();
        /* Update the displayed bunny stats */
        updateDisplayedStats();
    }
}
