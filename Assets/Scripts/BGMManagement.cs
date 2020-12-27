using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManagement : MonoBehaviour
{
    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<BGMManagement>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "Instruction")
        {
            //Debug.Log(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
        }


    }
}
