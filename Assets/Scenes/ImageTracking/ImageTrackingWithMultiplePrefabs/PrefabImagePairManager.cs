using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Collections;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
    /// and overlays some prefabs on top of the detected image.
    /// </summary>
    [RequireComponent(typeof(ARTrackedImageManager))]
    
    public class PrefabImagePairManager : MonoBehaviour, ISerializationCallbackReceiver
    {
        /// <summary>
        /// Used to associate an `XRReferenceImage` with a Prefab by using the `XRReferenceImage`'s guid as a unique identifier for a particular reference image.
        /// </summary>
        /// 
        private GameObject m_CurrentlyInstantiatedPrefab;
        private Vector3 m_LastPrefabPosition;
        
        [Serializable]

        struct NamedPrefab
        {
            // System.Guid isn't serializable, so we store the Guid as a string. At runtime, this is converted back to a System.Guid
            public string imageGuid;
            public GameObject imagePrefab;

            public NamedPrefab(Guid guid, GameObject prefab)
            {
                imageGuid = guid.ToString();
                imagePrefab = prefab;
            }
        }

        [SerializeField]

        [HideInInspector]
        List<NamedPrefab> m_PrefabsList = new List<NamedPrefab>();

        Dictionary<Guid, GameObject> m_PrefabsDictionary = new Dictionary<Guid, GameObject>();
        Dictionary<Guid, GameObject> m_Instantiated = new Dictionary<Guid, GameObject>();
        ARTrackedImageManager m_TrackedImageManager;

        [SerializeField]
        [Tooltip("Reference Image Library")]
        XRReferenceImageLibrary m_ImageLibrary;

        [SerializeField]
        [Tooltip("Reference Image Library")]
        AudioSource introductionAudio;

        [SerializeField]
        GameObject Long_Short_Buttons;

        [SerializeField]
        AudioSource Explanation1;

        [SerializeField]
        AudioSource Explanation2;

        [SerializeField]
        GameObject Formula;

        [SerializeField]
        GameObject LongerPrefab;

        [SerializeField]
        AudioSource LongerExp;

        [SerializeField]
        AudioSource ShorterExp;

        [SerializeField]
        AudioSource LongerEnd;

        [SerializeField]
        AudioSource ShorterEnd;

        [SerializeField]
        printer prin;

        [SerializeField]
        GameObject LongerData;

        [SerializeField]
        GameObject ShorterData;

        [SerializeField]
        GameObject LengthSlider;

        [SerializeField]
        AudioSource Finale;

        [SerializeField]
        GameObject PrintMarker;


        /// <summary>
        /// Get the <c>XRReferenceImageLibrary</c>
        /// </summary>
        public XRReferenceImageLibrary imageLibrary
        {
            get => m_ImageLibrary;
            set => m_ImageLibrary = value;
        }


        public void OnBeforeSerialize()
        {
            m_PrefabsList.Clear();
            foreach (var kvp in m_PrefabsDictionary)
            {
                m_PrefabsList.Add(new NamedPrefab(kvp.Key, kvp.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            m_PrefabsDictionary = new Dictionary<Guid, GameObject>();
            foreach (var entry in m_PrefabsList)
            {
                m_PrefabsDictionary.Add(Guid.Parse(entry.imageGuid), entry.imagePrefab);
            }
        }

        void Awake()
        {
            m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        }

        void OnEnable()
        {
            m_TrackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
        }

        void OnDisable()
        {
            m_TrackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
        }

        void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
        {
            foreach (var trackedImage in eventArgs.added)
            {
                // Give the initial image a reasonable default scale
                var minLocalScalar = Mathf.Min(trackedImage.size.x, trackedImage.size.y) / 2;
                trackedImage.transform.localScale = new Vector3(minLocalScalar, minLocalScalar, minLocalScalar);
                AssignPrefab(trackedImage);
            }
        }

        void AssignPrefab(ARTrackedImage trackedImage)
        {
            if (m_PrefabsDictionary.TryGetValue(trackedImage.referenceImage.guid, out var prefab))
            {
                m_CurrentlyInstantiatedPrefab = Instantiate(prefab, trackedImage.transform);
                m_Instantiated[trackedImage.referenceImage.guid] = Instantiate(prefab, trackedImage.transform);
                Debug.Log($"Prefab instantiated for {trackedImage.referenceImage.name} with Movment script: {prefab.GetComponent<Movment>() != null}");
                PrintMarker.SetActive(false);
                introductionAudio.Play();
                StartCoroutine(WaitforIntro());
                ///Long_Short_Buttons.SetActive(true);


            }

        }
        IEnumerator WaitforIntro()
        {
            yield return new WaitForSeconds(introductionAudio.clip.length);
            Explanation1.Play();
            yield return new WaitForSeconds(Explanation1.clip.length);
            Formula.SetActive(true);
            yield return new WaitForSecondsRealtime(4);
            Formula.SetActive(false);
            Explanation2.Play();
            yield return new WaitForSeconds(Explanation2.clip.length);
            Long_Short_Buttons.SetActive(true);



        }
         
        public void Longer()
        {
            Long_Short_Buttons.SetActive(false );
            LongerData.SetActive(true);
            LongerExp.Play();
            StartCoroutine(WaitForLonger());

        }

        public void Shorter()
        {
            Long_Short_Buttons.SetActive(false ) ;
            ShorterData.SetActive(true);
            ShorterExp.Play();
            StartCoroutine(WaitForShorter());
        }
        IEnumerator WaitForLonger()
        {
            yield return new WaitForSeconds(LongerExp.clip.length);
            LongerData.SetActive(false);
            prin.EditChildInAllActivePrefabsShorter();
            ShorterData.SetActive(true );
            ShorterEnd.Play();
            yield return new WaitForSeconds(ShorterExp.clip.length);
            ShorterData.SetActive (false );
            Finale.Play();
            LengthSlider.SetActive(true );
            

        }
        IEnumerator WaitForShorter()
        {
            yield return new WaitForSeconds(ShorterExp.clip.length);
            ShorterData.SetActive(false);
            prin.EditChildInAllActivePrefabsLonger();
            LongerData.SetActive(true);
            LongerEnd.Play();
            yield return new WaitForSeconds(LongerExp.clip.length);
            LongerData.SetActive (false );
            Finale.Play();
            LengthSlider.SetActive(true);

        }

        public GameObject GetPrefabForReferenceImage(XRReferenceImage referenceImage)
            => m_PrefabsDictionary.TryGetValue(referenceImage.guid, out var prefab) ? prefab : null;

        public void SetPrefabForReferenceImage(XRReferenceImage referenceImage, GameObject alternativePrefab)
        {
            m_PrefabsDictionary[referenceImage.guid] = alternativePrefab;
            if (m_Instantiated.TryGetValue(referenceImage.guid, out var instantiatedPrefab))
            {
                m_Instantiated[referenceImage.guid] = Instantiate(alternativePrefab, instantiatedPrefab.transform.parent);
                Destroy(instantiatedPrefab);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// This customizes the inspector component and updates the prefab list when
        /// the reference image library is changed.
        /// </summary>
        [CustomEditor(typeof(PrefabImagePairManager))]
        class PrefabImagePairManagerInspector : Editor
        {
            List<XRReferenceImage> m_ReferenceImages = new List<XRReferenceImage>();
            bool m_IsExpanded = true;

            bool HasLibraryChanged(XRReferenceImageLibrary library)
            {
                if (library == null)
                    return m_ReferenceImages.Count == 0;

                if (m_ReferenceImages.Count != library.count)
                    return true;

                for (int i = 0; i < library.count; i++)
                {
                    if (m_ReferenceImages[i] != library[i])
                        return true;
                }

                return false;
            }

            public override void OnInspectorGUI()
            {
                //customized inspector
                var behaviour = serializedObject.targetObject as PrefabImagePairManager;

                serializedObject.Update();
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
                }

                var libraryProperty = serializedObject.FindProperty(nameof(m_ImageLibrary));
                EditorGUILayout.PropertyField(libraryProperty);
                var library = libraryProperty.objectReferenceValue as XRReferenceImageLibrary;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("introductionAudio"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Long_Short_Buttons"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Explanation1"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Explanation2"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("LongerExp"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ShorterExp"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Formula"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("LongerPrefab"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("prin"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("LongerEnd"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ShorterEnd"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("LongerData"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ShorterData"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("LengthSlider"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Finale"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PrintMarker"));



                //check library changes
                if (HasLibraryChanged(library))
                {
                    if (library)
                    {
                        var tempDictionary = new Dictionary<Guid, GameObject>();
                        foreach (var referenceImage in library)
                        {
                            tempDictionary.Add(referenceImage.guid, behaviour.GetPrefabForReferenceImage(referenceImage));
                        }
                        behaviour.m_PrefabsDictionary = tempDictionary;
                    }
                }

                // update current
                m_ReferenceImages.Clear();
                if (library)
                {
                    foreach (var referenceImage in library)
                    {
                        m_ReferenceImages.Add(referenceImage);
                    }
                }

                //show prefab list
                m_IsExpanded = EditorGUILayout.Foldout(m_IsExpanded, "Prefab List");
                if (m_IsExpanded)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUI.BeginChangeCheck();

                        var tempDictionary = new Dictionary<Guid, GameObject>();
                        foreach (var image in library)
                        {
                            var prefab = (GameObject)EditorGUILayout.ObjectField(image.name, behaviour.m_PrefabsDictionary[image.guid], typeof(GameObject), false);
                            tempDictionary.Add(image.guid, prefab);
                        }

                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(target, "Update Prefab");
                            behaviour.m_PrefabsDictionary = tempDictionary;
                            EditorUtility.SetDirty(target);
                        }
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}
