using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject mainMenu;
    void Awake()
    {
        gameObject.SetActive(true);
        mainMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.anyKey)
        {
            Hide();
            show();
        }
    }
    public void show()
    {
        mainMenu.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
