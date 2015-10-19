
class ItemData
{
    private int _id = 0;
    private int _amount = 0;

    public ItemData(int id, int amount)
    {
        _id = id;
        _amount = amount;
    }

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
}

