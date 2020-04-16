﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rippleController;
    public GameObject player;
    public Transform playerRB;
    Vector3 target;
    public GameObject self;
    public float speed;
    public bool moving = false;
    public int hitPoints = 40;
    public GameObject miniPrefab;
    public Transform spawnPointA;
    public Transform spawnPointB;
    public GameObject[] currentEnemies;
    public GameObject enemyPrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRB = player.transform;
        target = (transform.position - playerRB.position).normalized;
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().nuked)
        {
            TakeDamage(5);
        }
        if (moving)
        {
            transform.position -= target * speed * Time.deltaTime;
        }
        if (hitPoints <= 0)
        {
            DestroySelf();
        }
    }

    IEnumerator FindPlayer()
    {
        while (true)
        {
            target = (transform.position - playerRB.position).normalized;
            yield return null;
        }
    }
    void Move()
    {
        moving = true;
    }

    void Stop()
    {
        rippleController.GetComponent<BossRippleController>().animator.SetTrigger("Ripple");
        moving = false;
    }
    public void TakeDamage( int damage)
    {
        hitPoints -= damage;
    }
    public void DestroySelf()
    {

        player.GetComponent<Player>().EnemyKilled();
        Instantiate(miniPrefab, spawnPointA.position, spawnPointA.rotation);
        Instantiate(miniPrefab, spawnPointB.position, spawnPointB.rotation);
        currentEnemies = new GameObject[4];
        currentEnemies[0] = (Instantiate(enemyPrefab, spawnPoint1.position, spawnPoint1.rotation));
        currentEnemies[0].GetComponent<enemyA>().initialShoot = 0.2f;
        currentEnemies[0].GetComponent<enemyA>().shootInterval = 0.5f;
        currentEnemies[0].GetComponent<enemyA>().randomizerInt = 1.5f;
        currentEnemies[0].GetComponent<enemyA>().bounceInt = 0.5f;
        currentEnemies[1] = (Instantiate(enemyPrefab, spawnPoint2.position, spawnPoint2.rotation));
        currentEnemies[1].GetComponent<enemyA>().initialShoot = 0.2f;
        currentEnemies[1].GetComponent<enemyA>().shootInterval = 0.5f;
        currentEnemies[1].GetComponent<enemyA>().randomizerInt = 0.5f;
        currentEnemies[1].GetComponent<enemyA>().bounceInt = 2f;
        currentEnemies[2] = (Instantiate(enemyPrefab, spawnPoint3.position, spawnPoint3.rotation));
        currentEnemies[2].GetComponent<enemyA>().initialShoot = 0.1f;
        currentEnemies[2].GetComponent<enemyA>().shootInterval = 0.5f;
        currentEnemies[2].GetComponent<enemyA>().randomizerInt = 1.5f;
        currentEnemies[2].GetComponent<enemyA>().bounceInt = 0.5f;
        currentEnemies[3] = (Instantiate(enemyPrefab, spawnPoint4.position, spawnPoint4.rotation));
        currentEnemies[3].GetComponent<enemyA>().initialShoot = 0.1f;
        currentEnemies[3].GetComponent<enemyA>().shootInterval = 0.5f;
        currentEnemies[3].GetComponent<enemyA>().randomizerInt = 0.5f;
        currentEnemies[3].GetComponent<enemyA>().bounceInt = 2f;
        Destroy(self);
    }
}
