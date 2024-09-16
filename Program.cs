using System.Numerics;
using System;

public interface IVisitor
{
    void Visit(Player player);
    void Visit(Enemy enemy);
    void Visit(Item item);
}
public interface IAcceptable
{
    void Accept(IVisitor visitor);
}
public class Creature
{
    protected int Health { get; set; } = 100;
    virtual public void Heal(int amount)
    {
        Health += amount;
    }
    virtual public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
public class Player : Creature, IAcceptable
{

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    override public void Heal(int amount)
    {
        base.Heal(amount);
        Console.WriteLine($"Player healed by {amount}. Current Health: {Health}");
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Console.WriteLine($"Player took {damage} damage. Remaining Health: {Health}");
    }
}

public class Enemy : Creature, IAcceptable
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    override public void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Console.WriteLine($"Enemy took {damage} damage. Remaining Health: {Health}");
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
        Console.WriteLine($"Enemy healed by {amount}. Current Health: {Health}");
    }
}

public class Item : IAcceptable
{
    public int Value { get; set; } = 10;

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void IncrementScore(int points)
    {
        Value += points;
        Console.WriteLine($"Item score increased by {points}. Current Score: {Value}");
    }
}
public class HealPowerUp : IVisitor
{
    public void Visit(Player player)
    {
        player.Heal(20);  // Heal the player by 20
    }

    public void Visit(Enemy enemy)
    {
        enemy.TakeDamage(10);
    }

    public void Visit(Item item)
    {
        Console.WriteLine("Heal power-up has no effect on items.");
    }
}
public class PoisonPowerUp : IVisitor
{
    public void Visit(Player player)
    {
        player.TakeDamage(10);  // Damage the player by 10
    }

    public void Visit(Enemy enemy)
    {
        enemy.Heal(20);  // Damage the enemy by 20
    }

    public void Visit(Item item)
    {
        Console.WriteLine("Poison power-up has no effect on items.");
    }
}
public class ScorePowerUp : IVisitor
{
    public void Visit(Player player)
    {
        Console.WriteLine("Score power-up has no effect on players.");
    }

    public void Visit(Enemy enemy)
    {
        Console.WriteLine("Score power-up has no effect on enemies.");
    }

    public void Visit(Item item)
    {
        item.IncrementScore(50);  // Increase the item score by 50
    }
}
public class Game
{
    public static void Main(string[] args)
    {
        // Create game entities
        Player player = new Player();
        Enemy enemy = new Enemy();
        Item item = new Item();

        // Create power-ups (visitors)
        HealPowerUp healPowerUp = new HealPowerUp();
        PoisonPowerUp poisonPowerUp = new PoisonPowerUp();
        ScorePowerUp scorePowerUp = new ScorePowerUp();

        // Apply healing power-up
        player.Accept(healPowerUp);
        enemy.Accept(healPowerUp);
        item.Accept(healPowerUp);

        // Apply poison power-up
        player.Accept(poisonPowerUp);
        enemy.Accept(poisonPowerUp);
        item.Accept(poisonPowerUp);

        // Apply score increment power-up
        player.Accept(scorePowerUp);
        enemy.Accept(scorePowerUp);
        item.Accept(scorePowerUp);
    }
}