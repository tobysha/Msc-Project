using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float zoomSpeed = 2f;
    public float minZoom = 1f;
    public float boundMinX;
    public float boundMinY;
    public float boundMaxX;
    public float boundMaxY;
    private Vector3 lastMousePosition;
    private float maxZoom;

    private void Start()
    {
        maxZoom = Camera.main.orthographicSize;
    }
    private void Update()
    {
        // WASD�ƶ�
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //Vector3 moveDirection = new Vector3(horizontal, vertical, 0);
        //transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ����������ƶ���Χ
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, boundMinX, boundMaxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, boundMinY, boundMaxY);
        transform.position = clampedPosition;

        // ����������
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = Camera.main.orthographicSize - scrollWheel * zoomSpeed;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
        Camera.main.orthographicSize = newZoom;

        // ����Ҽ���ק�ƶ�
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            transform.position += new Vector3(-delta.x, -delta.y, 0) * moveSpeed * Time.deltaTime;
            lastMousePosition = Input.mousePosition;
        }
    }
}
