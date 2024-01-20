using UnityEngine;

public class Ability
{
    // Variables for damage and cooldown
    public int damage;
    public float cooldown;

    // Constructor to initialize the ability with damage and cooldown values
    public Ability(int damage, float cooldown)
    {
        this.damage = damage;
        this.cooldown = cooldown;
    }

    // Common Cast function (can be overridden by subclasses)
    public virtual void Cast()
    {
        Debug.Log("Ability Casted! Damage: " + damage);
        // Common logic for all abilities can go here
    }
}

public class FireballAbility : Ability
{
    // Additional properties specific to Fireball
    public float fireDamage;

    public FireballAbility(int damage, float cooldown, float fireDamage) : base(damage, cooldown)
    {
        this.fireDamage = fireDamage;
    }

    // Override the Cast function for Fireball
    public override void Cast()
    {
        base.Cast(); // Call the common Cast logic from the base class
        Debug.Log("Fireball Casted! Fire Damage: " + fireDamage);
        // Implement Fireball-specific logic here
    }
}

public class IceballAbility : Ability
{
    // Additional properties specific to Iceball
    public float freezeDuration;

    public IceballAbility(int damage, float cooldown, float freezeDuration) : base(damage, cooldown)
    {
        this.freezeDuration = freezeDuration;
    }

    // Override the Cast function for Iceball
    public override void Cast()
    {
        base.Cast(); // Call the common Cast logic from the base class
        Debug.Log("Iceball Casted! Freeze Duration: " + freezeDuration);
        // Implement Iceball-specific logic here
    }
}
