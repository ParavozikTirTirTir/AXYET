using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class Inventory : MonoBehaviour
{
    public Image[] Icon; // ����� � ���������
    public Sprite[] Sprites; //������ ��� �� �������� ���� ��������, ������� �� ������ � ������
    private MissionObject MO;
    private MissionPlayer MP;
    //private Canvas canvas;
    public List<string> InventoryObjects = new List<string>();
    public Texture2D CloseButton;

    void Start()
    {
        MP = GameObject.FindGameObjectWithTag("Player").GetComponent<MissionPlayer>();
    }

    void OnGUI() //������ �������
    {
        for (int i = 0; i < InventoryObjects.Count; i++)
        {

            if (InventoryObjects[i] != "-")
            {
                if (GUI.Button(new Rect(50, 50, 30, 30), CloseButton))
                {
                    Icon[i].sprite = Sprites[4];
                    InventoryObjects[i] = "-";

                    MP.LastAction = "�������� ������� [" + InventoryObjects[i] + "]";
                }
            }
        }
    }
}