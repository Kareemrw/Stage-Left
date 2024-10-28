using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool isDialogueActive;

    // Start is called before the first frame update
    void Start()
    {
        if (textComponent != null)
        {
            textComponent.text = string.Empty;
            textComponent.transform.parent.gameObject.SetActive(false); // Keep the text component hidden initially
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        if (textComponent == null)
        {
            Debug.LogError("Text Component is not assigned!");
            return;
        }

        Debug.Log("Starting dialogue...");
        index = 0;
        isDialogueActive = true;
        textComponent.transform.parent.gameObject.SetActive(true);  // Activate the text component to make it visible
        textComponent.text = string.Empty; // Start with an empty text
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StopAllCoroutines(); // Stop any ongoing typing coroutines before starting a new line
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        Debug.Log("Dialogue ended.");
        textComponent.transform.parent.gameObject.SetActive(false); // Hide the text component when dialogue ends
        isDialogueActive = false;
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
