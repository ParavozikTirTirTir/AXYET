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
    public AudioClip sfx_jump, sfx_hit, sfx_char, sfx_dash, sfx_attack;

    public AudioClip music_back;
    //������� ������
    public GameObject currentMusicObject;

    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "jump":
                SoundObjectCreation(sfx_jump);
                break;
            case "hit":
                SoundObjectCreation(sfx_hit);
                break;
            case "char":
                SoundObjectCreation(sfx_char);
                break;
            case "dash":
                SoundObjectCreation(sfx_dash);
                break;
            case "attack":
                SoundObjectCreation(sfx_attack);
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
                SoundObjectCreation(music_back);
                break;
            default:
                break;
        }
    }


    void SoundObjectCreation(AudioClip clip)
    {
        //������� �������� ������
        GameObject newObject = Instantiate(soundObject, transform);
        //����������� ������ ����
        newObject.GetComponent<AudioSource>().clip = clip;
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
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        //����������� �������� �����
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        //������������� ����
        currentMusicObject.GetComponent<AudioSource>().Play();

    }
}
