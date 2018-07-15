using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceballController : Projectile {

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

    private float stunDuration = 1f;

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
            Explode(other);
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

            Damage hitscanDamage = new Damage(damage);

            enemy.applyHitscan(hitscanDamage);
            enemy.applyStun(stunDuration);
        }
    }
}
