using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_script : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] GameObject _droppedWeaponPrefab;
    float currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Flash());
        currentHP = Mathf.Clamp(currentHP - damage, 0f, maxHP);

        if (currentHP <= 0)
        {

            DestroyWeapon();
        }

    }

    private void DestroyWeapon()
    {
        GameObject droppedWeapon = Instantiate(_droppedWeaponPrefab, transform.position, transform.rotation);
        droppedWeapon.GetComponent<weaponCode>().StartCoroutine("WaitForPickUp");
        gameObject.SetActive(false);
    }

    public void RestoreHP()
    {
        currentHP = maxHP;
    }


    IEnumerator Flash()
    {

        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.08f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;

    }

}
