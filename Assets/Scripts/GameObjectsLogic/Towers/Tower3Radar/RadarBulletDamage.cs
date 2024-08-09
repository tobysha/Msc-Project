using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBulletDamage : MonoBehaviour
{
    public Sprite damageEffectSprite1;
    public Sprite damageEffectSprite2;
    public float duration = 3f;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isUsingSprite1;
    private ObjectsData obd;
    void Start()
    {
        obd = GetComponentInParent<ObjectsData>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("RangeDamageController requires a SpriteRenderer component on the same game object.");
            return;
        }

        timer = 0f;
        isUsingSprite1 = true;
        InvokeRepeating("SwitchSprite", 0f, 0.3f);
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void SwitchSprite()
    {
        if (isUsingSprite1)
        {
            spriteRenderer.sprite = damageEffectSprite2;
            isUsingSprite1 = false;
        }
        else
        {
            spriteRenderer.sprite = damageEffectSprite1;
            isUsingSprite1 = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& timer>=0.5f)
        {
            collision.GetComponent<LifeSystem>().setHP(-obd.atk);
            timer = 0f;
        }
        

    }
}
