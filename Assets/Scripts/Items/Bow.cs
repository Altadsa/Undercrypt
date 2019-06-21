using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GEV;
using TMPro;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private TMP_Text _arrowsText;
    [SerializeField] private List<Transform> _quiver;

    private Inventory _inventory;
    private ScriptableEvent _onBowAiming;

    private Camera _mainCamera;

    readonly Vector3 _veiwportCentre = new Vector3(0.5f,0.5f, 0);

    private void OnEnable()
    {
        _inventory = Inventory.Instance;
        _arrowsText.text = _inventory.Arrows.ToString();
        _mainCamera = Camera.main;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) && _inventory.Arrows > 0)
        {
            var arrow = _quiver[0];
            StartCoroutine(ShootArrow(arrow));
            _inventory.ChangeArrowCount(-1);
            _arrowsText.text = _inventory.Arrows.ToString();
        }
    }

    private float _despawnTime = 10;
    IEnumerator ShootArrow(Transform arrow)
    {
        arrow.gameObject.SetActive(true);
        var fireDistance = _mainCamera.ViewportToWorldPoint(_veiwportCentre) * 10f;
        float _currentTime = 0;
        while (_currentTime < _despawnTime)
        {
            arrow.position = Vector3.Lerp(arrow.position, fireDistance, Time.deltaTime);
            _currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        arrow.gameObject.SetActive(false);
        arrow.position = transform.position;
    }
}
