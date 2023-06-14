using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public bool isGround;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        //判断该点是否和某个Layer重叠
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+bottomOffset, checkRadius);
        //画线
    }

}
