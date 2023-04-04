public interface ICollectable
{
    public int points { get; }
    public float health { get; }
    public void Collect(ICanCollect collector);
}