using UnityEngine;
using UnityEngine.SceneManagement;
public class EscToMenu : MonoBehaviour { 
    void Update() { 
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            SceneManager.LoadScene("Menu"); 
        } 
    } 
}