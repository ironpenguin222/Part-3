using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI currentSelection;
    public static CharacterControl instance;
    public static Villager SelectedVillager { get; private set; }

    public List<Villager> availableVillagers; // List of available villagers to assign

    private int previousDropdownValue = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        int currentDropdownValue = dropdown.value;
        if (currentDropdownValue != previousDropdownValue && currentDropdownValue >= 0 && currentDropdownValue < availableVillagers.Count)
        {
            previousDropdownValue = currentDropdownValue;
            SetSelectedVillager(availableVillagers[currentDropdownValue]);
        }
    }

    public static void SetSelectedVillager(Villager villager)
    {
        if (SelectedVillager != null)
        {
            SelectedVillager.Selected(false);
        }
        SelectedVillager = villager;
        SelectedVillager.Selected(true);
        instance.currentSelection.text = villager.ToString();
    }
}


