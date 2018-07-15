using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float walkSpeed;

    private float health = 50f;

    private float maxHealth = 50f;

    private Rigidbody body;
    private Vector3 movementDirection;

    private CapsuleCollider collider;

    private Elements[] elements = { Elements.Fire };

    [SerializeField]
    private GameObject fireball;

    [SerializeField]
    private GameObject iceball;

    [SerializeField]
    private GameObject airball;

    

    private float fireballRadius = .75f;

    private float fireballSize = .25f;

    private float fireballSpeed = 15f;

    private float fireballFirerate = .5f;

    private float fireballCooldown = 0f;

    private Damage fireballDamage = new Damage(5f, 0, DamageTypes.Fire);

    private Status fireballStatus = new Status(new Damage(2.5f, 0, DamageTypes.Fire), 5f, .5f, 0f, StatusEffects.Burning);

    private float iceballRadius = .75f;

    private float iceballSize = .25f;

    private float iceballSpeed = 15f;

    private float iceballFirerate = .5f;

    private float iceballCooldown = 0f;

    private Damage iceballDamage = new Damage(7f, 0, DamageTypes.Ice);

    private Status iceballStatus = new Status(Damage.zero, 1f, 0, 0, StatusEffects.Frozen);

    private float airballSpeed = 15f;

    private float airballFirerate = .5f;

    private float airballCooldown = 0f;

    [SerializeField]
    private Camera cam;

	// Use this for initialization
	void Start () {
        
        body = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();

	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log("HP: " + health);

        //Player Movement

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        movementDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;

        //Switching elements

        if (Input.GetKey(KeyCode.Alpha1)) {

            elements[0] = Elements.Fire;

        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            elements[0] = Elements.Ice;
        }


        //Attacking

        if (Input.GetMouseButton(0))
        {
            switch(elements[0]) {
                case Elements.Fire:
                    if (fireballCooldown <= 0)
                    {

                        fireballCooldown = fireballFirerate;
                        shootFire();

                    }
                    break;
                case Elements.Ice:
                    if (iceballCooldown <= 0)
                    {

                        iceballCooldown = iceballFirerate;
                        shootIce();

                    }
                    break;
                case Elements.Air:
                    if (airballCooldown <= 0)
                    {

                        airballCooldown = airballFirerate;
                        //shoot();

                    }
                    break;
                    
            }



        }

        fireballCooldown -= Time.deltaTime;
        iceballCooldown -= Time.deltaTime;

	}

    private void FixedUpdate() {
        
        Move();

    }

    private void takeDamage(Damage damage) {

        health -= damage.getRawDamage();

    }

    public float getHealth() {

        return health;

    }

    public float getMaxHealth()
    {

        return maxHealth;

    }

    void Move() {

        //Adding Velocity

        float yVelocity = body.velocity.y;
        body.velocity = movementDirection * walkSpeed * Time.deltaTime;
        body.velocity += transform.up * yVelocity;

    }

    void shootFire() {

        //Shoot Fireball

        FireballController projectile = ObjectFactory.CreateFireball(10f, fireballRadius, fireballDamage, fireballStatus, fireballSize, fireballSpeed);
        projectile.transform.position = transform.position + transform.forward;
        projectile.GetComponent<Rigidbody>().velocity = cam.ScreenPointToRay(Input.mousePosition).direction * fireballSpeed;
    
    }

    void shootIce()
    {

        //Shoot Iceball

        IceballController projectile = ObjectFactory.CreateIceball(10f, iceballRadius, iceballDamage, iceballStatus, iceballSize, iceballSpeed);
        projectile.transform.position = transform.position + transform.forward;
        projectile.GetComponent<Rigidbody>().velocity = cam.ScreenPointToRay(Input.mousePosition).direction * iceballSpeed;

    }

    void checkGround() {
        
        Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1f);
  
    }

    public void applySpell(Status status) {



    }

    public void applyDamage(Damage damage) {

        takeDamage(damage);

    }

}



public enum Elements
{

    Fire = 1,

    Ice = 2,

    Air = 3

}
