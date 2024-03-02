using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int seconds;
    private float delta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if(delta > 1f)
        {
            seconds++;
            delta = 0f;
            UpdateTimer();
        }
    }

    public void UpdateTimer()
    {
        text.text = seconds.ToString();
    }
}
