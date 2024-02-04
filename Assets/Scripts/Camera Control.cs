using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    private Vector2 moveValue;
    //private Vector3 TempPosition;
    private float movespeed = 0.1f;
    private float smoothTime = 0.2f;
    private Vector3 smoothVelocity
        = new Vector3(1f,1f,1f);

    [SerializeField] private float mouseSensitivity = 3.0f;
    private float rotationY;
    private float rotationX;
    [SerializeField] private Vector2 rotationXMinMax = new Vector2(0, 90);
    private Vector3 currentRotation;
    // Start is called before the first frame update
    void Start()
    {
        initial_set();
    }
    private void initial_set()
    {
        //TempPosition = transform.localPosition;
        
    }
    // Update is called once per frame
    void Update()
    {
        Camera_moving();
        CameraRotator(false);
    }
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
        
        //Debug.Log(TempPosition);
    }
    void Camera_moving()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0f, movespeed * 1f, 0f);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position -= new Vector3(0f, movespeed * 1f, 0f);
        }
        //TempPosition = new Vector3(TempPosition.x + moveValue.x* movespeed, TempPosition.y, TempPosition.z + moveValue.y* movespeed);
        //this.transform.position = TempPosition + (Vector3)moveValue * movespeed;
        //transform.localPosition = Vector3.SmoothDamp(transform.position, TempPosition, ref smoothVelocity, smoothTime);

        transform.position += transform.forward * moveValue.y * movespeed + transform.right * moveValue.x* movespeed;
    }

    private void CameraRotator(bool start)
    {
        if (Input.GetMouseButton(1) || start)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationY += mouseX;
            rotationX += mouseY;

            rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
            //Vector3 player_nextRotation = new Vector3(0f, rotationY);

            Vector3 nextRotation = new Vector3(rotationX, rotationY);

            currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
            transform.localEulerAngles = currentRotation;
            //transform.localEulerAngles = player_nextRotation;
            ////transform.position = Vector3.SmoothDamp(transform.position, player.transform.position - transform.forward * distance, ref smoothVelocity, smoothTime);
            //transform.position = TempPosition;
            //offset = transform.position - player.transform.position;
            //offset = offset.normalized * freeDistance * proportion;
        }

    }
}
