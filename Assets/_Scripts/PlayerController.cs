using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(horizontalValue * 5f, rb.linearVelocity.y, verticalValue * 5f);
    }
}
