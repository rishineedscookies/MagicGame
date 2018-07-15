using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour
{
    protected static ObjectFactory instance; 
    public GameObject fireball;
    public GameObject iceball;
    public GameObject airball;

    public GameObject enemy;

    void Start()
    {
        instance = this;



    }

    public static FireballController CreateFireball(float lifetime, float explosionRadius, Damage initialDamage, Status status, float size, float speed)
    {
        FireballController fireball = Object.Instantiate(instance.fireball, Vector3.zero, Quaternion.identity).GetComponent<FireballController>();
        fireball.Initialize(lifetime, explosionRadius, initialDamage, status, size, speed);
        return fireball;
    }

    public static IceballController CreateIceball(float lifetime, float explosionRadius, Damage initialDamage, Status status, float size, float speed)
    {
        IceballController iceball = Object.Instantiate(instance.iceball, Vector3.zero, Quaternion.identity).GetComponent<IceballController>();
        iceball.Initialize(lifetime, explosionRadius, initialDamage, status, size, speed);
        return iceball;
    }

    public static EnemyController spawnEnemy(Damage attackDamage, float baseSpeed, float explosionResistance, float maxHealth, float attackCooldown, float attackRadius)
    {

        EnemyController enemy = Object.Instantiate(instance.enemy, Vector3.zero, Quaternion.identity).GetComponent<EnemyController>();
        enemy.Initialize(attackDamage, baseSpeed, explosionResistance, maxHealth, attackCooldown, attackRadius);
        return enemy;
    }
}