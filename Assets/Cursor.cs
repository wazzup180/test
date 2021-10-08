using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cursor : MonoBehaviour
{
    public float Speed = 10.0f;
    public LayerMask SelectMask;
    public LayerMask Placemask;
    private RectTransform rect;

    // Start is called before the first frame update
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private bool _isRelocating = false;
    private GameObject SelectedFactory;

    // Update is called once per frame
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(rect.position);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.black);

        RaycastHit hit;
        if (_isRelocating)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, Placemask))
            {
                float yy = SelectedFactory.transform.localScale.y / 2.0f;
                SelectedFactory.transform.position = hit.point + new Vector3(0, yy, 0);
                if (Input.GetButtonDown("South"))
                {
                    Factory factory = SelectedFactory.GetComponent<Factory>();
                    factory.enabled = true;
                    _isRelocating = false;
                }
            }
        }
        else
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, SelectMask))
            {
                Debug.Log("Factory");

                if (Input.GetButtonDown("South"))
                {
                    SelectedFactory = hit.transform.gameObject;
                    Factory factory = SelectedFactory.GetComponent<Factory>();
                    factory.enabled = false;
                    _isRelocating = true;
                }
            }
        }

        // get input
        Vector2 joy = new Vector2(Input.GetAxis("RightJoyX"), -Input.GetAxis("RightJoyY"));
        if (joy.magnitude < 0.3f) { return; }
        joy.Normalize();

        //local variables
        float width = Screen.width;
        float height = Screen.height;
        float multiplier = Speed * Time.deltaTime;
        Vector2 anchor = rect.anchoredPosition;

        //update their values
        float x = anchor.x + joy.x * multiplier;
        x = Mathf.Clamp(x, -width / 2, width / 2);

        float y = anchor.y + joy.y * multiplier;
        y = Mathf.Clamp(y, -height / 2, height / 2);

        //set anchor
        anchor = new Vector2(x, y);
        rect.anchoredPosition = anchor;
    }
}