using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicControl : MonoBehaviour
{
    public AudioClip[] audioList;
    public AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        player.clip = audioList[0];
        player.Play();
        Invoke("changeMusic", audioList[0].length + 0.5f) ;
    }

    void changeMusic()
    {
        player.clip = audioList[1];
        player.Play();
        player.loop = true;
    }
    // Update is called once per frame
    void Update()
    {
        /*if (!player.isPlaying)
        {
            player.clip = audioList[1];
            player.Play();
            player.loop = true;
        }*/
    }
}
