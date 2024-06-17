using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class KillSound : MonoBehaviour
{
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        if (!source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
