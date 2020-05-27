using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk, 
    attack, 
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttaack;
    public float moveSpeed;

    private void Awake()
    {
        health = maxHealth.inicialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D rigidbody, float knockTim, float damage)
    {
        StartCoroutine(knockCo(rigidbody, knockTim));
        TakeDamage(damage);
    }

    private IEnumerator knockCo(Rigidbody2D rigidbody2D, float knockTime)
    {
        if (rigidbody2D != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidbody2D.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}
