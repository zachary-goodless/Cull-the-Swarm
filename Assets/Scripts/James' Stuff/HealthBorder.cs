using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBorder : MonoBehaviour {

	public int health;
	public bool fiveHealth;
	Image currentHealth;
	public Sprite[] healthLevels;

	// Use this for initialization
	void Start () {
		currentHealth = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		if (!fiveHealth) {
			currentHealth.sprite = healthLevels [health-1];
		} else {
			switch (health) {
			case 5:
				currentHealth.sprite = healthLevels [4];
				break;
			case 4:
				currentHealth.sprite = healthLevels [2];
				break;
			case 3:
				currentHealth.sprite = healthLevels [1];
				break;
			case 2:
				currentHealth.sprite = healthLevels [3];
				break;
			case 1:
				currentHealth.sprite = healthLevels [0];
				break;
			}
		}
	}
}
