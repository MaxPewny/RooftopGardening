public class PlantCurrency
{
    public PlantType Plant;
    public uint Fruit;
    public uint Seed;

    public PlantCurrency(PlantType GivenType) 
    {
        Plant = GivenType;
        Fruit = 5;
        Seed = 5;
    }
}
