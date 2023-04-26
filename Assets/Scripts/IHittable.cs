using UnityEngine;

public interface IHitable
{
    public void ReceiveHit(RaycastHit2D hit);
    void TakeDamage(int v);
}
