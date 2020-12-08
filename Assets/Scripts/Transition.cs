using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour
{

    public void PushGoToMainButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void PushGoToInstructionButton()
    {
        SceneManager.LoadScene("Instruction");
    }


    public void PushGoToTitleButton()
    {
        SceneManager.LoadScene("Title");
    }
}