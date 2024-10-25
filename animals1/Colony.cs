namespace animals1{
public abstract class Colony
{
    public string name { get; }
    public char specie { get; }
    public int num { get; set; }
    public int StartingNum { get; }

    public Colony(string name, char specie, int num)
    {
        this.name = name;
        this.specie = specie;
        this.num = num;
        this.StartingNum = num;
    }

    public abstract bool IsPrey();
    public abstract bool IsPredator();
    public abstract void UpdatePopulation(int turn, int predatorNum);
}



public class Lemming : Colony
{
    public Lemming(string name, int num) : base(name, 'l', num) { }

    public override bool IsPrey() => true;
    public override bool IsPredator() => false;

    public override void UpdatePopulation(int turn, int predatorNum)
    {
        if (turn % 2 == 0)
        {
            num *= 2;
            if (num > 200)
                num = 30;
            num -= 4 * predatorNum;

        }
        else
        {
            num -= 4 * predatorNum;
        }
    }
}

public class Hare : Colony
{
    public Hare(string name, int num) : base(name, 'h', num) { }

    public override bool IsPrey() => true;
    public override bool IsPredator() => false;

    public override void UpdatePopulation(int turn, int predatorNum)
    {
        if (turn % 2 == 0)
        {
            num = (int)(num * 1.5);
            if (num > 100)
                num = 20;
            num -= 2 * predatorNum;
        }
        else
        {
            num -= 2 * predatorNum;
        }
    }
}

public class Gopher : Colony
{
    public Gopher(string name, int num) : base(name, 'g', num) { }

    public override bool IsPrey() => true;
    public override bool IsPredator() => false;

    public override void UpdatePopulation(int turn, int predatorNum)
    {
        if (turn % 4 == 0)
        {
            num *= 2;
            if (num > 200)
                num = 40;
            num -= 2 * predatorNum;

        }
        else
        {
            num -= 2 * predatorNum;
        }
    }
}

public class Fox : Colony
{
    public Fox(string name, int num) : base(name, 'f', num) { }

    public override bool IsPrey() => false;
    public override bool IsPredator() => true;

    public override void UpdatePopulation(int turn, int predatorNum)
    {
        if (turn % 8 == 0)
        {
            num += (num / 4) * 3;
        }
        else if (predatorNum == -1) // Indicating prey shortage
        {
            num -= num / 4;
        }
    }
}

public class Wolf : Colony
{
    public Wolf(string name, int num) : base(name, 'w', num) { }

    public override bool IsPrey() => false;
    public override bool IsPredator() => true;

    public override void UpdatePopulation(int turn, int predatorNum)
    {
        if (turn % 8 == 0)
        {
            num += (num / 4) * 2;
        }
        else if (predatorNum == -1) // Indicating prey shortage
        {
            num -= num / 4;
        }
    }
}

public class Owl : Colony
{
    public Owl(string name, int num) : base(name, 'o', num) { }

    public override bool IsPrey() => false;
    public override bool IsPredator() => true;

    public override void UpdatePopulation(int turn, int predatorNum)
    {
        if (turn % 8 == 0)
        {
            num += num / 4;
        }
        else if (predatorNum == -1) // Indicating prey shortage
        {
            num -= num / 4;
        }
    }
}
}