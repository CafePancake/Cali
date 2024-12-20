using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(menuName = "New Sauvegarde File", fileName = "Sauvegarde")]
public class SOSauvegarde : ScriptableObject
{
    [SerializeField] string _fichier = "saveData.cali";
    [SerializeField] SOData _data;

    // [DllImport("__Internal")]
    // private static extern void SynchroniserWebGL();


    public void LireFichier()
    {
        string _fichierEtChemin = Application.persistentDataPath + "/" + _fichier;

        if (File.Exists(_fichierEtChemin)) 
        {
            string contenu = File.ReadAllText(_fichierEtChemin);
            JsonUtility.FromJsonOverwrite(contenu, _data);

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(_data);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

        else{
            Debug.LogWarning("Le fichier n'existe pas");
            Debug.Log(_fichierEtChemin);
            EcrireFichier();
            Debug.Log("Fichier Ecrit");
        } 
        
    }


    public void EcrireFichier()
    {
        string _fichierEtChemin = Application.persistentDataPath + "/" + _fichier;
        string contenu = JsonUtility.ToJson(_data);
        File.WriteAllText(_fichierEtChemin, contenu);

        // if (Application.platform == RuntimePlatform.WebGLPlayer) //sauvegarde webgl si sur webgl
        // {
        //     // SynchroniserWebGL();
        // }
    }
}