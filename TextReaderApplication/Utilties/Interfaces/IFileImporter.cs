namespace TextReaderApplication {
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal interface IFileImporter {
        Task<List<string>> GetFileWords(string filePath);
        Task WriteWordsToFile(string filePath, List<string> wordList);

    }
}
