using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("PlayerProperty", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("All PlayerPrefs keys deleted");
        }
    }
}
