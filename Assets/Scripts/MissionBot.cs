using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionBot : MonoBehaviour
{
    public bool trigger = false;
    public bool CanGiveAnItem;
    public bool vis; // ����������, ������� ����� ���������� ������ ����� �����������
    public bool MissionDone = false; //����������, ������� �����������, ��� ����� ��� ������
    public string MissionName; // ����� ������� ����� ���������� ������������ ������
    public string MissionDialoge; // ����� ������� � ������
    public string MissionDialogeDone; // ����� ������� � ������
    public string MissionObjectName; //��������� �������

    public string RewardName; //��������� �������
    public Sprite RewardSprite;

    private MissionPlayer MP; // ���������� ������ MissionPlayer
    private Inventory Inv;
    public int ObjectIndexInInventory;
    public int EmptyIndexInInventory;

    void Start()
    {
        MP = GameObject.FindGameObjectWithTag("Player").GetComponent<MissionPlayer>(); // ���������� ��� ������ MissionPlayer ����� ��������� �� ��������� � ����� player;
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void OnTriggerStay2D(Collider2D obj) //����� ����� � ���
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //����� ������ �� ���
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update()
    {
        GameObject MissionTagScanner = GameObject.FindGameObjectWithTag("Player"); // �������� � �������� ����� ����� ����� ����������������� ������ � ��� �������� � �������� ��� Player;

        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !MissionDone) // ��� ������� �� ������� � � ���� ����� ����� � ���
        {
            vis = true;
        }
    }

    void OnGUI()
    {
        if (vis)
        {
            if (!MP.MissionsInProgress.Contains(MissionName) && !MissionDone) //���� ����� ��� �� ���� � �� ��������;
            {
                GUI.Box(new Rect((Screen.width - 300) / 2, (Screen.height - 300) / 2, 300, 300), MissionName); //�� ������ ������������ ���� � ��������� �����;
                GUI.Label(new Rect((Screen.width - 300) / 2 + 10, (Screen.height - 300) / 2 + 20, 290, 250), MissionDialoge); //������� ��������� �����;
                if (GUI.Button(new Rect((Screen.width - 100) / 2 - 25, (Screen.height - 300) / 2 + 250, 150, 40), "������� �����")) // ��� ������� �� ������ Ok;
                {
                    MP.MissionsInProgress.Add(MissionName); // �������� ������;
                    vis = false; // ��� ���������� ���� �����������;

                    MP.LastAction = "������ ����� [" + MissionName + "]";
                }
            }

            if (MP.MissionsInProgress.Contains(MissionName) && !MissionDone) // ���� ����� ��� ����, �� �� ��������;
            {
                GUI.Box(new Rect((Screen.width - 300) / 2, (Screen.height - 300) / 2, 300, 300), MissionName);
                GUI.Label(new Rect((Screen.width - 300) / 2 + 10, (Screen.height - 300) / 2 + 20, 290, 250), MissionDialogeDone); //�� �������� ������ �������� �� ������ �����;
                if (Inv.InventoryObjects.Contains(MissionObjectName))
                {
                    if (GUI.Button(new Rect((Screen.width - 100) / 2, (Screen.height - 300) / 2 + 250, 100, 40), "��")) // �� �������� ������ ��, ��� ������� �� �������;
                    {
                        ObjectIndexInInventory = Inv.InventoryObjects.IndexOf(MissionObjectName);
                        Inv.Icon[ObjectIndexInInventory].sprite = Inv.Sprites[4];
                        Inv.InventoryObjects.Insert(ObjectIndexInInventory, "-");
                        Inv.InventoryObjects.Remove(MissionObjectName);

                        MP.LastAction = "�������� ����� [" + MissionName + "]";
                        MP.MissionsInProgress.Remove(MissionName); // ������� ����� �� ������ ��������

                        if (CanGiveAnItem)
                        {
                            EmptyIndexInInventory = Inv.InventoryObjects.IndexOf("-");
                            Inv.Icon[EmptyIndexInInventory].sprite = RewardSprite;
                            Inv.InventoryObjects.Insert(EmptyIndexInInventory, RewardName);
                            Inv.InventoryObjects.Remove("-");
                            Inv.i++;

                            MP.LastAction = "�������� ����� [" + MissionName + "] � ������� ������� [" + RewardName + "]";
                        }

                        MP.Money = MP.Money + 100; //���������� ����� �� ���������� ������;
                        vis = false; // ���������� ���� �����������;
                        MissionDone = true;
                    }
                }

                else
                { // ���� �� ��� �� ��������� ������;
                    if (GUI.Button(new Rect((Screen.width - 100) / 2, (Screen.height - 300) / 2 + 250, 100, 40), "���")) // �� ������ ������ ��, ����� ������ ���;
                    {
                        vis = false; // ��� ������� �� �������, ���� ������ ���������;
                    }
                }
            }
        }
    }
}
   