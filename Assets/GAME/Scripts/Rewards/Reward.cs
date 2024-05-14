using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : MonoBehaviour
{
    protected IEnumerator Aminate()
    {
        Vector2 restingPosition = transform.localPosition;
        Vector2 aminatingPosition = restingPosition + Vector2.up * 2f;

        yield return Move(restingPosition, aminatingPosition);
        //yield return Move(aminatingPosition, restingPosition);

        //Destroy(gameObject);
        ObjectPoolManager.Instance.Return(gameObject);
    }

    private IEnumerator Move(Vector2 fromPosition, Vector2 toPosition)
    {
        float elapsed = 0f;
        float duration = 0.2f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector2.Lerp(fromPosition, toPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = toPosition;
    }
}
