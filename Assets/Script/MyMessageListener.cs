using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyMessageListener : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI applesText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject theCube;
    [SerializeField] int apples;
    [SerializeField] int lives = 2;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(lives <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnMessageArrived(string msg)
    {
        //Debug.Log("Arrived: " + msg);
        if (msg == "Button is being pressed")
        {
            theCube.transform.Translate(Vector3.up * Time.deltaTime * 10);
        }
        if (msg.Contains("X"))
        {
            int xMovement = int.Parse(msg.Remove(0, 13));
            xMovement = xMovement - 500;
            if (xMovement > 10 || xMovement < -10)
            theCube.transform.Translate(Vector3.right * Time.deltaTime * speed * Mathf.Clamp(xMovement, -200, 200));

        }
        if (msg.Contains("Y"))
        {
            int yMovement = int.Parse(msg.Remove(0, 13));
            yMovement = yMovement - 500;
            if(yMovement > 10 || yMovement < -10)
            theCube.transform.Translate(Vector3.forward * Time.deltaTime * speed * Mathf.Clamp(yMovement, -200, 200));
        }

    }

    void OnConnectionEvent(bool success)
    {
        //Debug.Log(success ? "Device connected" : "Device disconnected");
    }


    private void OnTriggerEnter(Collider other)
    {       
      
      if (other.gameObject.CompareTag("Apple"))
       {
           Destroy(other.gameObject);
            apples++;
           applesText.text = ("Apples collected: " + apples);
        }

        if (other.gameObject.CompareTag("Spike"))
        {
            Destroy(other.gameObject);
            lives--;
            livesText.text = ("Lives remaining: " + lives);
        }

    }

    
}
