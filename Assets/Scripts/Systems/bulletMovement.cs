using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class bulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Transform target;
    [SerializeField] private float speed  =  2;
    void Start()
    {
        
    }

    public void setTarget(Transform go)
    {
        target = go;
    }
    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector2 direction = target.position - transform.position;

            // ���㳯��Ŀ��ĽǶ�
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ���ö������ת�Ƕ�
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        //this.gameObject.transform.position += gameObject.transform.right * Time.deltaTime * speed;
        //Vector3 direct = transform.rotation * Vector3.up;
        //GetComponent<Rigidbody2D>().velocity = direct * speed;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 moveDirection = transform.right;

        rb.velocity = moveDirection * speed;
    }
}
