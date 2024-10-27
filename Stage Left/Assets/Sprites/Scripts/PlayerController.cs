using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float hSpeed;
    [SerializeField] private int dayCount;
    [SerializeField] private GameObject leftTrigger;
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

    // Function to update current day variable
    public void dayUpdate()
    {
        this.dayCount += 1;

        // Activates left SceneTrigger to allow for game end state
        if (this.dayCount == 2)
        {
            leftTrigger.SetActive(true);
        }
    }
    
    // Function to convert current day into a string
    public string dayCheck()
    {
        return "Day"+this.dayCount;
    }
}
