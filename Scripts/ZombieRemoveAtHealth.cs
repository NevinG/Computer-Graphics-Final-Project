using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRemoveAtHealth : MonoBehaviour
{
    public GameObject[] sprites;
    public float[] healthValues;
    ZombieStats zombieStats;

    private void Start()
    {
        zombieStats = GetComponentInChildren<ZombieStats>();
    }

    public void UpdateLimbs()
    {
        for(int i = 0; i < healthValues.Length; i++)
        {
            if (zombieStats.health <= healthValues[i])
            {
                sprites[i].SetActive(false);
            }
        }
    }
}
