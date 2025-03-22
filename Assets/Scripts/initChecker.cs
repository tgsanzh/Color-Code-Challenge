using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class initChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Check", 15);
    }
    void Check()
    {
        if(!AdsInitializer.isInited)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
