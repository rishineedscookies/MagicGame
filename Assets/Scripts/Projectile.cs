using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
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

    protected abstract void OnTriggerEnter(Collider other);


    protected abstract void Explode(Collider coll);

}