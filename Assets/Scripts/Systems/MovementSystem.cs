using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float speed;
    VelocityComp velocity;
    void Start()
    {
        velocity = new VelocityComp(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = transform.position;
        this.gameObject.transform.position = new Vector3(vector3.x + speed * velocity.GetVelocity_X() * Time.deltaTime, vector3.y + speed * velocity.GetVelocity_Y() * Time.deltaTime, 0f);
    }
}
