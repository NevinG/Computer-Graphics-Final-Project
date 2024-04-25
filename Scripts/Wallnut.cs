using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallnut : MonoBehaviour
{
    PlantHealth plantH;
    public Sprite damagedSprite;
    float startingHealth;

    private void Start()
    {
        plantH = GetComponentInChildren<PlantHealth>();
        startingHealth = plantH.health;
    }
    void Update()
    {
        if(plantH.health <= startingHealth/2f)
        {
            GetComponent<SpriteRenderer>().sprite = damagedSprite;
        }
    }
}
