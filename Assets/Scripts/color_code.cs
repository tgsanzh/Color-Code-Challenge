using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_code : MonoBehaviour
{
    [HideInInspector] public GameObject currentColor;
    [HideInInspector] public bool needMove;
    [HideInInspector] public Vector3 targetPos;
    private int speed = 20;
    [HideInInspector] public static bool isMoving = false;
    public int which;

    private void Start()
    {
        Vibration.Init();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentColor = collision.gameObject;
        Vibration.VibrateAndroid(10);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentColor = null;
    }
    private void Update()
    {
        if (needMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if(transform.position == targetPos) 
            {
                isMoving = false;
                needMove = false;
            }
        }
    }
}
