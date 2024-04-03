using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler
{
    public Plant plant;

    GameObject placedPlant;

    SpriteRenderer[] sprites;

    float timer = 0;
    bool cooldown = false;

    Image cooldownImage;

    bool isDragging = false;

    private void Start()
    {
        cooldownImage = transform.Find("Cooldown Image").GetComponentInChildren<Image>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameHandler.instance.started && plant.cost <= GameHandler.instance.sunAmount && !cooldown)
        {
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            placedPlant = Instantiate(plant.plantGameObject, spawnPos, Quaternion.identity);
            YOffset yOffset = placedPlant.GetComponent<YOffset>();
            if(yOffset){
                placedPlant.transform.position = new Vector3(placedPlant.transform.position.x, yOffset.yOffset, placedPlant.transform.position.z);
            }
            GameHandler.instance.placing = true;
            isDragging = true;

            //remove components from placed plant
            Component[] components = placedPlant.GetComponentsInChildren<Component>();
            foreach (Component com in components)
            {
                if (!(com is MeshFilter || com is Transform || com is SortingGroup || com is Animator || com is MeshRenderer || com is YOffset))
                {
                    Destroy(com);
                }
            }
        }
        
    }

    private void Update()
    {
        //move the placed plant to where your pointer is
        if (isDragging && GameHandler.instance.started && plant.cost <= GameHandler.instance.sunAmount && !cooldown)
        {
            if (GameHandler.instance.placeSpot == null)
            {
                placedPlant.SetActive(true);
                
                //Get spot to place plant STUFF
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                //Debug.DrawRay(ray.origin, ray.direction * 20, Color.white);
                if (Physics.Raycast(ray, out hit)){
                    if (hit.collider != null) {
                        Vector3 newPos = hit.point;
                        YOffset yOffset = placedPlant.GetComponent<YOffset>();
                        if(yOffset != null){
                            newPos.y += yOffset.yOffset;
                        }
                        placedPlant.transform.position = newPos;
                        
                    }
                }
            }
            else if (GameHandler.instance.OpenSpot())
            {
                placedPlant.SetActive(true);
                Vector3 newPos = GameHandler.instance.placeSpot.position;
                YOffset yOffset = placedPlant.GetComponent<YOffset>();
                if(yOffset != null){
                    newPos.y += yOffset.yOffset;
                }
                placedPlant.transform.position = newPos;
            }
            else
            {
                placedPlant.SetActive(false);
            }
        }

        //check if pointer is up
        if(isDragging && !Input.GetMouseButton(0)){
            if (GameHandler.instance.started && plant.cost <= GameHandler.instance.sunAmount && !cooldown)
            {
                if (GameHandler.instance.placeSpot != null && GameHandler.instance.OpenSpot())
                {
                    GameHandler.instance.placePlant(GameHandler.instance.placeSpot.position, plant);
                    StartCooldown();
                }
                Destroy(placedPlant);
                GameHandler.instance.placing = false;
                isDragging = false;
            }
        }


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
