using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float range = 50f;
    public float damage = 10f;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public LayerMask hitMask;
    public void Shoot()
    {
        RaycastHit hit;
        Vector3 endPosition = firePoint.position + firePoint.forward * range;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range, hitMask))
        {
            endPosition = hit.point;
            
            // Aplica daño si el objeto tiene un script con TakeDamage
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<EnemyScript>().downLife();
            }
        }

        StartCoroutine(DrawLaser(endPosition));
    }

    IEnumerator DrawLaser(Vector3 endPosition)
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, endPosition);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}
