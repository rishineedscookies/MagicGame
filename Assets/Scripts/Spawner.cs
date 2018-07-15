using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    protected static Spawner instance;

    [SerializeField]
    Text levelText;

    [SerializeField]
    Text enemiesLeftText;

    private int enemiesLeft = 5;

    private int numOfEnemies = 5;
    private int enemiesDead = 0;

    private int level = 0;

    private float cooldown = 0f;

    private float timeBetweenSpawns = 4f;

	// Use this for initialization
	void Start () {
        
        nextLevel();

	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(enemiesDead);

        Damage attackDamage = new Damage(10f + level * 1.2f, 0, DamageTypes.Pure);
        float baseSpeed = 3f + level * .25f;
        float explosionResistance = 6f;
        float maxHealth = 50f + level * 2f;
        float attackCooldown = 1f - level * .05f;
        float attackRadius = .75f;

        cooldown -= Time.deltaTime;

        if(cooldown < 0f && enemiesLeft > 0) {
            ObjectFactory.spawnEnemy(attackDamage, baseSpeed, explosionResistance, maxHealth, attackCooldown, attackRadius);
            cooldown = timeBetweenSpawns;
            enemiesLeft--;

        }

        if (enemiesLeft <= 0) {

            nextLevel();

        }



	}

    public void enemyDied() {

        enemiesDead++;

        enemiesLeftText.text = "Enemies Left: " + (numOfEnemies - enemiesDead).ToString();
    }

    private void nextLevel() {

        level++;
        enemiesLeft = level * 2 + 3;
        numOfEnemies = enemiesLeft;
        enemiesDead = 0;
        levelText.text = level.ToString();
        enemiesLeftText.text = "Enemies Left: " + enemiesLeft.ToString();
    }

}
