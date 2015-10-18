
class Item
{
    private int _id;
    private int _number;

    public Item(int id, int number)
    {
        _id = id;
        _number = number;
    }

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public int Number
    {
        get { return _number; }
        set { _number = value; }
    }
}

