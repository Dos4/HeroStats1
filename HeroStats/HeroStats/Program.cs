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

class Evasion : Stat
{
    public Evasion(string name, double value)
    {
        Name = name;
        Value = value;
    }
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
    public Evasion Evasion { get; set; }

    public Hero(Health health, Strength strength, Damage damage, Agility agility, Intelligence intelligence, 
                Evasion evasion, string name)
    {
        Name = name;
        Health = health;
        Strength = strength;
        Damage = damage;
        Agility = agility;
        Intelligence = intelligence;
        Evasion = evasion;

        Health.Value = health.Value + strength.Value * 1.6;
        Damage.Value = damage.Value + agility.Value * 1.2;

    }

    public void Attack(Hero hero)
    {
        Health.Value = Health.Value + Strength.Value * 1.6;
        Damage.Value = Damage.Value + Agility.Value * 1.2;

        Console.WriteLine($"{Name} with {string.Format("{0:0}", Health.Value)}HP attacked {hero.Name} " +
                          $"with {string.Format("{0:0}", hero.Health.Value)}HP");

        if (ShouldMiss(hero))
        {
            Console.WriteLine("\nMiss\n");
            return;
        }

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

    private bool ShouldMiss(Hero hero)
    {
        int chance = new Random().Next(0, 100);

        return !(chance > hero.Evasion.Value);
    }

    private void CalculateKillEnemyHeroExperience(Hero hero)
    {
        int difference = Level - hero.Level;

        if (Level > hero.Level)
        {
            Experience += 150 / difference;
        }
        else if (Level < hero.Level)
        {
            Experience += 150 * difference;
        }
        else if (Level == hero.Level)
        {
            Experience += 150;
        }
    }

    private void Kill(Hero hero)
    {
        Console.WriteLine($"{Name} killed {hero.Name}\n");
        CalculateKillEnemyHeroExperience(hero);

        Agility.Value += 2 * Level;
        Strength.Value += 2 * Level;
        Intelligence.Value += 2 * Level;

        Console.WriteLine($"{Name} reached {Level} lvl\n");
    }
}

class Healer : Hero
{
    public Healer(Health health, Strength strength, Damage damage, Agility agility, Intelligence intelligence, 
                  Evasion evasion, string name)
         : base(health, strength, damage, agility, intelligence, evasion, name) { }

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
        var axe = new Hero(new ("hp", 100), new ("strength", 2), new ("damage", 13), new ("agility", 4), 
                            new ("intelligence", 3), new ("evasion",40), "Axe");

        var lina = new Hero(new ("hp", 30), new ("strength", 1), new ("damage", 13), new ("agility", 8), 
                             new ("intelligence", 6), new ("evasion",65), "Lina");

        var witchDoctor = new Healer(new ("hp", 100), new ("strength", 0.5), new ("damage", 20), 
                                     new ("agility", 2), new ("intelligence", 7), new ("evasion",35), "Witch Doctor");

        axe.Attack(lina);

        axe.Attack(lina);

        axe.Attack(witchDoctor);

        witchDoctor.Heal(witchDoctor);
    }
}