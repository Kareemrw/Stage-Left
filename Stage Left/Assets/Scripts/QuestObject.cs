using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public PlayerController playerController;
    private bool canGrab = false; // Flag to determine if the player is in range to grab

    // Update is called once per frame
    void Update()
    {
        // Check if player is in range and presses the "E" key
        if (canGrab)
        {
            PlayerController.givenObject = false;
            //PlayerController.givenObject = false;
            if (Input.GetKeyDown(KeyCode.E))
            {
                grabObject();
                
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canGrab = true; // Set flag to true when the player is in range to grab
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canGrab = false; // Set flag to false when the player leaves the range
        }
    }

    private void grabObject()
    {
        PlayerController.hasObject = true; // Update the static variable directly
        Destroy(gameObject);
    }
}
