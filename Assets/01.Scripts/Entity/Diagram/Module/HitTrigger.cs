using System;
using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    public event Action<Collider2D> OnHit;

    private void Awake()
    {
        Debug.LogWarning("Need pooling");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cell"))
        {
            Destroy(collision.gameObject);
            CellSpawner.Instance.ModifyCellCount(-1);

            OnHit?.Invoke(collision);
        }
    }
}
