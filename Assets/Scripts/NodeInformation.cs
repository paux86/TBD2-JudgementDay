using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInformation : MonoBehaviour
{
    string nodeId;
    NodeInformation[] nextNodes = new NodeInformation[5];
    NodeInformation[] previousNodes = new NodeInformation[5];
    int numNextNode;
    int numPreviousNode;
    int row;
    int col;
    Vector2 nodePoint;

    

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

    public void SetNodePoint(Vector2 point)
    {
        this.nodePoint = point;
    }

    public Vector2 GetNodePoint()
    {
        return this.nodePoint;
    }

    public void AddNextNode(NodeInformation node)
    {
        nextNodes[numNextNode] = node;
        numNextNode++;
    }

    public void AddPreviousNode(NodeInformation node)
    {
        previousNodes[numPreviousNode] = node;
        numPreviousNode++;
    }

    public int GetNumPreviousNodes()
    {
        return this.numPreviousNode;
    }

    public int GetNumNextNodes()
    {
        return this.numNextNode;
    }


}
