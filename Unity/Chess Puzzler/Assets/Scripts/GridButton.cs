using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridButton : MonoBehaviour
{
    public int Rank;
    public char File;
    [field: SerializeField]
    public Button Button { get; set; }
    [field: SerializeField]
    public Image PieceImage { get; set; }
    [field: SerializeField]
    public Image GridImage { get; set; }
    [field: SerializeField]
    public TextMeshProUGUI Text { get; set; }

}
