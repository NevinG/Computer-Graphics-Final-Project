using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunGenerator : MonoBehaviour
{
    public GameObject sunPrefab;
    public float waitTime;
    float timer;
    bool firstSun = true;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= waitTime || (firstSun && timer >= 6))
        {
            firstSun = false;
            timer = 0;
            SpawnSun();
        }
    }

    public void SpawnSun()
    {
        Vector2 spawnPos = transform.position;
        spawnPos.x += Random.Range(-1.0f, 1.0f);
        spawnPos.y += Random.Range(-1.0f, 1.0f);
        Instantiate(sunPrefab, spawnPos, Quaternion.identity);
    }
}
