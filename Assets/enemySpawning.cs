using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemySpawning : MonoBehaviour
{
    //boolean to let the game know if it needs to load from saved file first
    public bool load;
    //wave win sound effect
    private AudioSource waveWinSound;
    public int maxEnemyHealth = 40;
    public int enemyDamage = 30;
    public float enemySpeed = 1.4f;
    private GameObject winChoiceUI;
    private GameObject waveStart;
    public int currentWave = 1;
    public GameObject enemy;
    public GameObject player;

    //player's shooting and movement scripts
    public shooting shoot;
    public movement move;
    public int zombieCountMax;
    private int randomSpawnZone;
    private float randomX, randomY;
    private Vector3 spawnPosition;

    //game map's limits
    private float startX = -5.82f;
    private float startY = -2.96f;
    private float endX = 3.56f;
    private float endY = 4.83f;

    public Text txt1, txt2, txt3, rarity1, rarity2, rarity3;
    string[] rarity = { "Common", "Rare", "Legendary" };
    Color[] rarityColours = { new Color(0.2196079f, 0.2196079f, 0.2196079f, 1f), new Color(0.1061321f, 0.381601f, 0.4245283f, 1f), new Color(0.587f, 0.5510877f, 0.121f, 1f) };

    void Start()
    {
        //gets the load boolean from another scene
        load = StartGame.load; 
        waveWinSound = GetComponent<AudioSource>();
        player = GameObject.Find("player");
        shoot = player.GetComponent<shooting>();
        move = player.GetComponent<movement>();
        winChoiceUI = GameObject.Find("WinChoice");
        winChoiceUI.SetActive(false);
        waveStart = GameObject.Find("waveStart");
        waveStart.SetActive(false);
        if (load)
        {
            //loads a save if the continue button was chosen on main screen
            loadSave();
        }
        //starts enemy spawning coroutine
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        //saves at the start of every wave
        autosave();
        //resets player to the centre
        player.transform.position = new Vector3(0f, 0f, 0f);
        //sets Wave Start label to be visible for a second
        waveStart.SetActive(true);
        yield return new WaitForSeconds(1f);
        waveStart.SetActive(false);
        //adds 2 zombies for each wave
        zombieCountMax = 5 + 2 * currentWave;
        for (int i = 0; i < zombieCountMax; i++)
        {
            //generates random spawn zone (up, left, bottom, or right) and specific coordinates for each zombie
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
            //spawns zombie in given location
            GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            newEnemy.GetComponent<health>().curHealth = maxEnemyHealth;
            newEnemy.GetComponent<zombieFollow>().damage = enemyDamage;
            newEnemy.GetComponent<zombieFollow>().speed = enemySpeed;

            //waits for a time before spawning another zombie, the delay gets slightly shorter each wave
            yield return new WaitForSeconds(1f-0.012f*currentWave);
        }
        //Debug.Log(maxEnemyHealth + " "+ enemyDamage + " " + enemySpeed);

        //program waits until all zombies are dead
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        yield return new WaitForSeconds(0.4f);
        if (currentWave == 50)
        {
            //if player has won the game, unset all saved playerPrefs and load win screen
            PlayerPrefs.SetInt("currentWave", 1);
            PlayerPrefs.SetInt("maxEnemyHealth", 40);
            PlayerPrefs.SetInt("enemyDamage", 30);
            PlayerPrefs.SetFloat("enemySpeed", 1.2f);
            PlayerPrefs.SetFloat("movementSpeed", 1f);
            PlayerPrefs.SetInt("curHealth", 300);
            PlayerPrefs.SetInt("maxHealth", 300);
            PlayerPrefs.SetInt("shootingSkill", 0);
            SceneManager.LoadScene(2);
        }
        else
        {
            //if player hasn't won the game, play wave win sound and allow to choose rewards
            waveWinSound.Play();
            winChoiceUI.SetActive(true);
            generate();
            //pauses game to allow player to choose
            Time.timeScale = 0;
            currentWave++;
            StartCoroutine(SpawnEnemy());
        }
    }

    void generate()
    {
        //gets all the components of the reward panel
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
            //generates random reward number
            int rewardN = Random.Range(1, 8);
            while (((rewardN==1)&&(shoot.shootingSkill==45))||((rewardN == 3) && (maxEnemyHealth == 10)))
            {
                //if random reward isn't possible, regenerate the number
                rewardN = Random.Range(1, 8);
            }

            //generates rarity index (0-70: common, 70-95: rare, 95-100: legendary)
            int index = Random.Range(1, 100);
            if (index < 70) index = 0;
            else if (index < 95) index = 1;
            else index = 2;

            //changes reward panel text to match the generated random rewards and rarities
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
                    texts[i].text = "Movement speed increased +" + (index + 1)*0.025 + " (Current: " + move.movementSpeed +")";
                    rarities[i].text = rarity[index];
                    rarities[i].color = rarityColours[index];
                    break;
                case 7:
                    texts[i].text = "Zombie speed decreased -" + (index + 1)*0.025 + " (Current: " +enemySpeed+")";
                    rarities[i].text = rarity[index];
                    rarities[i].color = rarityColours[index];
                    break;
            }
        }
    }

    void autosave()
    {
        //saves all current attributes
        PlayerPrefs.SetInt("currentWave", currentWave);
        PlayerPrefs.SetInt("maxEnemyHealth", maxEnemyHealth);
        PlayerPrefs.SetInt("enemyDamage", enemyDamage);
        PlayerPrefs.SetFloat("enemySpeed", enemySpeed);
        PlayerPrefs.SetFloat("movementSpeed", move.movementSpeed);
        PlayerPrefs.SetInt("curHealth", player.GetComponent<playerHealth>().curHealth);
        PlayerPrefs.SetInt("maxHealth", player.GetComponent<playerHealth>().maxHealth);
        PlayerPrefs.SetInt("shootingSkill", shoot.shootingSkill);
    }

    void loadSave()
    {
        //loads all saved attributes
        currentWave = PlayerPrefs.GetInt("currentWave", 1);
        //Debug.Log(currentWave);
        maxEnemyHealth = PlayerPrefs.GetInt("maxEnemyHealth", 40);
        //Debug.Log(maxEnemyHealth);
        enemyDamage = PlayerPrefs.GetInt("enemyDamage", 30);
        //Debug.Log(enemyDamage); 
        move.movementSpeed = PlayerPrefs.GetFloat("movementSpeed", 1f);
        //Debug.Log(move.movementSpeed); 
        player.GetComponent<playerHealth>().curHealth = PlayerPrefs.GetInt("curHealth", 300);
        //Debug.Log(player.GetComponent<playerHealth>().curHealth); 
        player.GetComponent<playerHealth>().maxHealth = PlayerPrefs.GetInt("maxHealth", 300);
        //Debug.Log(player.GetComponent<playerHealth>().maxHealth); 
        enemySpeed = PlayerPrefs.GetFloat("enemySpeed",1.4f);
        //Debug.Log(enemySpeed); 
        shoot.shootingSkill = PlayerPrefs.GetInt("shootingSkill", 0);
        //Debug.Log(shoot.shootingSkill);

        load = false;
    }
}
