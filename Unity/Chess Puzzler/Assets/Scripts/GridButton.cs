using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GridButton : MonoBehaviour
{
    public int Rank;
    public char File;
    [field: SerializeField]
    public Button Button { get; private set; }
    [field: SerializeField]
    public Image Image { get; set; }
    [field: SerializeField]
    public TextMeshProUGUI Text { get; set; }

    private void Awake()
    {
        Button = GetComponent<Button>();
        Image = GetComponent<Image>();
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }
}
