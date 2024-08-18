using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private Image image;
    public Sprite[] sprites;
    public float changeInterval = 1f;
    private int currentIndex = 0;
    private float timer = 0f;
    void Start()
    {
        image = GetComponent<Image>();
        if (sprites.Length > 0)
        {
            image.sprite = sprites[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f; 
            currentIndex = (currentIndex + 1) % sprites.Length; 
            image.sprite = sprites[currentIndex]; 
        }
    }
}
