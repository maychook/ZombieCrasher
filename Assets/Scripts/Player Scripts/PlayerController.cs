using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Rigidbody myBody;

    public Transform bullet_StartPoint;
    public GameObject bullet_Prefab;
    public ParticleSystem shootFX;

    private Animator shootSliderAnim;

    [HideInInspector]
    public bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        /*  NOTE! 
        * when a class extends a base class (baseController) I can only have one 
        * function with any given name so I cannot have an "Awake" function in both.
        */

        myBody = GetComponent<Rigidbody>();

        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();

        // get the button and attach the shooting function
        GameObject.Find("ShootBtn").GetComponent<Button>().onClick.AddListener(ShootingControl);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovementWithKeyboard();
        ChangeRotation();
    }

    private void FixedUpdate()
    {
        MoveTank();
        
    }

    void MoveTank()
    {
        print(speed.ToString());
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);

    }

    void ControlMovementWithKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft(); // funciton from the base controller script
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight(); // funciton from the base controller script
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast(); // funciton from the base controller script
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow(); // funciton from the base controller script
        }

        // return normal when key up

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight(); // funciton from the base controller script
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            MoveStraight(); // funciton from the base controller script
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            MoveNormal(); // funciton from the base controller script
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            MoveNormal(); // funciton from the base controller script
        }
    }

    void ChangeRotation()
    {
        if (speed.x > 0)
        {
            // go from current rotatoin to the maxangle rotation in the given time
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if(speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    public void ShootingControl()
    {
        if (Time.timeScale != 0)
        {
            if (canShoot)
            {
                GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position,
                    Quaternion.identity);
                bullet.GetComponent<BulletScript>().Move(2000f);
                shootFX.Play();

                canShoot = false;
                // call the anim
                shootSliderAnim.Play("Fill");
            }
        }
            
        
    }
}
