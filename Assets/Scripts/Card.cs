using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Plant plant;

    GameObject placedPlant;

    SpriteRenderer[] sprites;

    float timer = 0;
    bool cooldown = false;

    Image cooldownImage;

    private void Start()
    {
        cooldownImage = GetComponentInChildren<Image>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(GameHandler.instance.started && plant.cost <= GameHandler.instance.sunAmount && !cooldown)
        {
            Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            placedPlant = Instantiate(plant.plantGameObject, spawnPos, Quaternion.identity);
            GameHandler.instance.placing = true;

            //remove components from placed plant
            Component[] components = placedPlant.GetComponentsInChildren<Component>();
            foreach (Component com in components)
            {
                if (!(com is SpriteRenderer || com is Transform || com is SortingGroup || com is Animator))
                {
                    Destroy(com);
                }
            }
        }
        
    }
    private void OnMouseDrag()
    {
        if (GameHandler.instance.started && plant.cost <= GameHandler.instance.sunAmount && !cooldown)
        {
            if (GameHandler.instance.placeSpot == null)
            {
                placedPlant.SetActive(true);
                placedPlant.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (GameHandler.instance.OpenSpot())
            {
                placedPlant.SetActive(true);
                placedPlant.transform.position = GameHandler.instance.placeSpot.position;
            }
            else
            {
                placedPlant.SetActive(false);
            }
        }
    }

    private void OnMouseUp()
    {
        if (GameHandler.instance.started && plant.cost <= GameHandler.instance.sunAmount && !cooldown)
        {
            if (GameHandler.instance.placeSpot != null && GameHandler.instance.OpenSpot())
            {
                GameHandler.instance.placePlant(GameHandler.instance.placeSpot.position, plant);
                StartCooldown();
            }
            Destroy(placedPlant);
            GameHandler.instance.placing = false;
        }
    }

    private void Update()
    {
        if (GameHandler.instance.started && plant.cost <= GameHandler.instance.sunAmount)
        {
            foreach(SpriteRenderer sr in sprites)
            {
                sr.color = Color.white;
            }
        }
        else
        {
            foreach (SpriteRenderer sr in sprites)
            {
                sr.color = new Color(60/255f, 60/255f, 60/255f);
            }
        }

        if(cooldown)
        {
            timer += Time.deltaTime;
            cooldownImage.transform.localScale = new Vector3(1, (plant.cooldown - timer) / plant.cooldown, 1);
            if(timer >= plant.cooldown)
            {
                cooldownImage.transform.localScale = new Vector3(1, 0, 1);
                cooldown = false;
                timer = 0;
            }
        }
    }

    private void StartCooldown()
    {
        cooldown = true;
        timer = 0;
        cooldownImage.transform.localScale = new Vector3(1, 1, 1);
    }
}
