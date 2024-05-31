using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SauceCollector1 : MonoBehaviour
{
    [SerializeField] AudioSource collection;
    int Sauce = 0;

    [SerializeField] Text SauceText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sauce"))
        {
            Destroy(other.gameObject);
            Sauce++;
            PlayerPrefs.SetInt(" : ", Sauce);
            SauceText.text = " : " + Sauce;
            collection.Play();
            
        }
    }
}
