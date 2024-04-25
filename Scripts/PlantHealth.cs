using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    public float health = 6;

    float flashTimer = 0;
    float flashLength = .15f;
    bool flash = false;
    SpriteRenderer[] sprites;

    public void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void RemoveHealth(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        foreach(SpriteRenderer sr in sprites)
        {
            sr.color = Color.red;
        }
        flash = true;
        flashTimer = 0;
    }

    public void Update()
    {
        flashTimer += Time.deltaTime;
        if(flash && flashTimer >= flashLength)
        {
            flashTimer = 0;

            foreach (SpriteRenderer sr in sprites)
            {
                sr.color = Color.white;
            }
        }
    }


}
