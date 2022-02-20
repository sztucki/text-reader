namespace TextReaderApplication {
    public interface IWordProcessor {
        Task<List<string>> GetValidWords(List<string> wordList, string startWord, string endWord);
    }
}
