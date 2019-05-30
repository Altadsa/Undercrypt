using UnityEngine;

public class EnemyVision
{
    private Transform _transform;

    private float _maxVisionDistance = 20f;
    private Quaternion _startingAngle = Quaternion.AngleAxis(-75, Vector3.up);
    private Quaternion _stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    public EnemyVision(Transform enemyTransform)
    {
        _transform = enemyTransform;
    }

    public Transform UpdateVision()
    {
        RaycastHit hit;
        var angle = _transform.rotation * _startingAngle;
        var direction = angle * Vector3.forward;
        var pos = _transform.position;
        for (int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, _maxVisionDistance))
            {
                var player = hit.collider.GetComponent<PlayerController>();
                if (player != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return player.transform;
                }
                Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
            }
            else
            {
                Debug.DrawRay(pos, direction * _maxVisionDistance, Color.white);
            }

            direction = _stepAngle * direction;
        }

        return null;
    }
}