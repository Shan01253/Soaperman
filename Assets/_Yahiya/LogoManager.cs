using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogoManager : MonoBehaviour
{
    public SpriteRenderer Logo;
    public SpriteRenderer Black;
    public TextMeshProUGUI textgui;
    public GameObject MainMenuStuff;
    bool startingup = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startup());
    }

    // Update is called once per frame
    void Update()
    {
        if (startingup && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            startingup = false;
            StopAllCoroutines();
            Logo.color = Color.clear;
            Black.color = Color.clear;
            textgui.color = Color.clear;
            MainMenuStuff.SetActive(true);
            AudioManager.instance.Stop("startup");
            AudioManager.instance.Play("bubbles");
        }
    }

    IEnumerator startup()
    {
        startingup = true;
        MainMenuStuff.SetActive(false);
        Color c;
        Color b;
        Color t;
        AudioManager.Instance.Play("startup");
        //Logo.color = Color.clear;
        //textgui.color = Color.clear;
        for (int i = 0; i <= 20; i++)
        {
            c = Logo.color;
            t = textgui.color;
            c.a = i / 20f;
            t.a = c.a;
            Logo.color = c;
            textgui.color = t;
            yield return null;
        }
        yield return new WaitForSeconds(7);
        AudioManager.Instance.Play("bubbles");

        for (int i = 0; i <= 20; i++)
        {
            c = Logo.color;
            b = Black.color;
            t = textgui.color;
            c.a = 1 - i / 20f;
            b.a = c.a;
            Logo.color = c;
            Black.color = b;
            textgui.color = t;
            yield return null;
        }
        MainMenuStuff.SetActive(true);
        startingup = false;
    }


}
