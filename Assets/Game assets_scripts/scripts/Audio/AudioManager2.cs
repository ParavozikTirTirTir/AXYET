using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    //������������� ������ ��������
    public static AudioManager2 instance;

    //��������� ������ �� ��������
    void Awake() { instance = this; }

    public List<AudioClip> sfxLibrary;
    public AudioClip sfx_jump, sfx_char, sfx_dash, sfx_attack, sfx_attack2, sfx_enemy1attack, sfx_enemy2attack, sfx_bow;

    public AudioClip music_back;
    //������� ������
    public GameObject currentMusicObject;

    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "jump":
                SoundObjectCreation(sfx_jump, 0.05f);
                break;
            case "char":
                SoundObjectCreation(sfx_char, 0.05f);
                break;
            case "dash":
                SoundObjectCreation(sfx_dash, 0.05f);
                break;
            case "attack":
                SoundObjectCreation(sfx_attack, 0.02f);
                break;
            case "attack2":
                SoundObjectCreation(sfx_attack2, 0.02f);
                break;
            case "en1_attack":
                SoundObjectCreation(sfx_enemy1attack, 0.01f);
                break;
            case "en2_attack":
                SoundObjectCreation(sfx_enemy2attack, 0.01f);
                break;
            case "bow":
                SoundObjectCreation(sfx_bow, 0.01f);
                break;
            default:
                break;
        }
    }


    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "music_back":
                MusicObjectCreation(music_back);
                break;
            default:
                break;
        }
    }


    void SoundObjectCreation(AudioClip clip, float volume)
    {
        //������� �������� ������
        GameObject newObject = Instantiate(soundObject, transform);
        //����������� ������ ����
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().volume = volume;
        newObject.GetComponent<AudioSource>().loop = false;
        //������������� ����
        newObject.GetComponent<AudioSource>().Play();

    }

    public void MusicObjectCreation(AudioClip clip)
    {
        //��������� ���������� �� ������
        if (currentMusicObject)
            Destroy(currentMusicObject);
        //������� �������� ������
        GameObject newObject = Instantiate(soundObject, transform);
        //����������� ������ ����
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().volume = 0.01f;
        //����������� �������� �����
        newObject.GetComponent<AudioSource>().loop = true;
        //������������� ����
        newObject.GetComponent<AudioSource>().Play();

    }




}
