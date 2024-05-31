using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToppingCollector2 : MonoBehaviour
{
    [SerializeField] AudioSource collection;
    int Topping = 0;

    [SerializeField] Text ToppingText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Topping"))
        {
            Destroy(other.gameObject);
            Topping++;
            PlayerPrefs.SetInt(" : ", Topping);
            ToppingText.text = " : " + Topping;
            collection.Play();
        }
    }
}
