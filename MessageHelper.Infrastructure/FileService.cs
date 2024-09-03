using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace MessageHelper.Infrastructure;

public class FileService
{

    //Henter alle filer med en specifik extenion fra en mappe fx ".txt".
    public string[] GetFilesWithExtension(string folderPath, string extension)
    {
        return Directory.GetFiles(folderPath, $"*{extension}");
    }

    //Henter alle filnavne uden extension
    public string GetFileNameWithoutExtension(string filePath)
    {

        return Path.GetFileNameWithoutExtension(filePath);
    }

    //Læser indholdet af en fil
    public string ReadFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    //Ændre navnet på filens extension fx fra ".txt" til ".backup"
    public void RenameFileExtension(string filePath, string newExtension)
    {
        string newFilePath = Path.ChangeExtension(filePath, newExtension);
        File.Move(filePath, newFilePath);

    }

    //Flytter alle filer med .backup extension til en backup mappe
    public void MoveFilesToBackupFolder(string folderPath)
    {
        //Definer stigen til backup undermappe
        string backupFolder = Path.Combine(folderPath, "_backup");

        //Sikrer at mappen eksiterer eller laver en.
        Directory.CreateDirectory(backupFolder);

        //Få alle .backup filer fra root mappen
        var backupFiles = GetFilesWithExtension(folderPath, ".backup");


        foreach (var file in backupFiles)
        {

            string fileName = Path.GetFileName(file);

            //Definerer stigens til _backup mappen.
            string destinationPath = Path.Combine(backupFolder, fileName);

            File.Move(file, destinationPath);
        }
    }
    // Sorterer .txt filerne ind i mapper baseret på det første bogstav i filnavnet
    public void SortFilesByFirstLetter(string folderPath)
    {
        string sortedFolder = Path.Combine(folderPath, "sorted");

        Directory.CreateDirectory(sortedFolder);

        foreach (char letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ")
        {
            Directory.CreateDirectory(Path.Combine(sortedFolder, letter.ToString()));
        }

        var txtFiles = GetFilesWithExtension(folderPath, ".txt");

        foreach (var file in txtFiles)
        {
            string fileName = GetFileNameWithoutExtension(file);
            char firstLetter = char.ToUpper(fileName[0]);

            if (char.IsLetter(firstLetter))
            {
                string destinationFolder = Path.Combine(sortedFolder, firstLetter.ToString());

                string destinationPath = Path.Combine(destinationFolder, Path.GetFileName(file));

                File.Move(file, destinationPath);
            }
        }
    }
}

