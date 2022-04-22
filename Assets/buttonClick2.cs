using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonClick2 : MonoBehaviour
{
	private GameObject winChoiceUI;
	private Text txt1, rarity1;
	public void clickedButton()
	{
		//gets the text components of reward display screen
		GameObject choice1 = GameObject.Find("Reward2");
		txt1 = choice1.GetComponent<Text>();
		GameObject rchoice1 = GameObject.Find("Rarity2");
		rarity1 = rchoice1.GetComponent<Text>();
		//sets rarity to an integer value
		int rarityIndex;
		if (rarity1.text == "Common")
		{
			rarityIndex = 1;
		}
		else if (rarity1.text == "Rare")
		{
			rarityIndex = 2;
		}
		else
		{
			rarityIndex = 3;
		}

		//gets reward text
		string reward = txt1.text;

		//finds all the required scripts and game objects
		GameObject manager = GameObject.Find("enemyManager");
		enemySpawning spawner = manager.GetComponent<enemySpawning>();
		GameObject player = GameObject.Find("player");
		shooting shootScript = player.GetComponent<shooting>();
		playerHealth healthScript = player.GetComponent<playerHealth>();
		movement moveScript = player.GetComponent<movement>();

		//manages rewards based on their text
		if (reward.Contains("Shooting"))
		{
			shootScript.shootingSkill += rarityIndex;
			//checks if maximum shooting skill has been reached
			if (shootScript.shootingSkill > 45) shootScript.shootingSkill = 45;
		}
		else if (reward.Contains("Max"))
		{
			healthScript.maxHealth += rarityIndex * 10;
		}
		else if (reward.Contains("less"))
		{
			spawner.maxEnemyHealth -= rarityIndex;
			//checks if minimum enemy health has been reached
			if (spawner.maxEnemyHealth < 10) spawner.maxEnemyHealth = 10;
		}
		else if (reward.Contains("full"))
		{
			healthScript.curHealth = healthScript.maxHealth;
		}
		else if (reward.Contains("attack"))
		{
			spawner.enemyDamage -= rarityIndex;
			//checks if minimum enemy damage has been reached
			if (spawner.enemyDamage < 1) spawner.enemyDamage = 1;
		}
		else if (reward.Contains("Movement"))
		{
			moveScript.movementSpeed += 0.025f * rarityIndex;
		}
		else
		{
			spawner.enemySpeed -= 0.025f * rarityIndex;
			//checks if minimum enemy speed has been reached
			if (spawner.enemySpeed < 0.1f) spawner.enemySpeed = 0.1f;
		}

		//unpauses game and hides reward panel
		Time.timeScale = 1;
		winChoiceUI = GameObject.Find("WinChoice");
		winChoiceUI.SetActive(false);
	}
}
