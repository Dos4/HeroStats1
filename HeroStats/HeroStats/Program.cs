class Stat
{
    public string Name { get; set; }
    public double Value { get; set; }

    public Stat(string name, double value)
    {
        Name = name;
        Value = value;
    }
}

class Health : Stat
{
    public Health(double value, string name = nameof(Health)) : base(name, value) { }
}

class Damage : Stat
{
    public Damage(double value, string name = nameof(Damage)) : base(name, value) { }
}

class Strength : Stat
{
    public Strength(double value, string name = nameof(Strength)) : base(name, value) { }
}

class Agility : Stat
{
    public Agility(double value, string name = nameof(Agility)) : base(name, value) { }
}

class Intelligence : Stat
{
    public Intelligence(double value, string name = nameof(Intelligence)) : base(name, value) { }
}

class Hero
{
    public string Name { set; get; }
    public int Experience { set; get; }
    public int Level => Experience / 100;
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
        Health.Value = Health.Value + Strength.Value * 1.6;
        Damage.Value = Damage.Value + Agility.Value * 1.2;

        Console.WriteLine($"{Name} with {string.Format("{0:0}", Health.Value)}HP attacked {hero.Name} " +
                          $"with {string.Format("{0:0}", hero.Health.Value)}HP");

        Console.WriteLine($"{hero.Name} -{string.Format("{0:0}", Damage.Value)}hp\n");
        hero.Health.Value -= Damage.Value;

        if (hero.Health.Value <= 0)
        {
            Kill(hero);
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

    private void Kill(Hero hero)
    {
        Console.WriteLine($"{Name} killed {hero.Name}\n");
        Experience += 150;

        Agility.Value += 2 * Level;
        Strength.Value += 2 * Level;
        Intelligence.Value += 2 * Level;

        Console.WriteLine($"{Name} reached {Level} lvl\n");
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
            Console.WriteLine($"{Name} with {string.Format("{0:0}", Health.Value)}hp healed {hero.Name}");
            Health.Value += Intelligence.Value * 2.1;
            Console.WriteLine($"{hero.Name} +{string.Format("{0:0}", Intelligence.Value * 2.1)}hp\n");

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
        Hero axe = new Hero(new Health(100), new Strength(2), new Damage(13), new Agility(4), 
                            new Intelligence(3), "Axe");

        Hero lina = new Hero(new Health(30), new Strength(1), new Damage(13), new Agility(8), 
                             new Intelligence(6), "Lina");

        Healer witchDoctor = new Healer(new Health(100), new Strength(0.5), new Damage(20), new Agility(2), 
                             new Intelligence(7), "Witch Doctor");

        axe.Attack(lina);

        axe.Attack(lina);

        axe.Attack(witchDoctor);

        witchDoctor.Heal(witchDoctor);
    }
}