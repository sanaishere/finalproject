using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroller : MonoBehaviour
{
    public AudioSource audioshoot;
    // Start is called before the first frame update
    public void PlayAudio()
    {
        audioshoot.Play();
        
    }
    void Start()
    {
        Destroy(gameObject, 1f);
    }
}
