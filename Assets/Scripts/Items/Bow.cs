using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEV;
using TMPro;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private TMP_Text _arrowsText;
    [SerializeField] private Transform _quiver;

    private Inventory _inventory;
    private ScriptableEvent _onBowAiming;

    private Camera _mainCamera;

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
            var arrow = _quiver.GetChild(0);
            StartCoroutine(ShootArrow(arrow));
            _inventory.ChangeArrowCount(-1);
            _arrowsText.text = _inventory.Arrows.ToString();
        }
    }

    IEnumerator ShootArrow(Transform arrow)
    {
        arrow.gameObject.SetActive(true);
        var fireDistance = _mainCamera.transform.forward * 10f;
        while (Vector3.Distance(arrow.position, fireDistance) > 1)
        {
            arrow.position = Vector3.Lerp(arrow.position, fireDistance, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(4);
        arrow.gameObject.SetActive(false);
        arrow.position = transform.position;
        arrow.SetAsLastSibling();
    }
}
