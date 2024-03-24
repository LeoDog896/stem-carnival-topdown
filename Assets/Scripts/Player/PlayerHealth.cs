using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;

    public bool hit = true;

    private SpriteRenderer colorchanger;

    [SerializeField] private float maxHealth = 3f;

    public Text hpText;

    // Start is called before the first frame update
    void Start() { 
        hpText.text = "Health: " + health.ToString();
        colorchanger = GetComponent<SpriteRenderer>();
        Time.timeScale = 1;
        health = maxHealth;
    }


    void Update()
    {
        hpText.text = "Health: " + health.ToString();
    }

    IEnumerator HitBoxOff()
    {
        hit = false;
        Debug.Log(health);
        yield return new WaitForSeconds(1.5f);
        colorchanger.color = new Color(255, 255, 255);
        hit = true;
    }
    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            if (hit)
            {
                health--;
                colorchanger.color = new Color(255, 0, 0);
                StartCoroutine(HitBoxOff());
                if(health <= 0)
                {
                    Time.timeScale = 0;
                }
            }
        }
    }

}
