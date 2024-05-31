using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CheeseCollector : MonoBehaviour

{
    [SerializeField] AudioSource collection;
    int Cheese = 0;

    [SerializeField] Text CheeseText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cheese"))
        {
            Destroy(other.gameObject);
            Cheese++;
            PlayerPrefs.SetInt(" : ", Cheese);
            CheeseText.text = " : " + Cheese;
            collection.Play();
            
        }
    }
}
