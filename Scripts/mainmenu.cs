using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void loadsingleplayer()
    {
        SceneManager.LoadScene(1);
    }
    public void loadcoop()
    {
        SceneManager.LoadScene(2);
    }
    // Start is called before the first frame update
    
}
