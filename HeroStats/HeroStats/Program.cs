class Stat
{
    public string Name { get; set; }
    public double Value { get; set; }
}

class Health : Stat
{
    public Health(string name, double value)
    {
        Name = name;
        Value = value;
    }
}

class Damage : Stat
{
    public Damage(string name, double value)
    {
        Name = name;
        Value = value;
    }
}

class Strength : Stat
{
    public Strength(string name, double value)
    {
        Name = name;
        Value = value;
    }
}

class Agility : Stat
{
    public Agility(string name, double value)
    {
        Name = name;
        Value = value;
    }
}

class Intelligence : Stat
{
    public Intelligence(string name, double value)
    {
        Name = name;
        Value = value;
    }
}

class Hero
{
    public string Name { set; get; }
    public Health Health { set; get; }
    public Strength Strength { get; set; }
    public Agility Agility { get; set; }
    public Damage Damage { get; set; }
    public Intelligence Intelligence { get; set; }

    public Hero(Health health, Strength strength, Damage damage, Agility agility, Intelligence intelligence, string name)
    {
        Name = name;
        Health = health;
        Strength = strength;
        Damage = damage;
        Agility = agility;
        Intelligence = intelligence;

        Health.Value = health.Value + strength.Value * 1.6;
        Damage.Value = damage.Value + agility.Value * 1.2;
    }

    public void Attack(Hero hero)
    {
        Console.WriteLine($"{Name} with {string.Format("{0:0}", Health.Value)}HP attacked {hero.Name} " +
                          $"with {string.Format("{0:0}", hero.Health.Value)}HP");

        Console.WriteLine($"{hero.Name} -{string.Format("{0:0}", Damage.Value)}hp\n");
        hero.Health.Value -= Damage.Value;

        if (hero.Health.Value <= 0)
        {
            Console.WriteLine($"{hero.Name} killed by {Name} in the battle");
        }
        else if (Health.Value <= 0)
        {
            Console.WriteLine($"{Name} is dead");
        }
        else
        {
            Console.WriteLine($"{hero.Name} now have {string.Format("{0:0}", hero.Health.Value)}HP\n");
        }

    }
}

class Healer : Hero
{
    public Healer(Health health, Strength strength, Damage damage, Agility agility, Intelligence intelligence, string name)
         : base(health, strength, damage, agility, intelligence, name) { }

    public void Heal(Hero hero)
    {
        if (hero.Health.Value > 0 && Health.Value > 0)
        {
            Console.WriteLine($"{Name} with {string.Format("{0:0}", Health.Value)}hp healed {hero.Name}\n");
            hero.Health.Value += Intelligence.Value * 2.1;

            Console.WriteLine($"{hero.Name} now have {string.Format("{0:0}", hero.Health.Value)}hp");
        }
        else if (Health.Value <= 0)
        {
            Console.WriteLine($"{Name} is dead");
        }
        else if (hero.Health.Value <= 0)
        {
            Console.WriteLine($"{hero.Name} is dead");
        }
    }

}


internal class Program
{
    static void Main(string[] args)
    {
        Hero axe = new Hero(new Health("hp", 100), new Strength("strength", 2),
                            new Damage("damage", 13), new Agility("agility", 4), new Intelligence("intelligence", 3), "Axe");

        Hero lina = new Hero(new Health("hp", 56), new Strength("strength", 1),
                            new Damage("damage", 13), new Agility("agility", 8), new Intelligence("intelligence", 6), "Lina");

        Healer witchDoctor = new Healer(new Health("hp", 100), new Strength("strength", 0.5),
                            new Damage("damage", 20), new Agility("agility", 2), new Intelligence("intelligence", 7), "Witch Doctor");

        axe.Attack(lina);

        axe.Attack(lina);

        witchDoctor.Heal(lina);
    }
}