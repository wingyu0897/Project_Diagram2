//using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Ripple : Entity
{
    private SpriteRenderer _sprite;

    //private Sequence _seq;
    private int _cellLayer;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _cellLayer = 1 << LayerMask.NameToLayer("Cell");
    }

    public void Initalize(float radius, float duration)
    {
        float size = radius * 2.0f;

        transform.localScale = Vector3.zero;
        _sprite.color = Color.white;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, _cellLayer);

        //StartCoroutine(Hits(hits, radius, duration));
        StartCoroutine(Test(hits, radius, duration));
    }

    private IEnumerator Hits(Collider2D[] hits, float radius, float duration)
    {
        float[] distances = new float[hits.Length];
        for (int i = 0; i < hits.Length; i++)
            distances[i] = Vector2.Distance(transform.position, hits[i].transform.position);

        WaitForSeconds wait = new WaitForSeconds(0.1f);

        for (float i = 0; i < duration; i += 0.1f)
        {
            for (int j = 0; j < hits.Length; j++)
            {
                if (hits[j] != null 
                    && distances[j] < Ease.OutQuint(i / duration) * radius)
                {
                    CellManager.Instance.DestroyCell(hits[j].gameObject);
                    hits[j] = null;
                }
            }

            yield return wait;
        }
    }

    private IEnumerator Test(Collider2D[] hits, float radius, float duration)
    {
        float timer = 0f;
        float progress;
        float fadeProgress;

        float size = radius * 2f;

        float[] distances = new float[hits.Length];
        for (int i = 0; i < hits.Length; i++)
            distances[i] = Vector2.Distance(transform.position, hits[i].transform.position);


        while (timer < duration)
        {
            progress = Mathf.Clamp01(timer / duration);
            fadeProgress = Mathf.Clamp01(progress * 2f - 1f);

            transform.localScale = Vector3.one * size * Ease.OutQuint(progress);
            _sprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, fadeProgress));

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] != null
                    && distances[i] < Ease.OutQuint(progress) * radius)
                {
                    CellManager.Instance.DestroyCell(hits[i].gameObject);
                    hits[i] = null;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

        _sprite.color = new Color(1f, 1f, 1f, radius);

        PoolManager.Instance.Push(this);
    }
}
