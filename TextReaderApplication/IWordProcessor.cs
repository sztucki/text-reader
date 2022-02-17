using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReaderApplication {
    public interface IWordProcessor {
        Task<List<string>> GetValidWords(List<string> wordList, string startWord, string endWord);
    }
}
