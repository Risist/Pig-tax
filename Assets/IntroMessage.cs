using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

   public void OnPressStart()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
