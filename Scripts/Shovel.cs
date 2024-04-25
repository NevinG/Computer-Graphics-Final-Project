using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    Vector2 restPos;
    BoxCollider2D bc;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        restPos = transform.position;
    }

    public void OnMouseDrag()
    {
        GameHandler.instance.placing = true;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bc.enabled = false;
    }

    public void OnMouseUp()
    {
        bc.enabled = true;
        transform.position = restPos;
        if(GameHandler.instance.placeSpot != null)
        {
            GameHandler.instance.RemovePlant();
        }
        GameHandler.instance.placing = false;
    }
}
