﻿using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour
{
    private static GameObject _playingAudio;
    private static string _playingAudioName;

    // Use this for initialization
    void Start()
    {
        if (_playingAudio != null)
        {
            // If a same music is already playing.
            // Stop play myself.
            if (_playingAudio != gameObject && _playingAudioName == name)
            {
                Destroy(gameObject);
                return;
            }
        }
        Destroy(_playingAudio);
        DontDestroyOnLoad(gameObject);
        _playingAudio = gameObject;
        _playingAudioName = name;
    }
}
