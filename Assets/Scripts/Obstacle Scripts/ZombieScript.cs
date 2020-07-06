using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public GameObject bloodFXPrefab;
    private float speed = 1f;

    private Rigidbody myBody;

    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();

        speed = Random.Range(1f, 5f);

        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            myBody.velocity = new Vector3(0f, 0f, -speed);
        }
        // when the zombie falls of the ground it means we went pass him and we don't need it anymore
        if (transform.position.y < -10f) // detects when the zombie is lowert then the surfase
        {
            gameObject.SetActive(false);
        }
    }

    void Die()
    {
        isAlive = false;
        myBody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("Idle"); // stop the zombies from walking
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Player" || target.gameObject.tag == "Bullet")
        {
            Instantiate(bloodFXPrefab, transform.position, Quaternion.identity);

            Invoke("DeactivateGameObject", 3f);

            // INCREASE SCORE
            GameplayController.instance.IncreaseScore();

            Die();
        }
    }
}
