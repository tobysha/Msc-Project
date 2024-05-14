using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityComp : MonoBehaviour
{
    private int Velocity_x;
    private int Velocity_y;
    public VelocityComp(int velocityX, int velocityY)
    {
        Velocity_x = velocityX;
        Velocity_y = velocityY;
    }
    public void SetVelocity(int x, int y)
    {
        Velocity_x = x;
        Velocity_y = y;
    }
    public int GetVelocity_X()
    {
        return Velocity_x;
    }
    public int GetVelocity_Y()
    {
        return Velocity_y;
    }
}
