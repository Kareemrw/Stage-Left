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
    private bool canTalk = false; // Indicates if the player can interact
    private Dialogue currentDialogue; // Reference to the current NPC dialogue
    public static bool hasObject = false;
    public static bool givenObject = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (currentDialogue == null || !currentDialogue.IsDialogueActive()){
            ActivateEKey(true);
            hMove = Input.GetAxis("Horizontal");
            if (hMove < 0.5f || hMove > 0.5f)
            {
                transform.Translate(new Vector2(hMove * hSpeed* Time.fixedDeltaTime, 0));
            }

            if (hMove > 0 && !faceRight) Flip();
            
            if (hMove < 0 && faceRight) Flip();

        }

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        faceRight = !faceRight;
    }

    void Update()
    {
        Debug.Log(givenObject);
        // Check if the player can interact and presses the "E" key
        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            ActivateEKey(false);
            if (currentDialogue != null && !currentDialogue.IsDialogueActive())
            {
                Transform npcTransform = currentDialogue.transform;
                Transform eKeyTransform = npcTransform.Find("eKey");
                if (eKeyTransform != null)
                {
                    eKeyTransform.gameObject.SetActive(false);
                }
                if(hasObject == false && givenObject == false) currentDialogue.StartDialogue(); // Start the dialogue
                if(hasObject == true || givenObject == true) currentDialogue.ObjectStartDialogue();
                Debug.Log(givenObject);
            }
        }
    }

  private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
    {
        ActivateEKey(true, collision.transform);
        canTalk = true;
        currentDialogue = collision.GetComponent<Dialogue>();
    }
}



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            ActivateEKey(false, collision.transform);
            canTalk = false;
            currentDialogue = null; // Remove the reference to the NPC dialogue
        }
    }
    private void ActivateEKey(bool isActive, Transform npcTransform = null)
    {
        npcTransform = npcTransform ?? currentDialogue?.transform;
        Transform eKeyTransform = npcTransform?.Find("eKey");
        if (eKeyTransform != null)
        {
            eKeyTransform.gameObject.SetActive(isActive);
        }
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