using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    private Color cellColor = Color.green;
    private Renderer renderer;
    public Position CellPosition { get; set; }

    public void SetCell(Position position)
    {
        this.gameObject.name = "Cell"+position.ToString();
        this.CellPosition = position;
        this.gameObject.transform.position = new Vector3(CellPosition.X,0,CellPosition.Y);
        this.renderer = GetComponent<Renderer>();
        this.renderer.material.color = this.cellColor;
    }
}
