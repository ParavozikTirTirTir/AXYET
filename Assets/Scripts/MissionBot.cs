using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionBot : MonoBehaviour
{
    public bool trigger = false;
    public bool vis;
    public bool CanGiveAnItem;
    public bool MissionDone = false; //����������, ������� �����������, ��� ����� ��� ������
    public string MissionName; // ����� ������� ����� ���������� ������������ ������
    public string MissionDialoge; // ����� ������� � ������
    public string MissionDialogeDone; // ����� ������� � ������
    public string MissionObjectName; //��������� �������
    public string MissionPriority = "1";

    public string RewardName; //��������� �������
    public Sprite RewardSprite;

    private MissionManager MM;
    private Inventory Inv;
    public PlayerController PC;
    public PlayerCombatController PCC;
    public IsPlayerInDialoge PinD;
    public int ObjectIndexInInventory;
    public int EmptyIndexInInventory;
    public int MoneyForMission;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();
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
            PinD.InDialoge = true;
        }
    }

    void DialogeState()
    {
        PC.movementSpeed = 0;
        PC.jumpForce = 0;
        PC.dashSpeed = 0;
        PCC.combatEnabled = false;
    }

    void DialogeExit()
    {
        PC.movementSpeed = 7;
        PC.jumpForce = 16;
        PC.dashSpeed = 20;
        PCC.combatEnabled = true;
    }

    void OnGUI()
    {
        if (vis)
        {
            DialogeState();

            if (!MM.MissionsInProgress.Contains(MissionName) && !MissionDone) //���� ����� ��� �� ���� � �� ��������;
            {
                GUI.Box(new Rect((Screen.width - 300) / 2, (Screen.height - 300) / 2, 300, 300), MissionName); //�� ������ ������������ ���� � ��������� ������;
                GUI.Label(new Rect((Screen.width - 300) / 2 + 10, (Screen.height - 300) / 2 + 20, 290, 250), MissionDialoge); //������� ��������� �����;
                if (GUI.Button(new Rect((Screen.width - 100) / 2 - 65, (Screen.height - 300) / 2 + 250, 110, 40), "������� �����")) // ��� ������� �� ������ Ok;
                {
                    MM.MissionsInProgress.Add(MissionName);
                    MM.MissionsPriority.Add(MissionPriority);
                    vis = false; // ��� ���������� ���� �����������;
                    PinD.InDialoge = false;

                    MM.LastAction = "������ ����� [" + MissionName + "]";
                    DialogeExit();
                }

                if (GUI.Button(new Rect((Screen.width - 100) / 2 + 55, (Screen.height - 300) / 2 + 250, 110, 40), "����������"))
                {
                    vis = false;
                    PinD.InDialoge = false;
                    DialogeExit();
                }
            }

            if (MM.MissionsInProgress.Contains(MissionName) && !MissionDone) // ���� ����� ��� ����, �� �� ��������;
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

                        MM.LastAction = "�������� ����� [" + MissionName + "]";
                        MM.MissionsInProgress.Remove(MissionName); // ������� ����� �� ������ ��������
                        MM.MissionsPriority.Remove(MissionPriority);

                        if (CanGiveAnItem)
                        {
                            EmptyIndexInInventory = Inv.InventoryObjects.IndexOf("-");
                            Inv.Icon[EmptyIndexInInventory].sprite = RewardSprite;
                            Inv.InventoryObjects.Insert(EmptyIndexInInventory, RewardName);
                            Inv.InventoryObjects.Remove("-");

                            MM.LastAction = "�������� ����� [" + MissionName + "] � ������� ������� [" + RewardName + "]";
                        }

                        MM.Money = MM.Money + MoneyForMission; //���������� ����� �� ���������� ������;
                        vis = false; // ���������� ���� �����������;
                        PinD.InDialoge = false;
                        MissionDone = true;

                        DialogeExit();
                    }
                }

                else
                { // ���� �� ��� �� ��������� ������;
                    if (GUI.Button(new Rect((Screen.width - 100) / 2, (Screen.height - 300) / 2 + 250, 100, 40), "���")) // �� ������ ������ ��, ����� ������ ���;
                    {
                        vis = false; // ��� ������� �� �������, ���� ������ ���������;
                        PinD.InDialoge = false;

                        DialogeExit();
                    }
                }
            }
        }
    }
}
   