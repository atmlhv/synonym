using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class Transition : MonoBehaviour
{

    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToInstruction()
    {
        SceneManager.LoadScene("Instruction");
    }


    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void GoToResult()
    {
        SceneManager.LoadScene("Result");
    }
}