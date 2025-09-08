using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System.Diagnostics;

public class Restart : MonoBehaviour
{
    public GameObject restartButton; // assign in Inspector
    public string firstSceneName = "SampleScene"; // set this to your first scene name
 
    // Hook this to the button OnClick
    public void RestartGame()
    {
        SceneManager.LoadScene(firstSceneName);
    }
}
