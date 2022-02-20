using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReaderApplication {
    internal class FileImporter : IFileImporter {
       

  
        public async Task<List<string>> GetFileWords(string filePath) {
            List<string> lines = await Task.Run(() => File.ReadAllLines(filePath).ToList());
           return lines;
        }

        public async Task WriteWordsToFile(string filePath, List<string> wordList) {
            await File.WriteAllLinesAsync("filePath", wordList);
        }

 


 
    }
}
