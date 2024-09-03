using MessageHelper.Core;

namespace MessageHelper.ConsoleApp
{
    internal class Program
    {
     
        static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                Console.WriteLine("Invalid arguments. Usage: MessageHelper.exe [messagefolder path] [operation]");
                return; 
            }

            // Assign the first argument as the folder path
            string messageFolderPath = args[0];

            // Assign the second argument as the operation, defaulting to "printmessages" if not provided
            string operation = args.Length > 1 ? args[1] : "printmessages";

            // Instantiate the MessageProcessor from the Core project
            var messageProcessor = new MessageProcessor();

            // Determine which operation to perform based on the second argument
            switch (operation.ToLower())  // Convert the operation to lowercase for case-insensitive comparison
            {
                case "printmessages":
                    messageProcessor.PrintMessages(messageFolderPath);  // Call the method to print messages
                    break;
                case "cleanup":
                    messageProcessor.Cleanup(messageFolderPath);  // Call the method to clean up files
                    break;
                case "sort":
                    messageProcessor.SortMessages(messageFolderPath);  // Call the method to sort messages
                    break;
                default:
                    Console.WriteLine("Unknown operation. Please use 'printmessages', 'cleanup', or 'sort'.");
                    break;  // Handle any unknown operations with an error message
            }
        }
    }
}
