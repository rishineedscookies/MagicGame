using UnityEngine;
using System.Collections;

public enum DamageTypes {

    Fire = 1,

    Ice = 2,

    Pure = 3

}

public enum StatusEffects {

    Burning = 1,

    Frozen = 2

}

public class Status
{

    StatusEffects effect;

    Damage rawDamage;

    float duration;

    float iterationTime;

    float timeSinceLastIteration;

    public Status(Damage rawDamage, float duration, float iterationTime, float timeSinceLastIteration, StatusEffects effect)
    {

        this.effect = effect;

        this.rawDamage = rawDamage;

        this.duration = duration;

        this.iterationTime = iterationTime;

        this.timeSinceLastIteration = timeSinceLastIteration;

    }

    public Status nextStatus() {

        return new Status(rawDamage, duration - Time.deltaTime, iterationTime, timeSinceLastIteration, effect);

    }

    public Damage getRawDamage()
    {

        return rawDamage;

    }

    public float getDuration()
    {

        return duration;

    }

    public float getIterationTime() {

        return iterationTime;

    }

    public StatusEffects getStatus()
    {
        
        return effect;

    }

    public Damage effectDamage() {
        
        timeSinceLastIteration += Time.fixedDeltaTime;


        Damage damage = new Damage(rawDamage);

        Debug.Log("TSLI: " + timeSinceLastIteration);

        if(timeSinceLastIteration > iterationTime) {

            Debug.Log("Status");
            timeSinceLastIteration = 0f;
            return damage;

        }

        Debug.Log("RETURNED NO DMG");

        return new Damage(0, 0, DamageTypes.Pure);

    }

}

public class Damage
{

    float rawDamage;

    float duration;

    DamageTypes damageType;

    public static Damage zero = new Damage(0f, 0, DamageTypes.Pure);

    public float getRawDamage() {

        return rawDamage;

    }

    public float getDuration() {
        
        return duration;

    }

    public DamageTypes getDamageType() {
        
        return damageType;

    }

    public string toString() {

        return "[" + rawDamage + ", " + duration + ", " + damageType + "]";

    }

    public  Damage(float damage, float duration, DamageTypes damageType) {
     
        this.rawDamage = damage;

        this.duration = duration;

        this.damageType = damageType;

    }

    public Damage(Damage damage) {
        
        this.rawDamage = damage.rawDamage;

        this.duration = damage.duration;

        this.damageType = damage.damageType;

    }

    public Damage nextDamage() {

        return new Damage(rawDamage, duration - Time.deltaTime, damageType);

    }

}

public class FireDamage : Damage {

    float rawDamage;

    float duration;

    DamageTypes damageType = DamageTypes.Fire;

    public FireDamage(float damage, float duration):base(damage, duration, DamageTypes.Fire) {

        this.rawDamage = damage;

        this.duration = duration;

    }

}

public class IceDamage : Damage
{

    float rawDamage;

    float duration;

    DamageTypes damageType = DamageTypes.Ice;

    public IceDamage(float damage, float duration): base(damage, duration, DamageTypes.Ice)
    {

        this.rawDamage = damage;

        this.duration = duration;

    }

}

public class PureDamage : Damage
{

    float rawDamage;

    float duration;

    DamageTypes damageType = DamageTypes.Pure;

    public PureDamage(float damage, float duration): base(damage, duration, DamageTypes.Pure)
    {

        this.rawDamage = damage;

        this.duration = duration;

    }

}

public class ExplosionDamage : Damage
{

    float rawDamage;

    float duration;

    DamageTypes damageType;

    public ExplosionDamage(float damage, float duration, DamageTypes damageType): base(damage, duration, damageType)
    {

        this.rawDamage = damage;

        this.duration = duration;

        this.damageType = damageType;

    }

}

public class HitscanDamage : Damage
{

    float rawDamage;

    float duration;

    DamageTypes damageType;

    public HitscanDamage(float damage, float duration, DamageTypes damageType): base(damage, duration, damageType)
    {

        this.rawDamage = damage;

        this.duration = duration;

        this.damageType = damageType;

    }

}

public class DPSDamage : Damage
{

    float rawDamage;

    float duration;

    DamageTypes damageType;

    public DPSDamage(float damage, float duration, DamageTypes damageType): base(damage, duration, damageType)
    {

        this.rawDamage = damage;

        this.duration = duration;

        this.damageType = damageType;

    }

}