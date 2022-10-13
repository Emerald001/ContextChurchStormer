using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public float timer = 10;

    void Update() {
        if(Timer(ref timer) < 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public float Timer(ref float timer) {
        return timer -= Time.deltaTime;
    }
}