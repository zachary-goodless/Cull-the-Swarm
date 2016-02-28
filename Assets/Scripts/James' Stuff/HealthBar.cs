using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public int health;
	Image currentHealth;
	public Sprite[] barLevels;

	// Use this for initialization
	void Start () {
		currentHealth = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth.sprite = barLevels [health]; 
	}
}
