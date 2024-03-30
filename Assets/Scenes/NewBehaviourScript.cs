using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    private void OnEnable()
    {
        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Отписываемся от события загрузки сцены, чтобы избежать утечек памяти
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // В этом методе вы можете выполнить код, который должен выполняться при загрузке сцены
        Debug.Log("Сцена загружена: " + scene.name);
        Start();

        // Ваши действия, например, создание объектов
        // ObjectSpawner objectSpawner = FindObjectOfType<ObjectSpawner>();
        // if (objectSpawner != null)
        // {
        //     objectSpawner.SpawnObjects();
        // }
    }
    // Start is called before the first frame update

    public Mesh cubeMesh = Resources.Load<Mesh>("Cube"); // Меш куба
    public int numberOfObjects = 1; // Количество объектов, которые нужно создать

    float dominoHeight = 1;

    void Start()
    {
        
    }
    
    public void InitDomino()
    {
        int numberOfObjects = 1;
        // Создаем указанное количество объектов
        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //new GameObject("Cube");
            //MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
            //meshFilter.mesh = cubeMesh;

            // Добавляем компонент MeshRenderer для отображения объекта
            //MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();
            //meshRenderer.material = new Material(Shader.Find("Standard")); // Простой материал

            // Добавляем компонент BoxCollider
            //BoxCollider boxCollider = newObject.AddComponent<BoxCollider>();

            // Указываем случайную позицию для объекта
            newObject.transform.position = new Vector3(0f, 1f, 0);
            newObject.transform.localScale = new Vector3(1f, 1f, 0.2f);

            // Добавляем компонент Rigidbody к объекту
            Rigidbody rb = newObject.AddComponent<Rigidbody>();
            rb.mass = 40f;
        }
    }
    public Material black;
    GameObject GetDomino()
    {
        GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //new GameObject("Cube");
        //MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
        //meshFilter.mesh = cubeMesh;

        // Добавляем компонент MeshRenderer для отображения объекта
        //MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();
        MeshRenderer meshRenderer = newObject.GetComponent<MeshRenderer>();
        //meshRenderer.material = new Material(Shader.Find("custom_material")); // Простой материал
        string materialPath = "Assets/black.mat"; // Путь к вашему физическому материалу
        Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath); ;//Resources.Load<Material>(materialPath);
        meshRenderer.material = material;


        // Добавляем компонент BoxCollider
        //BoxCollider boxCollider = newObject.AddComponent<BoxCollider>();
        BoxCollider boxCollider = newObject.GetComponent<BoxCollider>();
        string path = "Assets/custom_material.physicMaterial"; // Путь к вашему физическому материалу
        PhysicMaterial physicMaterial = AssetDatabase.LoadAssetAtPath<PhysicMaterial>(path);
        boxCollider.material = physicMaterial;
        //boxCollider.isTrigger = true; 

        // Указываем случайную позицию для объекта
        newObject.transform.position = new Vector3(0f, 1f, 0);
        newObject.transform.localScale = new Vector3(1f, dominoHeight, 0.2f);

        // Добавляем компонент Rigidbody к объекту
        Rigidbody rb = newObject.AddComponent<Rigidbody>();
        rb.mass = 80f;
        rb.detectCollisions = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        
        //rb.isKinematic = true;//закоментировать

        newObject.tag = "Domino";
        //string path_update_script = "Assets/update.cs"; // Путь к вашему физическому материалу
        //MonoScript monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(path);

        update component = newObject.AddComponent<update>();

        return newObject;
    }
    public double angle = Math.PI / 2; //Угол поворота на 90 градусов
    public double ang1 = Math.PI / 4;  //Угол поворота на 45 градусов
    public double ang2 = Math.PI / 6;  //Угол поворота на 30 градусов

    Vector3 placementPosition, scale;
    bool GetDominoTree(double xnew, double ynew, double angle, ref GameObject created_object)
    {
        //Проверка колизии
        GameObject new_object;
        placementPosition = new Vector3((float)xnew, 1f, (float)ynew);
        scale = new Vector3(1, dominoHeight, 0.2f);
        float degrees = 90 + (float)angle * Mathf.Rad2Deg; // Преобразование радиан в градусы
        Vector3 angles = new Vector3(0f, degrees, 0f);

        Vector3 check_location, check_scale;
        Quaternion rotation = Quaternion.Euler(angles);
        Collider[] colliders = Physics.OverlapBox(placementPosition, scale/2, rotation);

        //OnDrawGizmos();
        if (colliders.Length > 0)
        {
            return false;

            for (int i = 0; i < colliders.Length; i++)
            {

                float distance = Vector3.Distance(colliders[i].gameObject.transform.position, placementPosition);
                if (distance < 0.8)
                {
                    new_object = null;
                    return false;
                }

            }
            // Нет коллизий, размещаем объект
            
        }

        new_object = GetDomino();
        new_object.transform.position = placementPosition;

        //1f, dominoHeight, 0.2f;


        new_object.transform.eulerAngles = angles;
        //Приводим к радианам
        //new_object.transform.rotation = new Quaternion(0, degrees, 0, 1);//Quaternion.Euler(0, (float)angle, 0);
        //return new_object;
        Physics.Simulate(Time.fixedDeltaTime);
        created_object = new_object;
        return true;
    }
    public void StartFirstDomino()
    {
        var cur = start_object.transform.eulerAngles;
        cur.x = 45f;
        start_object.transform.eulerAngles = cur;
        var positon = start_object.transform.position;
        positon.z = -0.35f;
        positon.y = 1f;
        start_object.transform.position = positon;
    }
    GameObject start_object = null;

    public void InitDomino2()
    {
        Physics.autoSimulation = false;
        double xnew = 0, ynew = 0;
        
        if (GetDominoTree(xnew, ynew, angle, ref start_object))
        {
            DrawTree(xnew, ynew, 200, angle);
        }
        Physics.autoSimulation = true;
    }
    public int DrawTree(double x, double y, double a, double angle)
    {
        GameObject newObject = null; 
        //GameObject gameObject;
        if (a > 2)
        {
            a *= 0.7; //Меняем параметр a

            //Считаем координаты для вершины-ребенка
            double xnew = Math.Round(x + a * Math.Cos(angle)),
                   ynew = Math.Round(y - a * Math.Sin(angle));

            //рисуем линию между вершинами
            Vector3 oldPosition = new Vector3((float)x, 0, (float)y);
            Vector3 newPosition = new Vector3((float)xnew, 0, (float)ynew);
            double distance = Vector3.Distance(oldPosition,newPosition);
            if (distance > dominoHeight)
            {
                //Разбиваем на позиции
                int count = (int)(distance / dominoHeight)+1;
                Vector3 dir = newPosition - oldPosition;
                Vector3 s_dir = dir / count;
                Vector3 start_pos = oldPosition + s_dir;
                
                for (int i =0; i < count; ++i)
                {
                    if (!GetDominoTree(start_pos.x, start_pos.z, angle,ref  newObject))
                    {
                        return 0;

                    }

                    start_pos += s_dir;
                }
            }
            else
            {
                if (!GetDominoTree(xnew, ynew, angle, ref newObject))
                {
                    return 0;
                }
            }
            //Переприсваеваем координаты
            x = xnew;
            y = ynew;

            //Вызываем рекурсивную функцию для левого и правого ребенка
            DrawTree(x, y, a, angle + ang1);
            DrawTree(x, y, a, angle - ang2);
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
