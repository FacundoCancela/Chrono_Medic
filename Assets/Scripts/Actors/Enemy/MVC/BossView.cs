using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossView : MonoBehaviour, IActorView
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

    public void LookDir(Vector2 dir)
    {

        if (dir.x > 0)
        {
            body.transform.localScale = new Vector2(1, 1);
        }
        else if (dir.x < 0)
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

    public void Attack(bool isAttacking)
    {
        if (isAttacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    public void Walk(bool IsWalking)
    {
        anim.SetBool("isWalking", IsWalking);
    }
}
