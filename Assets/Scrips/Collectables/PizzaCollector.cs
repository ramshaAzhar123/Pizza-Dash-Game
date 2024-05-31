using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PizzaCollector : MonoBehaviour
{
    [SerializeField] AudioSource collection;
    int Pizza = 0;

    [SerializeField] Text PizzaText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pizza"))
        {
            Destroy(other.gameObject);
            Pizza++;
            PlayerPrefs.SetInt(" : ", Pizza);
            PizzaText.text = " : " + Pizza;
            collection.Play();
        }
    }
}
