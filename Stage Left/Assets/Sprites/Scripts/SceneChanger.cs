using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private GameObject[] disableObjects;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {

    }

    // Detects if player has entered scene edge trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Changes Scene
            if (nextScene != null)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    // Function updates variables and gameObjects to match day state
    // Potentially could be moved to NPC script
    private void dayChange()
    {
        player.GetComponent<PlayerController>().dayUpdate();
        string currDay = player.GetComponent<PlayerController>().dayCheck();
        disableObjects = GameObject.FindGameObjectsWithTag(currDay);
        for (int i = 0; i < disableObjects.Length; i++)
        {
            disableObjects[i].SetActive(false);
        }
    }
}
