public enum bunnyState
{
    Starving, RadPoinsoning, Satiated
}

[System.Serializable]
public struct HealthData
{
    public int HungerLvl { get; set; }
    public int SteroidLvl { get; set; }
    public int RadsLvl { get; set; }
    public int NutsLvl { get; set; }
    public int CarrotsLvl { get; set; }
    public bunnyState State { get; set; }
}
