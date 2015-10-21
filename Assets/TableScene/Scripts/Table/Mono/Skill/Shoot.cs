using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject _projectilePrefab;
    public GameObject _target;
    public AudioSource _audio;

    // Cast the skill.
    public void Cast(int damage)
    {
        _audio.Play();
        GameObject projectileObject = Instantiate(_projectilePrefab);
        projectileObject.transform.position = transform.position;
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile._table = GameObject.Find("Table").GetComponent<PinBallTable>();
        projectile.Damage = damage;
        projectile.SetTarget(_target);
        Destroy(projectileObject, 2f);
    }
}
