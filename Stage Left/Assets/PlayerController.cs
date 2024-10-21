using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float hSpeed;
    private float hMove;
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        hMove = Input.GetAxis("Horizontal");
        if (hMove < 0.5f || hMove > 0.5f)
        {
            transform.Translate(new Vector2(hMove * hSpeed, 0));
        }
    }
}
