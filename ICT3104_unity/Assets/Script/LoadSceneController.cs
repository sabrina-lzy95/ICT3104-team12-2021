using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System;

public class LoadSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		
		PlayerPrefs.SetString ("log","");
    }

    // Update is called once per frame
	
    void Update()
    {
        //writer.WriteLine(localDate.ToString() + "," + "Test");

	//PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+"Try");
		
		string[] cars = {PlayerPrefs.GetString("log")};
		File.WriteAllLines("./test.csv",cars );
        var pressedKey = Input.inputString;

        switch (pressedKey)
        {

            case "0":
                StartCoroutine(LoadWelcomeScene());
                break;
            case "1":
                StartCoroutine(LoadYourAsyncScene("1"));
                break;
            case "2":
                StartCoroutine(LoadYourAsyncScene("2"));
                break;
            case "3":
                StartCoroutine(LoadYourAsyncScene("3"));
                break;
            case "4":
                StartCoroutine(LoadYourAsyncScene("4"));
                break;
            case "5":
                StartCoroutine(LoadYourAsyncScene("5"));
                break;
            case "6":
                StartCoroutine(LoadYourAsyncScene("6"));
				
                break;
            case "7":
                StartCoroutine(LoadYourAsyncScene("7"));
                break;
        }

    }

    IEnumerator LoadYourAsyncScene(string key)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene " + key + "");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadWelcomeScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Welcome Scene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
