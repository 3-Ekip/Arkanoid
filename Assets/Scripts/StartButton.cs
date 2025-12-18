using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartTheGame()
    {         
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
