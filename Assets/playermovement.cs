using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float movementSpeed;
    float xMov, yMov;
    bool isColliding = false;
    Vector3 movVector;
    public 
        bool commmitingCrime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColliding)
        { 
        Movement();
        }
    }

    public void Movement()
    {
        xMov = Input.GetAxis("Horizontal") * movementSpeed* Time.deltaTime;
        yMov = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        movVector = new Vector3(xMov, yMov, 0);
        transform.position += movVector;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            commmitingCrime = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
