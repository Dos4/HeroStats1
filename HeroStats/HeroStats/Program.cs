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
        bool chanceOfMiss = false; 

        Health.Value = Health.Value + Strength.Value * 1.6;
        Damage.Value = Damage.Value + Agility.Value * 1.2;

        Console.WriteLine($"{Name} with {string.Format("{0:0}", Health.Value)}HP attacked {hero.Name} " +
                          $"with {string.Format("{0:0}", hero.Health.Value)}HP");

        chanceOfMiss = ChanceMiss(hero);

        if (chanceOfMiss == false)
        {
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
        else
        {
            Console.WriteLine("\nMiss\n");
        }
    }

    private bool ChanceMiss(Hero hero)
    {
        int chance = 0;
        Random rnd = new Random();
        chance = rnd.Next(0, 100);

        if (chance > hero.Evasion.Value)
        {
            return false;
        }
        else
        {
            return true;
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
        Hero axe = new Hero(new Health("hp", 100), new Strength("strength", 2),
                            new Damage("damage", 13), new Agility("agility", 4), new Intelligence("intelligence", 3),
                            new Evasion("evasion",40), "Axe");

        Hero lina = new Hero(new Health("hp", 30), new Strength("strength", 1),
                             new Damage("damage", 13), new Agility("agility", 8), new Intelligence("intelligence", 6),
                             new Evasion("evasion",65), "Lina");

        Healer witchDoctor = new Healer(new Health("hp", 100), new Strength("strength", 0.5),
                             new Damage("damage", 20), new Agility("agility", 2), new Intelligence("intelligence", 7), 
                             new Evasion("evasion",35), "Witch Doctor");

        axe.Attack(lina);

        axe.Attack(lina);

        axe.Attack(witchDoctor);

        witchDoctor.Heal(witchDoctor);
    }
}