public interface ICanCollect
{
    int points { get; set; }

    public void Collected(ICollectable collectable);
}