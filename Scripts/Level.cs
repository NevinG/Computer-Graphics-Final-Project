using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    GameObject[] rows = new GameObject[5];
    public bool[] enabledRows = new bool[5];
    #region
    public static Level instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public bool day;

    public int startingSun;

    public ZombieWave[] waves;

    public float startLevelWaitTime;

    bool started = false;
    float waveTimer = 0;
    int curWave = 0;

    public GameObject otherReward;

    private GameObject scene;

    GravePlacer gravePlacer;
    private void Start()
    {
        //get gravePlacer
        scene = GameObject.Find("Scene");

        //gravePlacer = GameObject.Find("PlacedPlants").GetComponent<GravePlacer>();
        GameHandler.instance.AddSun(startingSun);
        rows[0] = GameObject.Find("Row 1");
        rows[1] = GameObject.Find("Row 2");
        rows[2] = GameObject.Find("Row 3");
        rows[3] = GameObject.Find("Row 4");
        rows[4] = GameObject.Find("Row 5");

        for(int i = 0;i < enabledRows.Length; i++)
        {
            rows[i].SetActive(enabledRows[i]);
        }
        
    }
    private void Update()
    {
        if (started)
        {
            waveTimer += Time.deltaTime;
            if ((GameHandler.instance.zombiePos.Count == 0 || waveTimer > waves[0].waitTimeBeforeNextWavel) && curWave < waves.Length - 1)
            {
                waveTimer = 0;
                curWave++;
                RectTransform rt = GameHandler.instance.progressImage.GetComponent<RectTransform>();
                Vector2 scale = rt.sizeDelta;
                scale.x = (float)curWave/(waves.Length - 1) * 103.4457f;
                rt.sizeDelta = scale;
                SpawnWave(waves[curWave]);
            }
        }
        else if(GameHandler.instance.started)
        {
            waveTimer += Time.deltaTime;
            if(waveTimer >= startLevelWaitTime)
            {
                waveTimer = 0;
                started = true;
                SpawnWave(waves[curWave]);
            }
        }
        //check win
        if(curWave == waves.Length -1 && !GameHandler.instance.dead && GameHandler.instance.zombiePos.Count == 0 && !GameHandler.instance.won)
        {
            GameHandler.instance.WinLevel();
            int levelNum = PlayerPrefs.GetInt("level", 1);
            PlayerPrefs.SetInt("level", levelNum + 1);
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                PlayerPrefs.SetInt("level", 1);
            }
        }
    }

    private void SpawnWave(ZombieWave wave)
    {
        foreach(Zombie zom in wave.zombies)
        {
            GameObject spawnRow = rows[Random.Range(0, 5)];
            while(!spawnRow.activeSelf)
            {
                spawnRow = rows[Random.Range(0, 5)];
            }
            Vector3 spawnPos = new Vector3(10, scene.transform.position.y, spawnRow.transform.position.z);

            GameObject z = Instantiate(zom.zombieGameObject, spawnPos, Quaternion.identity, scene.transform);
            GameHandler.instance.zombiePos.Add(z);
            z.GetComponentInChildren<ZombieStats>().zombie = zom;
        }

        //graves
        // if(curWave == waves.Length - 1 && !day && gravePlacer != null)
        // {
        //     gravePlacer.SpawnZombies();
        // }
    }
}
