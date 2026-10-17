using UnityEngine;

public interface ITool
{
    GameObject PickupPrefab { get; }
    ToolInput Input { get; }

    bool Use(RaycastHit hit);
}