using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update
    Text title;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float with = title.rectTransform.sizeDelta.x;
    }

    public void Level_1Botton()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadSceneAsync(1);
    }
}
