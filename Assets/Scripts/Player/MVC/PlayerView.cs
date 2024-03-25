using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    public GameObject body;

    public void LookDir(Vector2 dir)
    {
        if (dir.x > 0)
        {
            body.transform.localScale = new Vector2(-1,1);
        }
        else if(dir.x < 0)
        {
            body.transform.localScale = new Vector2(1, 1);
        }
    }

}
