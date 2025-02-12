using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class playerController : MonoBehaviour
{
    public float Speed;
    public TMP_Text ScoreText;
    public TMP_Text WinText;
    public GameObject Gate;
    private Rigidbody rb;
    public int Score;

    void SetScoreText()
    {
        ScoreText.text = "Score: " + Score.ToString();
        if (Score >= 9)
        {
            WinText.text = "You won! Press R to restart or ESC to quit";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Score = 0;
        SetScoreText();
        WinText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * Speed);

        //restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        //Quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pick_up"))
        {
            other.gameObject.SetActive(false);
            Score += 1;
            if (Score >= 5)
            {
                Gate.gameObject.SetActive(false);
            }
            SetScoreText();
        }

        if (other.gameObject.CompareTag("danger"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}