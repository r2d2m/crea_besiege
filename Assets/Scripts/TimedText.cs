using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimedText : MonoBehaviour
{
    Coroutine coroutine = null;
    Text textComponent = null;

    private void Awake()
    {
        this.textComponent = GetComponent<Text>();
    }

    void Start()
    {
        this.textComponent.enabled = false;
    }

    void Update()
    {
        
    }

    private IEnumerator ShowCoroutine(string message, float time)
    {
        this.textComponent.text = message;
        this.textComponent.enabled = true;

        yield return new WaitForSeconds(time);

        this.textComponent.enabled = false;
        this.coroutine = null;
    }

    public void Show(string message, float time)
    {
        if (this.coroutine != null)
        {
            StopCoroutine(this.coroutine);
        }

        this.coroutine = StartCoroutine(ShowCoroutine(message, time));
    }
}
