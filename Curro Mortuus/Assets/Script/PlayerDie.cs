using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit detected");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
