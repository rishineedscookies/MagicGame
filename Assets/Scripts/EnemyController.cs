using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    private NavMeshAgent agent;

    private Spawner spawner;
  
    private PlayerController player;

    private List<Damage> damageStack = new List<Damage>();

    private List<Status> statusStack = new List<Status>();

    private Damage attackDamage;

    private float baseSpeed;

    private float explosionResistance;

    private float health;

    private float maxHealth;

    private float attackCooldown;

    private float attackRadius;

    private float timeSinceLastAttack = 1f;

    private float stunDuration = 0f;

    private bool isStuned = false;

    private bool canAttack = true;

    public void Initialize(Damage attackDamage, float baseSpeed, float explosionResistance, float maxHealth, float attackCooldown, float attackRadius) {

        this.attackDamage = attackDamage;
        this.baseSpeed = baseSpeed;
        this.explosionResistance = explosionResistance;
        this.maxHealth = maxHealth;
        health = this.maxHealth;
        this.attackCooldown = attackCooldown;
        timeSinceLastAttack = this.attackCooldown;
        this.attackRadius = attackRadius;

    }

	// Use this for initialization
	void Start () {
		
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Spawner>();

	}
	
	// Update is called once per frame
	void Update () {
		

        //Health Handling
        //Debug.Log("HP: " + health);

        if (health < 0)
        {
            spawner.enemyDied();
            Destroy(this.gameObject);

        }

        //Pathfinding
        agent.SetDestination(player.transform.position);

        //Speed
        agent.speed = calculateSpeed();


        //Stun Handling

        canAttack = true;

        if(isStuned) {

            agent.isStopped = true;
            canAttack = false;
            stunDuration -= Time.deltaTime;

        }

        if(stunDuration <= 0) {

            agent.isStopped = false;
            isStuned = false;

        }

        //Attack

        if(canAttack && timeSinceLastAttack >= attackCooldown) {

            if(checkForAttack()) {

                attackPlayer();

            }

        }

        timeSinceLastAttack += Time.deltaTime;
       
	}

    private void LateUpdate()
    {

        List<Damage> damages = new List<Damage>(damageStack);

        damageStack.Clear();
        damageStack.TrimExcess();


        foreach (Damage damage in damages)
        {

            //Debug.Log("DMG: " + damage.getRawDamage() + ", DUR: " + damage.getDuration() + ", TYP: " + damage.getDamageType());

            if (damage.getDuration() < 0)
            {

                break;

            }

            takeDamage(damage.getRawDamage(), damage.getDamageType());
            if (damage.getDuration() - Time.deltaTime >= 0)
            {
                addToDamageStack(damage.nextDamage());
            }
        }

    }

    private void FixedUpdate()
    {

        List<Status> statuses = new List<Status>(statusStack);
        statusStack.Clear();
        statusStack.TrimExcess();

        foreach (Status status in statuses)
        {

            addToDamageStack(status.effectDamage());

            switch(status.getStatus()) {

                case StatusEffects.Burning:
                    break;

                case StatusEffects.Frozen:
                    isStuned = true;
                    break;
            }

            if(status.getDuration() - Time.fixedDeltaTime > 0) {

                addToStatusStack(status.nextStatus());

            }

        }



    }

    private bool checkForAttack() {

        Collider[] colls = Physics.OverlapSphere(transform.position, attackRadius);

        foreach(Collider coll in colls) {

            if(coll.gameObject.CompareTag("Player")) {

                return true;

            }

        }

        return false;

    }

    private void attackPlayer() {

        player.applyDamage(attackDamage);
        timeSinceLastAttack = 0f;
    }

    private float calculateSpeed() {

        return baseSpeed;

    }

    public float getHealth() {
        
        return health;
    
    }

    private void takeDamage(float damage, DamageTypes damageType) {
        
        health -= damage;


    }

    private void addToDamageStack(Damage damage) {

        damageStack.Add(damage);

    }

    private void addToStatusStack(Status status) {

        statusStack.Add(status);

    }

    public void applyExplosion(ExplosionDamage explosionDamage, Vector3 offset) {
        
        addToDamageStack(explosionDamage);

        float proximity = offset.magnitude;

        Vector3 force = offset.normalized * (explosionDamage.getRawDamage() / (proximity * explosionResistance));

        body.AddForce(force, ForceMode.Impulse);

    }

    public void applyStatus(Status status) {

        addToStatusStack(status);

    }

    public void applyHitscan(Damage hitscanDamage) {

        addToDamageStack(hitscanDamage);

    }

    public void applyDPS(DPSDamage damage) {

        addToDamageStack(damage);
    }

    public void applyStun(float duration) {

        isStuned = true;
        if (stunDuration < duration)
        {

            stunDuration = duration;

        }
    }

}
