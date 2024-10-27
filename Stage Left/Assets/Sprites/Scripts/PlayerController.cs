using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float hSpeed;
    private float hMove;
    
    private bool faceRight = true;
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

        if (hMove > 0 && !faceRight) Flip();
     

        if (hMove < 0 && faceRight) Flip();
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        faceRight = !faceRight; ;
    }
}
