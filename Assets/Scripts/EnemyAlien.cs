using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlien : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;

    Rigidbody2D rigidbody2D;
    int direction = 1;
    public GameObject projectilePrefab;

    float shotTime;
    float resetShot = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        shotTime = resetShot;
    }

    void Update()
    {
        shotTime -= Time.deltaTime;

        if(shotTime < 0)
        {
            Launch();
            shotTime = resetShot;
        }
        // 
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        
        rigidbody2D.MovePosition(position);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerShip player = other.gameObject.GetComponent<PlayerShip >();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
        direction = -direction;
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Bullet projectile = projectileObject.GetComponent<Bullet>();
        projectile.Launch(new Vector2(0,-1), 500);
    }
}