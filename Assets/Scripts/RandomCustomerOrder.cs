using Random = System.Random;

public class RandomCustomerOrder
{
    private Random _randomSize = new Random();
    private Random _randomShot = new Random();
    private Random _randomEspresso = new Random();
    private Random _randomSyrup = new Random();
    private Random _randomBeverage = new Random();
    private Random _randomTemperature = new Random();
    private Random _randomMilk = new Random();
    private Random _randomIntro = new Random();
    private Random _randomName = new Random();
    private Random _randomOutro = new Random();

    private int _sizeIndex;
    private int _shotIndex;
    private int _espressoIndex;
    private int _syrupIndex;
    private int _beverageIndex;
    private int _temperatureIndex;
    private int _milkIndex;
    private int _introIndex;
    private int _nameIndex;
    private int _outroIndex;

    private string[] _customerIntros =
        {"Hello there I want a", "Hiya can I have a", "Sup a", "Hola gimme", "Yo", "Lemme get uhhhh"};

    private string[] _femaleNames = {"Shantely", "Nancy", "Lucy", "Sara"};
    private string[] _maleNames = {"Eddie", "Mario", "Derek", "John", "Brian", "Brandon"};


    private string[] _outros = {"Thanks, Bye!", "Yay! Bye!", "Finally, Bye!"};

    public string GetRandomOutro()
    {
        _outroIndex = _randomOutro.Next(_outros.Length);
        
        return _outros[_outroIndex];
    }

    public string GetRandomName(bool isFemale)
    {
        if (isFemale)
        {
            _nameIndex = _randomName.Next(_femaleNames.Length);
            return _femaleNames[_nameIndex];
        }
        else
        {
            _nameIndex = _randomName.Next(_maleNames.Length);
            return _maleNames[_nameIndex];
        }
    }


    public string GetRandomCustomerIntro()
    {
        _introIndex = _randomIntro.Next(_customerIntros.Length);

        return _customerIntros[_introIndex];
    }


    public Order CreateRandomOrder()
    {
        _sizeIndex = _randomSize.Next(OrderDictionary.SIZES.Length);
        _shotIndex = _randomShot.Next(OrderDictionary.SHOTS.Length);
        _espressoIndex = _randomEspresso.Next(OrderDictionary.ESPRESSOS.Length);
        _syrupIndex = _randomSyrup.Next(OrderDictionary.SYRUPS.Length);
        _beverageIndex = _randomBeverage.Next(OrderDictionary.BEVERAGES.Length);
        _temperatureIndex = _randomTemperature.Next(OrderDictionary.TEMPERATURES.Length);
        _milkIndex = _randomMilk.Next(OrderDictionary.MILKS.Length);

        return new Order(
            OrderDictionary.SIZES[_sizeIndex],
            OrderDictionary.SHOTS[_shotIndex],
            OrderDictionary.ESPRESSOS[_espressoIndex],
            OrderDictionary.SYRUPS[_syrupIndex],
            OrderDictionary.BEVERAGES[_beverageIndex],
            OrderDictionary.TEMPERATURES[_temperatureIndex],
            OrderDictionary.MILKS[_milkIndex]
        );
    }
}