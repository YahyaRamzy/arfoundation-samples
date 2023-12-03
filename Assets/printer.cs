using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class printer : MonoBehaviour
{

    public Slider LengthValueSlider;
    public GameObject LV;
    public GameObject PeriodT;
    public GameObject MarkerDownloader;
    private float period;
    private float length;
    //public string imageUrl = "https://i.imgur.com/baBzVKt.png"; 

    void Start()
    {
        PrintAllActivePrefabs();
    }

    private void Update()
    {
        PrintAllActivePrefabs();
    }


    
    void PrintAllActivePrefabs()
    {
        // Find all GameObjects with the tag "Prefab" (replace it with your own tag)
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Prefab");

        // Iterate through each prefab and print its name
        foreach (GameObject prefab in prefabs)
        {
            // Check if the prefab is active in the scene
            if (prefab.activeInHierarchy)
            {
                Debug.Log("Active Prefab: " + prefab.name);

                // Find the "pivot" child
                Transform pivotChild = prefab.transform.Find("Pivot");

                // Check if the "pivot" child exists
                if (pivotChild != null)
                {
                    Debug.Log("Pivot Child found in Prefab: " + prefab.name);

                    // Iterate through each child of the "pivot" child and print its information
                    foreach (Transform child in pivotChild)
                    {
                        Debug.Log("Child Name: " + child.name);
                        Debug.Log("Child Size: " + child.localScale);
                        Debug.Log("Child Position (Y-axis): " + child.localPosition.y);

                        // If you want to print additional information about the child, you can add more Debug.Log statements here.
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab does not have a child named 'pivot': " + prefab.name);
                }
            }
        }
    }


    public void EditChildInAllActivePrefabsLonger()
    {
        // Find all GameObjects with the tag "Prefab" (replace it with your own tag)
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Prefab");

        // Iterate through each prefab
        foreach (GameObject prefab in prefabs)
        {
            // Check if the prefab is active in the scene
            if (prefab.activeInHierarchy)
            {
                Debug.Log("Editing Prefab: " + prefab.name);

                // Find the pivot child
                Transform pivotChild = prefab.transform.Find("Pivot");
                // Access the script attached to the "Pivot" child and adjust the speed
                Move2 moveScript = pivotChild.GetComponent<Move2>();
                if (moveScript != null)
                {
                    moveScript.speed = 1.2f ;  // Adjust the speed as needed
                }
                else
                {
                    Debug.LogWarning("Pivot child does not have a script named 'MoveScript': " + prefab.name);
                }
                // Check if the "Pivot" child exists
                if (pivotChild != null)
                {
                    // Find the child named "Cylinder" under the pivot
                    Transform cylinderChild = pivotChild.Find("Cylinder");

                    // Check if the "Cylinder" child exists
                    if (cylinderChild != null)
                    {
                        // Change the Y scale of the "Cylinder" child
                        Vector3 newCylinderScale = cylinderChild.localScale;
                        newCylinderScale.y = 7.5f;
                        cylinderChild.localScale = newCylinderScale;
                        Vector3 newCylinderPosition = cylinderChild.localPosition;
                        newCylinderPosition.y = -2.427f;
                        newCylinderPosition.y -= 5f;
                        cylinderChild.localPosition = newCylinderPosition;

                        Debug.Log("Edited Cylinder in Prefab: " + prefab.name);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Cylinder' under 'Pivot': " + prefab.name);
                    }

                    // Find the child named "Sphere" under the pivot
                    Transform sphereChild = pivotChild.Find("Sphere");

                    // Check if the "Sphere" child exists
                    if (sphereChild != null)
                    {
                        // Move the "Sphere" child down by 0.25f in the Y-axis
                        Vector3 newSpherePosition = sphereChild.localPosition;
                        newSpherePosition.y = -11.416f;
                        newSpherePosition.y -= 2.5f;
                        sphereChild.localPosition = newSpherePosition;

                        Debug.Log("Moved Sphere down in Prefab: " + prefab.name);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Sphere' under 'Pivot': " + prefab.name);
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab does not have a child named 'Pivot': " + prefab.name);
                }
            }
        }
    }

    public void EditChildInAllActivePrefabs0_5m()
    {
        // Find all GameObjects with the tag "Prefab" (replace it with your own tag)
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Prefab");

        // Iterate through each prefab
        foreach (GameObject prefab in prefabs)
        {
            // Check if the prefab is active in the scene
            if (prefab.activeInHierarchy)
            {
                Debug.Log("Editing Prefab: " + prefab.name);

                // Find the pivot child
                Transform pivotChild = prefab.transform.Find("Pivot");

                // Check if the "Pivot" child exists
                if (pivotChild != null)
                {
                    // Find the child named "Cylinder" under the pivot
                    Transform cylinderChild = pivotChild.Find("Cylinder");
                    Move2 moveScript = pivotChild.GetComponent<Move2>();
                    if (moveScript != null)
                    {
                        moveScript.speed = 1.5f;  // Adjust the speed as needed
                    }
                    else
                    {
                        Debug.LogWarning("Pivot child does not have a script named 'MoveScript': " + prefab.name);
                    }
                    // Check if the "Cylinder" child exists
                    if (cylinderChild != null)
                    {
                        // Change the Y scale of the "Cylinder" child
                        Vector3 newCylinderScale = cylinderChild.localScale;
                        newCylinderScale.y = 5f;
                        cylinderChild.localScale = newCylinderScale;
                        Vector3 newCylinderPosition = cylinderChild.localPosition;
                        newCylinderPosition.y = -2.427f;
                        newCylinderPosition.y -= 2.5f;
                        cylinderChild.localPosition = newCylinderPosition;

                        Debug.Log("Edited Cylinder in Prefab: " + prefab.name);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Cylinder' under 'Pivot': " + prefab.name);
                    }

                    // Find the child named "Sphere" under the pivot
                    Transform sphereChild = pivotChild.Find("Sphere");

                    // Check if the "Sphere" child exists
                    if (sphereChild != null)
                    {
                        // Move the "Sphere" child down by 0.25f in the Y-axis
                        Vector3 newSpherePosition = sphereChild.localPosition;
                        newSpherePosition.y = -11.416f;
                        
                        sphereChild.localPosition = newSpherePosition;

                        Debug.Log("Moved Sphere down in Prefab: " + prefab.name);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Sphere' under 'Pivot': " + prefab.name);
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab does not have a child named 'Pivot': " + prefab.name);
                }
            }
        }
    }


    public void EditChildInAllActivePrefabsLonger1m()
    {
        // Find all GameObjects with the tag "Prefab" (replace it with your own tag)
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Prefab");

        // Iterate through each prefab
        foreach (GameObject prefab in prefabs)
        {
            // Check if the prefab is active in the scene
            if (prefab.activeInHierarchy)
            {
                Debug.Log("Editing Prefab: " + prefab.name);

                // Find the pivot child
                Transform pivotChild = prefab.transform.Find("Pivot");

                // Check if the "Pivot" child exists
                if (pivotChild != null)
                {
                    // Find the child named "Cylinder" under the pivot
                    Transform cylinderChild = pivotChild.Find("Cylinder");
                    Move2 moveScript = pivotChild.GetComponent<Move2>();
                    if (moveScript != null)
                    {
                        moveScript.speed = 0.9f;  // Adjust the speed as needed
                    }
                    else
                    {
                        Debug.LogWarning("Pivot child does not have a script named 'MoveScript': " + prefab.name);
                    }
                    // Check if the "Cylinder" child exists
                    if (cylinderChild != null)
                    {
                        // Change the Y scale of the "Cylinder" child
                        Vector3 newCylinderScale = cylinderChild.localScale;
                        newCylinderScale.y = 10f;
                        cylinderChild.localScale = newCylinderScale;
                        Vector3 newCylinderPosition = cylinderChild.localPosition;
                        newCylinderPosition.y = -2.427f;
                        newCylinderPosition.y -= 7.5f;
                        cylinderChild.localPosition = newCylinderPosition;

                        Debug.Log("Edited Cylinder in Prefab: " + prefab.name);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Cylinder' under 'Pivot': " + prefab.name);
                    }

                    // Find the child named "Sphere" under the pivot
                    Transform sphereChild = pivotChild.Find("Sphere");

                    // Check if the "Sphere" child exists
                    if (sphereChild != null)
                    {
                        // Move the "Sphere" child down by 0.25f in the Y-axis
                        Vector3 newSpherePosition = sphereChild.localPosition;
                        newSpherePosition.y = -11.416f;
                        newSpherePosition.y -= 7.5f;
                        sphereChild.localPosition = newSpherePosition;

                        Debug.Log("Moved Sphere down in Prefab: " + prefab.name);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Sphere' under 'Pivot': " + prefab.name);
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab does not have a child named 'Pivot': " + prefab.name);
                }
            }
        }
    }


    public void EditChildInAllActivePrefabsShorter()
    {
        // Find all GameObjects with the tag "Prefab" (replace it with your own tag)
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Prefab");

        // Iterate through each prefab
        foreach (GameObject prefab in prefabs)
        {
            // Check if the prefab is active in the scene
            if (prefab.activeInHierarchy)
            {
                Debug.Log("Editing Prefab: " + prefab.name);

                // Find the pivot child
                Transform pivotChild = prefab.transform.Find("Pivot");

                // Check if the "Pivot" child exists
                if (pivotChild != null)
                {
                    // Find the child named "Cylinder" under the pivot
                    Transform cylinderChild = pivotChild.Find("Cylinder");
                    Move2 moveScript = pivotChild.GetComponent<Move2>();
                    if (moveScript != null)
                    {
                        moveScript.speed = 1.85f;  // Adjust the speed as needed
                    }
                    else
                    {
                        Debug.LogWarning("Pivot child does not have a script named 'MoveScript': " + prefab.name);
                    }
                    // Check if the "Cylinder" child exists
                    if (cylinderChild != null)
                    {
                        // Change the Y scale of the "Cylinder" child
                        Vector3 newCylinderScale = cylinderChild.localScale;

                        newCylinderScale.y = 2.5f;
                        cylinderChild.localScale = newCylinderScale;



                        Vector3 newCylinderPosition = cylinderChild.localPosition;
                        newCylinderPosition.y = -2.427f;
                        ////newCylinderPosition.y -= 2.5f;
                        cylinderChild.localPosition = newCylinderPosition;

                        Debug.Log("Edited Cylinder in Prefab: " + prefab.name);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Cylinder' under 'Pivot': " + prefab.name);
                    }

                    // Find the child named "Sphere" under the pivot
                    Transform sphereChild = pivotChild.Find("Sphere");

                    // Check if the "Sphere" child exists
                    if (sphereChild != null)
                    {
                        // Move the "Sphere" child down by 0.25f in the Y-axis
                        Vector3 newSpherePosition = sphereChild.localPosition;
                        newSpherePosition.y = -11.416f;
                        newSpherePosition.y += 5f;
                        sphereChild.localPosition = newSpherePosition;

                        Debug.Log("Moved Sphere down in Prefab: " + prefab.name);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Sphere' under 'Pivot': " + prefab.name);
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab does not have a child named 'Pivot': " + prefab.name);
                }
            }
        }
    }



    public void PendulumChanger()
    {
        // Find all GameObjects with the tag "Prefab" (replace it with your own tag)
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Prefab");

        // Iterate through each prefab
        foreach (GameObject prefab in prefabs)
        {
            // Check if the prefab is active in the scene
            if (prefab.activeInHierarchy)
            {
                Debug.Log("Editing Prefab: " + prefab.name);

                // Find the pivot child
                Transform pivotChild = prefab.transform.Find("Pivot");

                // Check if the "Pivot" child exists
                if (pivotChild != null)
                {
                    // Find the child named "Cylinder" under the pivot
                    Transform cylinderChild = pivotChild.Find("Cylinder");

                    // Check if the "Cylinder" child exists
                    if (cylinderChild != null)
                    {

                        if (LengthValueSlider.value >= 50)
                        {
                            // Change the Y scale of the "Cylinder" child
                            Vector3 newCylinderScale = cylinderChild.localScale;
                            newCylinderScale.y = LengthValueSlider.value/10;
                            cylinderChild.localScale = newCylinderScale;



                            Vector3 newCylinderPosition = cylinderChild.localPosition;
                            newCylinderPosition.y += 5f - (LengthValueSlider.value/10);
                            cylinderChild.localPosition = newCylinderPosition;

                            Debug.Log("Edited Cylinder in Prefab: " + prefab.name);
                        }
                        else
                        {
                            // Change the Y scale of the "Cylinder" child
                            Vector3 newCylinderScale = cylinderChild.localScale;
                            newCylinderScale.y = LengthValueSlider.value / 10;
                            cylinderChild.localScale = newCylinderScale;



                            Vector3 newCylinderPosition = cylinderChild.localPosition;
                            newCylinderPosition.y += (5f - (LengthValueSlider.value / 10));
                            cylinderChild.localPosition = newCylinderPosition;

                            Debug.Log("Edited Cylinder in Prefab: " + prefab.name);
                        }

                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Cylinder' under 'Pivot': " + prefab.name);
                    }

                    // Find the child named "Sphere" under the pivot
                    Transform sphereChild = pivotChild.Find("Sphere");

                    // Check if the "Sphere" child exists
                    if (sphereChild != null)
                    {

                        if (LengthValueSlider.value >= 50)
                        {
                            // Move the "Sphere" child down by 0.25f in the Y-axis
                            Vector3 newSpherePosition = sphereChild.localPosition;
                            newSpherePosition.y += 5f - (LengthValueSlider.value/10);
                            sphereChild.localPosition = newSpherePosition;

                            Debug.Log("Moved Sphere down in Prefab: " + prefab.name);
                        }
                        else
                        {
                            // Move the "Sphere" child down by 0.25f in the Y-axis
                            Vector3 newSpherePosition = sphereChild.localPosition;
                            newSpherePosition.y += (5f - (LengthValueSlider.value / 10))*2;
                            sphereChild.localPosition = newSpherePosition;

                            Debug.Log("Moved Sphere down in Prefab: " + prefab.name);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Prefab does not have a child named 'Sphere' under 'Pivot': " + prefab.name);
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab does not have a child named 'Pivot': " + prefab.name);
                }
            }
        }
    }



    public void sliderchanger()
    {
        TextMeshProUGUI textMesh = LV.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMesh2 = PeriodT.GetComponent<TextMeshProUGUI>();
        if (LengthValueSlider.value >= 25 && LengthValueSlider.value < 43)
        {
            EditChildInAllActivePrefabsShorter();
            textMesh.text = "L = 25 cm";
            textMesh2.text = "Period = 1 seconds";

        }
        else if(LengthValueSlider.value >= 43 && LengthValueSlider.value <62)
        {
            EditChildInAllActivePrefabs0_5m();
            textMesh.text = "L = 50 cm";
            textMesh2.text = "Period = 1.42 seconds";
        }
        else if(LengthValueSlider.value >=62 && LengthValueSlider.value < 81)
        {
            EditChildInAllActivePrefabsLonger();
            textMesh.text = "L = 75 cm";
            textMesh2.text = "Period = 1.74 seconds";
        }
        else if(LengthValueSlider.value >=81 && LengthValueSlider.value <= 100)
        {
            EditChildInAllActivePrefabsLonger1m();
            textMesh.text = "L = 100 cm";
            textMesh2.text = "Period = 2 seconds";
        }
    }

    public void TextUpdate()
    {
        TextMeshProUGUI textMesh = LV.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMesh2 = PeriodT.GetComponent<TextMeshProUGUI>();
        //length = LengthValueSlider.value / 100;
        period = 2 * Mathf.PI * Mathf.Sqrt(LengthValueSlider.value / 980);
        textMesh.text = "L = " + LengthValueSlider.value + " cm";
        textMesh2.text = "Period = " + RoundToDecimalPlaces(period, 2) + " seconds";

    }
    float RoundToDecimalPlaces(float value, int decimalPlaces)
    {
        float multiplier = Mathf.Pow(10f, decimalPlaces);
        return Mathf.Round(value * multiplier) / multiplier;
    }


    public void printMarker()
    {
        OpenTheURL();
        MarkerDownloader.SetActive(false);

    }
    public string urlToOpen = "https://imgur.com/a/FsmtOcK";

    // Call this function to open the URL
    public void OpenTheURL()
    {
        Application.OpenURL(urlToOpen);
    }

    public void quitpressed()
    {
        Application.Quit();
    }

    
}
