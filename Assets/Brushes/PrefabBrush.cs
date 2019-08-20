using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefab brush", menuName = "Brushes/Prefab Brush")]
[CustomGridBrush(false, true, false, "Prefab Brush")]
public class PrefabBrush : GridBrushBase
{
    public GameObject prefab;
    public int zPosition = 0;

    private GameObject previousBrushTarget;
    private Vector3Int previousPosition;

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        //check to see if tile is already placed here.
        Transform itemInCell = GetObjectInCell(gridLayout, brushTarget.transform, new Vector3Int(position.x, position.y, zPosition));
        if (itemInCell)
        { return; }
        if (position == previousPosition)
        { return; }

        previousPosition = position;
        if (brushTarget)
        { previousBrushTarget = brushTarget; }
        brushTarget = previousBrushTarget;

        //Do not allow editing palettes.
        if (brushTarget.layer == 31)
        { return; }

        //GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        if (instance)
        {
            Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint prefab");
            Undo.RegisterCreatedObjectUndo(instance, "Paint prefab");
            instance.transform.SetParent(brushTarget.transform);
            instance.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(new Vector3(position.x, position.y, zPosition) + (Vector3.one * 0.5f)));
        }
    }

    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget)
        { previousBrushTarget = brushTarget; }
        brushTarget = previousBrushTarget;

        if (brushTarget.layer == 31)
        { return; }

        Transform erased = GetObjectInCell(gridLayout, brushTarget.transform, new Vector3Int(position.x, position.y, zPosition));
        if (erased)
        { Undo.DestroyObjectImmediate(erased.gameObject); }
        //do not allow editing palettes.
    }

    private static Transform GetObjectInCell(GridLayout gridLayout, Transform parent, Vector3Int position)
    {
        int childCount = parent.childCount;
        Vector3 min = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position));
        Vector3 max = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position + Vector3Int.one));
        Bounds bounds = new Bounds((max + min) * 0.5f, max - min); //The bounds of a tile

        //Checks child objects position against bounds of tile.

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (bounds.Contains(child.position))
            {
                return child;
            }
        }

        return null;
    }
}
