using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageInteraction : MonoBehaviour
{
    private bool canBeActivated = false;
    private bool hasBeenActivated = false;
    private float activationTime = 0f;
    [SerializeField] public float duration;
    [SerializeField] InteractionSpawnPoint spawnPoint;
    [SerializeField] private Direction moveDirection;
    private Vector3 initialPosition;
    public List<Animator> animations;
    public AudioSource audioSource;
    public AudioClip clip;

    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    private void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canBeActivated && Input.GetKeyDown(KeyCode.F))
        {
            foreach (var anim in animations)
            {
                anim.SetBool("Run", true);
            }
            hasBeenActivated = true;
        }

        if (hasBeenActivated)
        {
            MoveObject();
            activationTime += Time.deltaTime;

            if (activationTime >= duration)
            {
                StopInteraction();
                activationTime = 0f;
            }
        }
    }

    private void MoveObject()
    {
        Vector2 direction = Vector2.zero;

        switch (moveDirection)
        {
            case Direction.Right:
                direction = Vector2.right;
                break;
            case Direction.Left:
                direction = Vector2.left;
                break;
            case Direction.Up:
                direction = Vector2.up;
                break;
            case Direction.Down:
                direction = Vector2.down;
                break;
        }
        if(audioSource !=null || clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        transform.Translate(direction * Time.deltaTime * 5f);
    }

    private void StopInteraction()
    {
        foreach (var anim in animations)
        {
            anim.SetBool("Run", false);
        }
        hasBeenActivated = false;
        transform.position = initialPosition;
        spawnPoint.GetRespawnWave();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasBeenActivated)
        {
            IEnemyController enemyController = collision.gameObject.GetComponent<IEnemyController>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(9999);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (var anim in animations)
            {
                anim.SetBool("Interact", true);
            }
            canBeActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (var anim in animations)
            {
                anim.SetBool("Interact", false);
            }
            canBeActivated = false;
        }
    }
}
