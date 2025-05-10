namespace Sample7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                Console.WriteLine("Не задан файл для преобразования");
                return;
            }
            string inputFileName = args[0];
            if (!Path.Exists(inputFileName)) {
                Console.WriteLine(@"Файл {inputFileName} не найден");
                return;
            }
            string documentNumber;
            if (args.Length > 1) 
            {
                documentNumber = args[1];
            }
            else
            {
                documentNumber = "Unknown";
            }
            // Ну поехали
            byte[] inputFileBytes = File.ReadAllBytes(inputFileName);
            PrintService.ProcessInputFile(inputFileBytes, inputFileName, documentNumber, DateTime.Now, false);
            return;
        }
    }
}
