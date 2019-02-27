using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BasePlayer : MonoBehaviour
{
    public PlayerType PlayerType { get; set; }

    IDictionary<Piece, Position> pieces;

    protected virtual void Start()
    {
        pieces = new Dictionary<Piece, Position>(64);
    }
    public virtual void SetPlayer(PlayerType playerType)
    {
        this.PlayerType = playerType;
    }
}
