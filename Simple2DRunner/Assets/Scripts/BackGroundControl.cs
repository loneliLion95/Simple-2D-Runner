using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundControl : MonoBehaviour
{
    float moveSpeed = -4f;
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!GameControl.gameStopped)
        {
            rectTransform.position = new Vector2(rectTransform.position.x + moveSpeed * Time.deltaTime, 0);
            if (rectTransform.localPosition.x < -1915)
                rectTransform.localPosition = new Vector2(0, 0);
        }
    }
}
