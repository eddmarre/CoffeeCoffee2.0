public class Order
{
    private string _size;
    private string _shot;
    private string _espresso;

    private string _syrup;

    private string _beverage;

    private string _temperature;
    private string _milk;


    public Order()
    {
        _size = OrderDictionary.SIZES[0];
        _shot = OrderDictionary.SHOTS[0];
        _espresso = OrderDictionary.ESPRESSOS[0];

        _syrup = OrderDictionary.SYRUPS[0];

        _beverage = OrderDictionary.BEVERAGES[0];

        _temperature = OrderDictionary.TEMPERATURES[0];
        _milk = OrderDictionary.MILKS[0];
    }

    public Order(string size,
        string shots,
        string espresso,
        string syrup,
        string beverage,
        string temperature,
        string milk)
    {
        _size = size;
        _shot = shots;
        _espresso = espresso;

        _syrup = syrup;

        _beverage = beverage;

        _temperature = temperature;
        _milk = milk;
    }
    
    public void SetSize(string size)
    {
        _size = size;
    }

    public void SetShots(string shot)
    {
        _shot = shot;
    }

    public void SetEspresso(string espresso)
    {
        _espresso = espresso;
    }

    public void SetSyrup(string syrup)
    {
        _syrup = syrup;
    }

    public void SetBeverage(string beverage)
    {
        _beverage = beverage;
    }

    public void SetTemperature(string temperature)
    {
        _temperature = temperature;
    }

    public void SetMilk(string milk)
    {
        _milk = milk;
    }

    public string GetSize()
    {
        return _size;
    }

    public string GetShot()
    {
        return _shot;
    }

    public string GetEspresso()
    {
        return _espresso;
    }

    public string GetSyrup()
    {
        return _syrup;
    }

    public string GetBeverage()
    {
        return _beverage;
    }

    public string GetTemperature()
    {
        return _temperature;
    }

    public string GetMilk()
    {
        return _milk;
    }

    public override string ToString()
    {
        return _size + " " + _shot + " " + _espresso + " " + _syrup + " " + _beverage + " " + _temperature + " " +
               _milk;
    }
}