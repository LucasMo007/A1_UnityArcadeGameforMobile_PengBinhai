using UnityEngine;

public class PlayerCouscous : MonoBehaviour
{
    [SerializeField] int couscousAmmo = 0;

    public void AddAmmo(int amount)
    {
        couscousAmmo += amount;
        Debug.Log("Couscous ammo: " + couscousAmmo);
    }

    public bool CanFire()
    {
        return couscousAmmo > 0;
    }

    public void UseAmmo()
    {
        couscousAmmo = Mathf.Max(0, couscousAmmo - 1);
    }

    public int GetAmmo()
    {
        return couscousAmmo;
    }
}
