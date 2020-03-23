using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInformation : MonoBehaviour
{
    string nodeId;
    NodeInformation[] nextNodes;
    NodeInformation[] previousNodes;
    int row;
    int col;

    public void SetNodeId(string nodeId)
    {
        this.nodeId = nodeId;
    }

    public string GetNodeId()
    {
        return this.nodeId;
    }

    public int GetRow()
    {
        return this.row;
    }

    public int GetCol()
    {
        return this.col;
    }

    public void SetRowCol(int row, int col)
    {
        this.row = row;
        this.col = col;
    }


}
