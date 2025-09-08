using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; // Already included

public class Winner : MonoBehaviour
{
    public string winSceneName = "WinScene";

    void OnTriggerEnter(Collider other)
    {
        // Make sure the player has a tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("🎉 Player wins!");
            SceneManager.LoadScene(winSceneName);
        }
    }
}
