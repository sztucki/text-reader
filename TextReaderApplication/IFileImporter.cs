using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReaderApplication {
    internal interface IFileImporter {
        Task<List<string>> GetFileWords(string filePath);
        Task WriteWordsToFile(string filePath, List<string> wordList);

    }
}
