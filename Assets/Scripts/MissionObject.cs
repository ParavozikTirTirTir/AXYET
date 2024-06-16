using UnityEngine;
using System.Collections;
public class MissionObject : MonoBehaviour

{
    private MissionPlayer MP; // ���������� ������ MissionPlayer;
    public bool trigger = false;
    public string ObjectName;
    private Inventory Inv;
    private SpriteRenderer ThisObjectSprite;
    public int EmptyIndexInInventory;


    void Start() {
        MP = GameObject.FindGameObjectWithTag("Player").GetComponent<MissionPlayer>();// ���������� ��� ������ MissionPlayer ����� ��������� �� ��������� � ����� player;
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        ThisObjectSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D obj) //������ �� ������
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //���� ����� �� ������
    {
            if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // ���� ����� ����� � �������� � ����� �
        {
            //MP.IsObjectCollected = true;
            //Inv.Icon[Inv.i].sprite = ThisObjectSprite.sprite;
            //Inv.InventoryObjects.Add(ObjectName);

            EmptyIndexInInventory = Inv.InventoryObjects.IndexOf("-");
            Inv.Icon[EmptyIndexInInventory].sprite = ThisObjectSprite.sprite;
            Inv.InventoryObjects.Insert(EmptyIndexInInventory, ObjectName);
            Inv.InventoryObjects.Remove("-");
            Inv.i++;

            MP.LastAction = "�������� [" + ObjectName + "]";

            Destroy(gameObject); // � ������� ���� ������ �� �����;
        }
    }

    void OnGUI() //������ �������
    {
        if (trigger) //����� �������� �� ������
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 90, 25), "[E] �������");
        }
    }
}
