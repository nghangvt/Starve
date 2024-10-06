using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public GameObject strike;
    public Transform attackPos;
    public float AttackSpeed = 0.5f;
    public float attackTime;
    private float attackSpeed;
    public int hitCount = 0;

    void Update()
    {
        RotateAttack();
        attackSpeed -= Time.deltaTime;
        if(Input.GetMouseButton(0) && attackSpeed < 0)
        {
            palmStrike();
        }
    }

    void RotateAttack()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0,0,angle);
        transform.rotation = rotation;
    }

    void palmStrike()
    {
        attackSpeed = AttackSpeed;
        //Lay vi tri con tro chuot va tinh goc xoay
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - attackPos.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        //Tao ra trike voi goc xoay da tinh
        GameObject strikeTmp = Instantiate(strike, attackPos.position, rotation);

        Destroy(strikeTmp, 0.2f);
    }
}
