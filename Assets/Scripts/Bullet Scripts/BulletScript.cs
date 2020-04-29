using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody myBody;

    public void Move(float speed)
    {
        myBody.AddForce(transform.forward.normalized * speed);
        // call the DeactivateGameObject method after 5 seconds
        Invoke("DeactivateGameObject", 5f); // invoke the mathod after the given time

    }
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
    }

} // class
