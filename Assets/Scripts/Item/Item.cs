[System.Serializable]
public class Item
{
    public string name;
    public float weight;
    public float price;
    public string description;

    public Item(string name, float weight, float price, string description)
    {
        this.name = name;
        this.weight = weight;
        this.price = price;
        this.description = description;
    }

    public override string ToString()
    {
        return $"{name} (무게: {weight}, 가격: {price})";
    }
}