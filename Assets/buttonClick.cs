using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonClick : MonoBehaviour
{
	private GameObject winChoiceUI;
	private Text txt1, rarity1;
	public void clickedButton()
	{
		GameObject choice1 = GameObject.Find("Reward1");
		txt1 = choice1.GetComponent<Text>();
		GameObject rchoice1 = GameObject.Find("Rarity1");
		rarity1 = rchoice1.GetComponent<Text>();
		int rarityIndex;
		if(rarity1.text=="Common")
        {
			rarityIndex = 1;
        }
		else if (rarity1.text=="Rare")
        {
			rarityIndex = 2;
        }
        else
        {
			rarityIndex = 3;
        }
		string reward = txt1.text;
		GameObject manager = GameObject.Find("enemyManager");
		enemySpawning spawner = manager.GetComponent<enemySpawning>();
		GameObject player = GameObject.Find("player");
		shooting shootScript = player.GetComponent<shooting>();
		playerHealth healthScript = player.GetComponent<playerHealth>();
		movement moveScript = player.GetComponent<movement>();
		if (reward.Contains("Shooting"))
		{
			shootScript.shootingSkill += rarityIndex;
			if (shootScript.shootingSkill > 45) shootScript.shootingSkill = 45;
		}
		else if (reward.Contains("Max"))
		{
			healthScript.maxHealth += rarityIndex * 10;
		}
		else if (reward.Contains("less"))
		{
			spawner.maxEnemyHealth -= rarityIndex;
			if (spawner.maxEnemyHealth < 10) spawner.maxEnemyHealth = 10;
		}
		else if (reward.Contains("full"))
		{
			healthScript.curHealth = healthScript.maxHealth;
		}
		else if (reward.Contains("attack"))
		{
			spawner.enemyDamage -= rarityIndex;
			if (spawner.enemyDamage < 1) spawner.enemyDamage = 1;
		}
		else if (reward.Contains("Movement"))
		{
			moveScript.movementSpeed += 0.05f * rarityIndex;
		}
		else
		{
			spawner.enemySpeed -= 0.05f * rarityIndex;
			if (spawner.enemySpeed < 0.1f) spawner.enemySpeed = 0.1f;
		}

		Time.timeScale = 1;
		winChoiceUI = GameObject.Find("WinChoice");
		winChoiceUI.SetActive(false);
	}
}
