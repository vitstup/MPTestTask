using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public int points { get => 1; }
    public float health { get => 0.1f; }

    public void Collect(ICanCollect collector)
    {
        collector.Collected(this);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICanCollect collector = collision.GetComponent<ICanCollect>();
        if (collector != null) Collect(collector);
    }
}