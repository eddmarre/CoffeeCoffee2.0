using UnityEngine;

public interface IMonitorOrderButton
{
    void SetCollision(bool value);
    public void SetColor(Material color);
    string GetButtonOrderName();
}