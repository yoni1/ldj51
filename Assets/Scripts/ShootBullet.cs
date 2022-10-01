using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    private bool isShooting;
    public GameObject bullet;
    public Transform shootPosition;
    public float shootSpeed, shootTimer;

    
    // Start is called before the first frame update
    void Start()
    {
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            // Wait for shoot to be done async and continue to the next frame
            StartCoroutine(Shoot());
            
        }
    }

    IEnumerator Shoot()
    {
        Debug.Log("Shoot");

        int direction()
        {
            if (transform.localScale.x < 0f)
            {
                return -1;
            } else
            {
                return +1;
            }
        }
        isShooting = true;
        GameObject newbullet = Instantiate(bullet, shootPosition.position, Quaternion.identity);
        newbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime * direction(), 0f);
        newbullet.transform.localScale = new Vector2(newbullet.transform.localScale.x * direction(), newbullet.transform.localScale.y);
        yield return new WaitForSeconds(shootTimer);
        isShooting = false;
        Debug.Log("Stop Shooting!");
    }
}
