using UnityEngine;
using System;
using System.IO;

public class SaveFileDataWriter
{
    public string saveDataDirectoryPath = "";
    public string saveFileName = "";

    // BEFORE WE CREATE A NEW SAVE FILE, WE MUST CHECK TO SEE IF ONE OF THIS CHARACTER SLOT ALREADY EXISTS (MAX 10 CHARACTER SLOTS)
    public bool CheckToSeeIfFileExists()
    {
        return File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName));
    }

    // USED TO DELETE CHARACTER SLOTS
    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
    }

    public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
    {
        // MAKE A PATH TO SAVE THE FILE 
        string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

        try
        {
            // CREATE THE DIRECTORY THE FILE WILL BE WRITTEN TO, IF IT DOES NOT EXISTS
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log($"CREATING SAVE FILE, AT SAVE PATH: {savePath}");

            // SERIALIZE THE C# GAME DATA OBJECT INTO JSON
            string dataToStore = JsonUtility.ToJson(characterData, true);

            // WRITE THAT FILE TO OUR SYSTEM
            using (FileStream steam = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter fileWriter = new StreamWriter(steam))
                {
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("ERROR WHILE TRYING TO SAVE CHARACTER DATA, GAME NOT SAVED" + savePath + "\n" + ex);
        }
    }

    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterData = null;

        // MAKE A PATH TO LOAD THE FILE
        string LoadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

        if (File.Exists(LoadPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(LoadPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // DESERIALIZE THE DATA FROM JSON BACK TO UNITY
                characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);

            }
            catch (Exception ex)
            {
                Debug.Log("ERROR WHILE TRYING TO LOAD CHARACTER DATA, GAME NOT SAVED\n" + ex);
            }
        }
        return characterData;

    }
}
