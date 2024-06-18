using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionChest : MonoBehaviour
{
    public bool trigger = false;
    public bool CanGiveAnItem; //������ ���� ������� ��� ���

    public bool MissionDone = false; //����������, ������� �����������, ��� ����� ��� ������
    public string MissionName; // ����� ������� ����� ���������� ������������ ������
    public string MissionObjectName; //��������� �������
    public string MissionPriority = "2";

    public string RewardName; //������� �� ������� ���� ��� ����
    public Sprite RewardSprite; //������ ������� �� �������
    public Sprite OpenedChest; //������ ��������� �������

    private MissionManager MM;
    private Inventory Inv;
    public int ObjectIndexInInventory;
    public int EmptyIndexInInventory;
    public int MoneyForMission;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
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
        GameObject MissionTagScanner = GameObject.FindGameObjectWithTag("Player");

        if (!MM.MissionsInProgress.Contains(MissionName) && !MissionDone && trigger) //���� ����� ��� �� ���� � �� ��������;
        {
            MM.MissionsInProgress.Add(MissionName);
            MM.MissionsPriority.Add(MissionPriority);
            MM.LastAction = "������ ����� [" + MissionName + "]";
        }

        // �������� �������
        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !MissionDone && MM.MissionsInProgress.Contains(MissionName) && Inv.InventoryObjects.Contains(MissionObjectName))
        {
            ObjectIndexInInventory = Inv.InventoryObjects.IndexOf(MissionObjectName);
            Inv.Icon[ObjectIndexInInventory].sprite = Inv.Sprites[4];
            Inv.InventoryObjects.Insert(ObjectIndexInInventory, "-");
            Inv.InventoryObjects.Remove(MissionObjectName);

            MM.LastAction = "������ ������";
            MM.MissionsInProgress.Remove(MissionName);
            MM.MissionsPriority.Remove(MissionPriority);

            GetComponent<SpriteRenderer>().sprite = OpenedChest;

            MM.Money = MM.Money + MoneyForMission; //���������� ����� �� ���������� ������;
            MissionDone = true;
        }
    }

    void OnGUI() //������ �������
    {
        if (trigger && !MissionDone && Inv.InventoryObjects.Contains(MissionObjectName))
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 110, 25), "[�] �������");
        }
    }
}
   