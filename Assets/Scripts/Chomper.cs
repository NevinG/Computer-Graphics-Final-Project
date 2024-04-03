using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : MonoBehaviour
{
    float chewTime = 42;
    float chewTimer = 0;
    bool chewing = false;
    Animator animator;

    private void Start(){
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(!chewing)
        {
            if(animator && animator.HasState(0, Animator.StringToHash("idle"))) {
                animator.Play("idle");
            }
            CheckHit();
        }
        else
        {
            if(animator && animator.HasState(0, Animator.StringToHash("chewing"))) {
                animator.Play("chewing");
            }
            chewTimer += Time.deltaTime;
            if(chewTimer > chewTime)
            {
                chewTimer = 0;
                chewing = false;
            }
        }
    }

    public void CheckHit()
    {
        foreach (GameObject g in GameHandler.instance.zombiePos)
        {
            Vector2 pos = new Vector2(g.transform.position.x, g.transform.position.z);
            if (transform.position.z == pos.y && pos.x - transform.position.x <= 2.5f && pos.x - transform.position.x >= 0)
            {
                Destroy(g);
                chewing = true;
                return;
                
            }
        }
    }
}
