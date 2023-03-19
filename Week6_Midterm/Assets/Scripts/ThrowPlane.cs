using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPlane : MonoBehaviour
{
    public static ThrowPlane Instance;
    
    private float startTime, endTime, swipeDistance, swipeTime;

    private Vector2 startMousePos, endMousePos;   //mouse pos
    public float minSwipeDistance = 0;

    private float planeSpd = 0;
    private float maxPlaneSpd = 230;
    private Vector3 angle;

    private Vector3 newPlanePos, resetPlanePos;
    private Quaternion resetPlaneRotation;
    private Vector3 resetCameraPos;
    private Rigidbody rb;

    public bool flying = false;

    void Awake()
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
        rb = GetComponent<Rigidbody>();
        resetPlanePos = transform.position;
        resetPlaneRotation = transform.rotation;
        resetCameraPos = Camera.main.transform.position;
        ResetPlane();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -12f, 7f),
            transform.position.z);
        if (transform.position.y <= -10f)
        {
            Invoke("ResetPlane", 1.5f);
        }
    }

    void ResetPlane()
    {
        angle = Vector3.zero;
        endMousePos = Vector2.zero;
        startMousePos = Vector2.zero;
        planeSpd = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;

        gameObject.SetActive(true);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = resetPlanePos;
        transform.rotation = resetPlaneRotation;
        Camera.main.transform.position = resetCameraPos;

        flying = false;
        
        // GameManager.Instance.beginText.gameObject.SetActive(true);
        // GameManager.Instance.beginText.text = "Let's try again";
    }

    private void OnMouseDown()
    {
        startTime = Time.time;
        startMousePos = Input.mousePosition;
        
        if (GameManager.Instance.gameObject.activeSelf)
        {
            Debug.Log("remove text");
            GameManager.Instance.beginText.gameObject.SetActive(false);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        newPlanePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = newPlanePos;
    }

    private void OnMouseUp()
    {
        endTime = Time.time;
        endMousePos = Input.mousePosition;
        swipeDistance = (endMousePos - startMousePos).magnitude;
        swipeTime = endTime - startTime;

        if (swipeTime is >= 0.5f and < 5f && swipeDistance > 30f)
        {
            flying = true;
            CalculateAngle();
            CalculateSpeed();
            rb.isKinematic = false;
            rb.AddForce(new Vector3(angle.x * planeSpd, angle.y * planeSpd * 0.7f, angle.z * planeSpd));
            // Invoke("ResetPlane", 4f);
        }
        else
        {
            ResetPlane();
        }
    }

    void CalculateAngle()
    {
        angle = new Vector3(newPlanePos.x, newPlanePos.y * (-1), newPlanePos.z * (-1));
    }
    
    void CalculateSpeed()
    {
        if (swipeTime > 0)
        {
            planeSpd = swipeDistance * swipeTime * 30;
        }

        if (planeSpd > maxPlaneSpd)
        {
            planeSpd = maxPlaneSpd;
        }
        swipeTime = 0;
    }
}
