using UnityEngine;

public class PortalArrow : MonoBehaviour
{
    [SerializeField] private Transform player; // Referencia al jugador

    private void Update()
    {
        if (player != null)
        {
            transform.position = new Vector2(player.position.x, player.position.y + 2);
        }
    }
}
