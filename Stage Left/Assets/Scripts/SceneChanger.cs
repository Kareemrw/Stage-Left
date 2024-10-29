using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject player; // Reference to the player object
    private Vector3 nextScenePosition = new Vector3(-3.57f, -1.37f, 0); // Adjust this position as needed

    private void Start()
    {
        if (player != null)
        {
            DontDestroyOnLoad(player); // Ensure player persists across scenes
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (nextScene != null)
            {
                SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to sceneLoaded each time
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player"); // Find the player in the new scene
        if (player != null)
        {
            player.transform.position = nextScenePosition; // Set the position for the next scene
        }
        
        // Unsubscribe to avoid duplicate calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
