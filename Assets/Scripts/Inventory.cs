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
    public List<string> InventoryObjects = new List<string>();
    public Texture2D CloseButton;
    public bool MouseOnLabel = false;

    private MissionPlayer MP;
    void Start()
    {
        MP = GameObject.FindGameObjectWithTag("Player").GetComponent<MissionPlayer>();
    }

    void OnGUI() //������ �������
    {
        if (InventoryObjects[0] != "-")
        {
            if (GUI.Button(new Rect(100, 100, 20, 20), CloseButton))
            {
                Icon[0].sprite = Sprites[4];
                InventoryObjects[0] = "-";

                MP.LastAction = "�������� ������� [" + InventoryObjects[0] + "]";
            }
        }
    }
}