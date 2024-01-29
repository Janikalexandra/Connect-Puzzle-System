using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float rotationSpeed = 5.0f;
    private bool invertRotation = false;

    private const string mouseX = "Mouse X";
    private const string mouseY = "Mouse Y";

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        float h = invertRotation ? rotationSpeed * Input.GetAxis(mouseX) : -rotationSpeed * Input.GetAxis(mouseX);
        float v = invertRotation ? rotationSpeed * Input.GetAxis(mouseY) : -rotationSpeed * Input.GetAxis(mouseY);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            gameObject.transform.Rotate(v, h, 0);
        }
    }
}
