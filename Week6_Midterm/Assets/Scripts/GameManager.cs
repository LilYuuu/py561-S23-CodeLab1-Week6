using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int counter = 0;
    public int target = 1;   // will be changed based on number of levels

    public TextMeshProUGUI beginText;
    public TextMeshProUGUI endText;

    public Image arrowImg;
    private float arrowImgOpacity;
    
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
        arrowImgOpacity = arrowImg.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        arrowImg.color = new Vector4(arrowImg.color.r, arrowImg.color.g, arrowImg.color.b, 255 * Mathf.Sin(Time.time * 6));
        Debug.Log(arrowImg.color.a);
        if (counter == target)
        {
            // Debug.Log("target achieved");
            ThrowPlane.Instance.gameObject.SetActive(false);
            endText.gameObject.SetActive(true);
        }
    }
}
