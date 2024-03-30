using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Push : MonoBehaviour
{
    public float pushForce;
    private float _currentPushForce;
    private bool _isPressed;

    NewBehaviourScript d;
    // Start is called before the first frame update
    void Start()
    {
        d = new NewBehaviourScript();
        d.InitDomino2();
    }

    //public void Start()
    //{
    //    Physics.autoSimulation = false;

    //    for (int i = 0; i < 200; i++)
    //    {
    //        createWall();
    //        Physics.Simulate(Time.fixedDeltaTime);
    //    }
    //    Physics.autoSimulation = true;
    //}

    void createWall()
    {
        float x = Random.Range(-20f, 20f);
        float z = Random.Range(-20f, 20f);
        Vector3 position = new Vector3(x, 1.51f, z);
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        int scalex = Random.Range(2, 5);
        Collider[] colliders = Physics.OverlapBox(position, new Vector3(scalex, 1, 1) / 2, rotation);

        if (colliders.Length == 0)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.localPosition = position;
            wall.transform.localScale = new Vector3(scalex, 3, 1);
            wall.transform.rotation = rotation;
            wall.tag = "wall";
        }
    }

    // Update is called once per frame

    public float moveSpeed = 5f; // �������� ����������� ������
    public float rotationSpeed = 5f;
    public float maxHeight = 100f; // ������������ ������ ������
    public float minHeight = 1f; // ����������� ������ ������
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            d.StartFirstDomino();
        }

        //Debug.Log(_currentPushForce);
        //if (_isPressed && _currentPushForce <= 60)
        //{
        //    _currentPushForce += 2.5f;
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    _isPressed = true;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 100))
        //    {
        //        GameObject domino = hit.transform.gameObject;
        //        if (domino.GetComponent<MeshFilter>().sharedMesh.ToString() == "Cube (UnityEngine.Mesh)")
        //        {
        //            Rigidbody domino_rb = domino.GetComponent<Rigidbody>();
        //            Vector3 dir = ray.origin - domino_rb.transform.position;
        //            dir.Normalize();
        //            domino_rb.AddRelativeForce(dir * _currentPushForce, ForceMode.Impulse);
        //        }
        //    }
        //    _currentPushForce = pushForce;
        //    _isPressed = false;
        //}

        /*
        //�������� ������
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ��������� ����������� ��������
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // ��������� ����������� ������
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        //������� ������
        if (Input.GetMouseButton(1)) // ���������, ������ �� ������ �������� ����
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

            // ������� ������ ������ �������� ������� �� ��� Y
            transform.RotateAround(Camera.main.transform.position, Vector3.up, mouseX);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
        }

        // ��������� ������ ��� ������� E
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }

        // ����������� ������ ������
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minHeight, maxHeight), transform.position.z);
        //*/
    }
}
