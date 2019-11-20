public class PlantCurrency
{
    public PlantType Plant;
    public uint Fruit;
    public uint Seed;

    public PlantCurrency(PlantType GivenType) 
    {
        Plant = GivenType;
        Fruit = 0;
        Seed = 0;
    }
}
