using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour, IActorView
{
    public GameObject body;
    private Color colorOriginal;
    private Renderer rend;
    public Animator anim;
    void Start()
    {
       
        rend = GetComponent<Renderer>();
        colorOriginal = rend.material.color;
    }

    public void Walk(bool IsWalking)
    {
        if (IsWalking)
        {
            anim.SetBool("IsWalking", true);
        }
        if (!IsWalking)
        {
            anim.SetBool("IsWalking", false);
        }
    }
    
    public void Attack(bool IsAttacking)
    {
        if (IsAttacking)
        {
            anim.SetBool("IsAttack", true);
        }
        if (!IsAttacking)
        {
            anim.SetBool("IsAttack", false);
        }
    }


    public void LookDir(Vector2 dir)
    {
        if (dir.x > 0)
        {
            body.transform.localScale = new Vector2(1,1);
        }
        else if(dir.x < 0)
        {
            body.transform.localScale = new Vector2(-1, 1);
        }
    }

    public void GetDamaged()
    {
        rend.material.color = Color.red;
        StartCoroutine(RestoreColorAfterDelay(0.2f));
    }

    IEnumerator RestoreColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rend.material.color = colorOriginal;
    }

}
