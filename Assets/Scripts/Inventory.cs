using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class Inventory : MonoBehaviour
{
    //public ���������[] ���������������;
    public Image[] Icon; // ����� � ���������
	public Sprite[] Sprites; //������ ��� �� �������� ���� ��������, ������� �� ������ � ������
    private MissionObject MO;
    public List<string> InventoryObjects = new List<string>();

    public int i = 0;
}