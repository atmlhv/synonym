﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetKey : MonoBehaviour
{
    public Fade fade;
    public string NextStage;
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip transition;

    // Start is called before the first frame update
    void Start()
    {
        fade.FadeOut(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.PlayOneShot(transition);
            fade.FadeIn(0.5f, () =>
            {
                SceneManager.LoadScene(NextStage);
            });

        }
    }
}
