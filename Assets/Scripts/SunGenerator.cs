using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunGenerator : MonoBehaviour
{
    public GameObject sunPrefab;
    public float waitTime;
    float timer;
    bool firstSun = true;

    bool hasGold = false;
    
    GameObject gold;

    private void Start(){
        gold = transform.Find("Gold").gameObject;
    }
    private void Update()
    {
        if(!hasGold)
            timer += Time.deltaTime;

        if(!hasGold && timer >= waitTime || (firstSun && timer >= 6))
        {
            firstSun = false;
            timer = 0;
            hasGold = true;
            SpawnSun();
        }

        //check for click on object
        //Get spot to place plant STUFF
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 20, Color.white);
            if (Physics.Raycast(ray, out hit)){
                if (hit.collider.gameObject == gameObject) {
                    if(hasGold)
                        collectGold();
                }
            }
        }
    }

    private void collectGold(){
        hasGold = false;
        GameHandler.instance.AddSun(50);
        gold.SetActive(false);
    }

    public void SpawnSun()
    {
        gold.SetActive(true);
    }

}
