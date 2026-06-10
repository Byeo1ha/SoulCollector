using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class SortingLayerRenderer : MonoBehaviour
{
    [SerializeField] private Transform sortPoint;
    [SerializeField] private int orderOffset;

    private SortingGroup sortingGroup;

    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();

        if (sortPoint == null)
        {
            sortPoint = transform;
        }
    }

    private void LateUpdate()
    {
        if (sortingGroup == null)
            return;

        sortingGroup.sortingOrder = -(int)(sortPoint.position.y * 100) + orderOffset;
    }
}
