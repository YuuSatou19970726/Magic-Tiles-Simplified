using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    [SerializeField] private LayerMask whatIsLayer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        this.GetHitTile(this.whatIsLayer);
    }

    public void GetHitTile(LayerMask layerMask)
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed) return;
        if (!(Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)) return;

        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero, layerMask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
