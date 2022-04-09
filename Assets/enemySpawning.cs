using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemySpawning : MonoBehaviour
{
    private AudioSource waveWinSound;
    public int maxEnemyHealth = 40;
    public int enemyDamage = 30;
    public float enemySpeed = 1.2f;
    private GameObject winChoiceUI;
    private GameObject waveStart;
    public int currentWave = 1;
    public GameObject enemy;
    public GameObject player;
    public shooting shoot;
    public movement move;
    public int zombieCountMax;
    private int randomSpawnZone;
    private float randomX, randomY;
    private Vector3 spawnPosition;
    private float startX = -5.82f;
    private float startY = -2.96f;
    private float endX = 3.56f;
    private float endY = 4.83f;
    public Text txt1, txt2, txt3, rarity1, rarity2, rarity3;
    string[] rarity = { "Common", "Rare", "Legendary" };
    Color[] rarityColours = { new Color(0.2196079f, 0.2196079f, 0.2196079f, 1f), new Color(0.1061321f, 0.381601f, 0.4245283f, 1f), new Color(0.587f, 0.5510877f, 0.121f, 1f) };

    void Start()
    {
        waveWinSound = GetComponent<AudioSource>();
        player = GameObject.Find("player");
        shoot = player.GetComponent<shooting>();
        move = player.GetComponent<movement>();
        winChoiceUI = GameObject.Find("WinChoice");
        winChoiceUI.SetActive(false);
        waveStart = GameObject.Find("waveStart");
        waveStart.SetActive(false);
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        player.transform.position = new Vector3(0f, 0f, 0f);
        waveStart.SetActive(true);
        yield return new WaitForSeconds(1f);
        waveStart.SetActive(false);
        zombieCountMax = 5 + 2 * currentWave;
        for (int i = 0; i < zombieCountMax; i++)
        {
            randomSpawnZone = Random.Range(0, 4);

            switch (randomSpawnZone)
            {
                case 0:
                    randomX = Random.Range(startX, endX);
                    randomY = startY;
                    break;
                case 1:
                    randomX = startX;
                    randomY = Random.Range(startY, endY);
                    break;
                case 2:
                    randomX = Random.Range(startX, endX);
                    randomY = endY;
                    break;
                case 3:
                    randomX = endX;
                    randomY = Random.Range(startY, endY);
                    break;
            }
            spawnPosition = new Vector3(randomX, randomY, 0f);
            GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            newEnemy.GetComponent<health>().curHealth = maxEnemyHealth;
            newEnemy.GetComponent<zombieFollow>().damage = enemyDamage;
            newEnemy.GetComponent<zombieFollow>().speed = enemySpeed;
            yield return new WaitForSeconds(1);
        }
        //Debug.Log(maxEnemyHealth + " "+ enemyDamage + " " + enemySpeed);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        yield return new WaitForSeconds(0.4f);
        if (currentWave == 50) SceneManager.LoadScene(2);
        else
        {
            waveWinSound.Play();
            winChoiceUI.SetActive(true);
            generate();
            Time.timeScale = 0;
            currentWave++;
            StartCoroutine(SpawnEnemy());
        }
    }

    void generate()
    {
        GameObject choice1 = GameObject.Find("Reward1");
        txt1 = choice1.GetComponent<Text>();
        GameObject choice2 = GameObject.Find("Reward2");
        txt2 = choice2.GetComponent<Text>();
        GameObject choice3 = GameObject.Find("Reward3");
        txt3 = choice3.GetComponent<Text>();
        Text[] texts = new Text[3] { txt1, txt2, txt3 };
        GameObject rchoice1 = GameObject.Find("Rarity1");
        rarity1 = rchoice1.GetComponent<Text>();
        GameObject rchoice2 = GameObject.Find("Rarity2");
        rarity2 = rchoice2.GetComponent<Text>();
        GameObject rchoice3 = GameObject.Find("Rarity3");
        rarity3 = rchoice3.GetComponent<Text>();
        Text[] rarities = new Text[3] { rarity1, rarity2, rarity3 };
        for (int i = 0; i < 3; i++)
        {
            int rewardN = Random.Range(1, 7);
            while (((rewardN==1)&&(shoot.shootingSkill==45))||((rewardN == 3) && (maxEnemyHealth == 10)))
            {
                rewardN = Random.Range(1, 7);
            }
            int index = Random.Range(1, 100);
            if (index < 70) index = 0;
            else if (index < 95) index = 1;
            else index = 2;
            switch (rewardN)
            {
                case 1:
                    texts[i].text = "Shooting skill increased +" + (index + 1) + " (Current: "+ shoot.shootingSkill +"/45)";
                    rarities[i].text = rarity[index];
                    rarities[i].color = rarityColours[index];
                    break;
                case 2:
                    texts[i].text = "Max health +" + (index + 1) * 10;
                    rarities[i].text = rarity[index];
                    rarities[i].color = rarityColours[index];
                    break;
                case 3:
                    texts[i].text = "Zombies have less health -" + (index + 1) + " (Current: " + maxEnemyHealth+")";
                    rarities[i].text = rarity[index];
                    rarities[i].color = rarityColours[index];
                    break;
                case 4:
                    texts[i].text = "Health restored to full";
                    rarities[i].text = "Common";
                    rarities[i].color = rarityColours[0];
                    break;
                case 5:
                    texts[i].text = "Zombie attack damage decreased -" + (index + 1) + " (Current: " + enemyDamage +")";
                    rarities[i].text = rarity[index];
                    rarities[i].color = rarityColours[index];
                    break;
                case 6:
                    texts[i].text = "Movement speed increased +" + (index + 1)*0.05 + " (Current: " + move.movementSpeed +")";
                    rarities[i].text = rarity[index];
                    rarities[i].color = rarityColours[index];
                    break;
                case 7:
                    texts[i].text = "Zombie speed decreased -" + (index + 1)*0.05 + " (Current: " +enemySpeed+")";
                    rarities[i].text = rarity[index];
                    rarities[i].color = rarityColours[index];
                    break;
            }
        }
    }
}
