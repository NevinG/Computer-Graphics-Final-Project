using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSun : MonoBehaviour
{
    public int value = 25;
    float fallDistance;
    float fallSpeed = 2;
    Vector3 fallPos;
    float life = 15.0f;
    float timer = 0;
    bool collected = false;
    public bool fall = true;

    Vector2 collectedPos = new Vector2(-8.17f,4.66f);
    private void Start()
    {
        if(fall)
        {
            transform.position = new Vector3(Random.Range(-8.36f, 8.36f), 6.29f,-1);
            fallDistance = Random.Range(30, 80) / 10f;
            fallPos = new Vector3(transform.position.x, transform.position.y - fallDistance,-1);
        }
        else
        {
            fallPos = transform.position;
        }
        fallPos.z = -1;
        Vector3 pos = transform.position;
        pos.z = -1;
        transform.position = pos;
    }

    private void Update()
    {
        if(!collected)
        {
            timer += Time.deltaTime;
            if (timer >= life)
            {
                Destroy(gameObject);
            }
            transform.position = Vector3.MoveTowards(transform.position, fallPos, Time.deltaTime * fallSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, collectedPos, Time.deltaTime * fallSpeed * 10);
            if((Vector2)transform.position == collectedPos)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if(!collected)
        {
            GameHandler.instance.AddSun(value);
            collected = true;
        }
    }
}
