using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public PlayerController playerController;
    public string[] lines;
    public string[] objectLines;
    public float textSpeed;
    private bool objectTalk = false;
    private int index;
    private bool isDialogueActive;
    private bool isObjectDialogueActive;
    void Start()
    {
        if (textComponent != null)
        {
            textComponent.text = string.Empty;
            textComponent.transform.parent.gameObject.SetActive(false); // Keep the text component hidden initially
        }
    }

    void Update()
    {
        if(PlayerController.givenObject == true) 
        {
            playerController.dayUpdate();
            playerController.RemoveObjectsWithTag("Day1");
        }
        if ((isDialogueActive || isObjectDialogueActive) && Input.GetMouseButtonDown(0))
        {
            if (index >= GetCurrentLines().Length)
            {
                EndDialogue();
                return;
            }

            if (textComponent.text == GetCurrentLines()[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = GetCurrentLines()[index];
            }
        }
    }

    public void StartDialogue()
    {
        if (textComponent == null)
        {
            return;
        }

        if (lines.Length == 0)
        {
            EndDialogue();
            return;
        }

        index = 0;
        isDialogueActive = true;
        isObjectDialogueActive = false;
        textComponent.transform.parent.gameObject.SetActive(true);
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    public void ObjectStartDialogue()
    {
        if (textComponent == null)
        {
            return;
        }

        if (objectLines.Length == 0)
        {
            StartDialogue();
            return;
        }
        
        index = 0;
        isObjectDialogueActive = true;
        isDialogueActive = false;
        textComponent.transform.parent.gameObject.SetActive(true);
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
        PlayerController.hasObject = false;
        objectTalk = true;
         BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
    Debug.Log("Number of BoxColliders found: " + colliders.Length);

    if (colliders.Length > 1)
    {
        // Confirm we're accessing the second collider
        Debug.Log("Destroying second BoxCollider: " + colliders[1].name);
        Destroy(colliders[1]); // Attempt to remove the second BoxCollider
    }
    else
    {
        Debug.LogWarning("Second BoxCollider not found or does not exist.");
    }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in GetCurrentLines()[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < GetCurrentLines().Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        textComponent.transform.parent.gameObject.SetActive(false);
        isDialogueActive = false;
        isObjectDialogueActive = false;
        if(objectTalk == true) PlayerController.givenObject = true;
        index = 0;
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive || isObjectDialogueActive;
    }

    private string[] GetCurrentLines()
    {
        string[] currentLines = isObjectDialogueActive ? objectLines : lines;
        return currentLines != null && currentLines.Length > 0 ? currentLines : new string[0];
    }
}