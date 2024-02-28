using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ConveyorCard : MonoBehaviour
{
    [HideInInspector] public Plant plant;
    protected float beltspeed = 1f;
    [HideInInspector] public bool canMoveLeft = true;

    protected GameObject placedPlant;

    private void Start()
    {
        transform.Find("Plant Sprite").GetComponent<SpriteRenderer>().sprite = plant.sprite;
    }

    private void OnMouseDown()
    {
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        placedPlant = Instantiate(plant.plantGameObject, spawnPos, Quaternion.identity);
        GameHandler.instance.placing = true;

        //remove components from placed plant
        Component[] components = placedPlant.GetComponentsInChildren<Component>();
        foreach(Component com in components)
        {
            if(!(com is SpriteRenderer || com is Transform || com is SortingGroup || com is Animator))
            {
                Destroy(com);
            }
        }
    }
    private void OnMouseDrag()
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

    private void OnMouseUp()
    {
        if (GameHandler.instance.placeSpot != null && GameHandler.instance.OpenSpot())
        {
            GameHandler.instance.placePlant(placedPlant.transform.position = GameHandler.instance.placeSpot.position, plant);
            Destroy(gameObject);
        }
        Destroy(placedPlant);
        GameHandler.instance.placing = false;
    }

    private void Update()
    {
        if(canMoveLeft)
        {
            Vector2 pos = transform.position;
            pos.x -= beltspeed * Time.deltaTime;
            transform.position = pos;
        }
    }
}
