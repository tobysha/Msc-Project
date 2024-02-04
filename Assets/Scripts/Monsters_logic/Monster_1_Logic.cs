using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_1_Logic : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator Animation;
    
    private float Attack_value = 30f;
    private float Repeatspeed = 1f;
    private Collider tower;
    void Start()
    {
        Initial_value();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Initial_value()
    {
        Animation = GetComponent<Animator>();
        Animation.SetBool("isDead", false);
        Animation.SetBool("Enemy_Find", false);
        Animation.SetInteger("Action_num", 0);
    }
    private void OnColliderEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Tower")
        {
            tower = collision;
            this.gameObject.transform.LookAt(tower.transform);
            InvokeRepeating("Attack", 0.1f, Repeatspeed);
        }
    }
    private void Attack()
    {
        Animation.SetBool("Enemy_Find", true);
        int num = Random.Range(1, 2);
        Animation.SetInteger("Action_num", num);
        tower.gameObject.GetComponent<Tower_data>().Hp -= Attack_value;
    }
    void death()
    {
        if(this.gameObject.GetComponent<Mosters_data>().Hp <= 0f)
        {
            Animation.SetBool("isDead", false);
            GameObject.Destroy(this.gameObject, 1f);
            //Invoke("Destroy", 1f);
        }
    }
    private void Destroy()
    {
        GameObject.Destroy(this.gameObject, 1f);
    }
}
