using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : Projectile
{

    [SerializeField]
    Rigidbody body;

    [SerializeField]
    GameObject explosion;

    private float lifetime;

    private float explosionRadius;

    private Damage damage;

    private Status status;

    private float duration;

    [SerializeField]
    private SphereCollider coll;

    private float size;

    private float speed;

    public void Initialize(float lifetime, float explosionRadius, Damage initialDamage, Status status, float size, float speed)
    {

        this.lifetime = lifetime;

        this.explosionRadius = explosionRadius;

        this.damage = initialDamage;

        this.status = status;

        this.size = size;

        this.speed = speed;

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        coll.radius = size;

        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    override
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != GameObject.FindGameObjectWithTag("Player"))
        {

            Collider[] colls = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider coll in colls)
            {

                Explode(coll);

            }

            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(coll.gameObject);
        }
    }

    override
    protected void Explode(Collider coll)
    {


        if (coll.CompareTag("Enemy"))
        {

            EnemyController enemy = coll.GetComponent<EnemyController>();

            Vector3 offset = enemy.transform.position - transform.position;

            float proximity = offset.magnitude;

            float damageMultiplier = 2 - (proximity / explosionRadius);

            ExplosionDamage explosiveDamage = new ExplosionDamage(damageMultiplier * damage.getRawDamage(), duration, DamageTypes.Fire);

            enemy.applyExplosion(explosiveDamage, offset);
            enemy.applyStatus(status);
        }

    }

}

