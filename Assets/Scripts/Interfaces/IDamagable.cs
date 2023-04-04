public interface IDamagable
{
    float health { get; set; }

    public void TakeDamage(float damage);
}