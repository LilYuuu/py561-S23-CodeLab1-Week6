using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int counter = 0;
    public int target = 1;   // will be changed based on number of levels

    public TextMeshProUGUI beginText;
    public TextMeshProUGUI endText;
    
    private void Awake()
    {
        // singleton
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if (Time.time > 3f && beginText.gameObject.activeSelf)
        // {
        //     beginText.gameObject.SetActive(false);
        // }
        
        if (counter == target)
        {
            // Debug.Log("target achieved");
            ThrowPlane.Instance.gameObject.SetActive(false);
            endText.gameObject.SetActive(true);
        }
    }
}
