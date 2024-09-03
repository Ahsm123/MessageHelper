using MessageHelper.Infrastructure;

namespace MessageHelper.Core;

public class MessageProcessor
{
    private readonly FileService _fileService;

    public MessageProcessor()
    {
        _fileService = new FileService();
    }

    public void PrintMessages(string folderPath)
    {
        var txtFiles = _fileService.GetFilesWithExtension(folderPath, ".txt");

        if (txtFiles == null)
        {
            Console.WriteLine("No messages");
            return;
        }

        foreach (var filePath in txtFiles)
        {

            string subject = _fileService.GetFileNameWithoutExtension(filePath.ToUpper());

            string content = _fileService.ReadFile(filePath);

            Console.WriteLine(subject);
            Console.WriteLine(content);

            _fileService.RenameFileExtension(filePath, ".backup");
        }
    }

    public void Cleanup(string folderPath)
    {
        _fileService.MoveFilesToBackupFolder(folderPath);
    }
    public void SortMessages(string folderPath)
    {
        _fileService.SortFilesByFirstLetter(folderPath);
    }
}
