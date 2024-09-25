using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossView : MonoBehaviour, IActorView
{
    public GameObject body;
    private Color colorOriginal;
    private Renderer rend;

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
        throw new System.NotImplementedException();
    }

    public void Walk(bool IsWalking)
    {
        throw new System.NotImplementedException();
    }
}
