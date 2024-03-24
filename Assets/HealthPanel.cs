using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private EnemyHealthTracking enemyHealthTracking;

    // Update is called once per frame
    void Update()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(-(enemyHealthTracking.maxHealth - enemyHealthTracking.currentHealth), rectTransform.offsetMax.y);
    }
}
