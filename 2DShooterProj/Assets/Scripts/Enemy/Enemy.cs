using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform enemyTransform;
    private Vector2 targetPosition;

    void Awake()
    {
        enemyTransform = GetComponent<Transform>();
    }

    void Update()
    {
        targetPosition = PlayerInfo.instance.GetPlayerPosition();
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        else if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);   
            GameManager.instance.SetGameScore(1);
        }
    }
}
