using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BreadCollector : MonoBehaviour
{
    int Bread = 0;

    [SerializeField] Text BreadText;
    [SerializeField] AudioSource collection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bread"))
        {
            Destroy(other.gameObject);
            Bread++;
            PlayerPrefs.SetInt(" : ", Bread);
            BreadText.text = " : " + Bread;
            collection.Play();
            
        }
    }
}
