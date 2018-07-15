using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    [SerializeField]
    Slider hpSlider;

    [SerializeField]
    Text hpText;

    PlayerController player;

	// Use this for initialization
	void Start () {

        player = this.GetComponent<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {

        float healthPercentage = player.getHealth() / player.getMaxHealth();

        healthPercentage = Mathf.Clamp(healthPercentage, 0f, 1f);

        hpSlider.value = healthPercentage;

        hpText.text = player.getHealth().ToString("#") + "/" + player.getMaxHealth().ToString("#");

	}
}
