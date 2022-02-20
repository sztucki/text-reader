namespace TextReaderApplication {
    public interface IWordProcessor {
        List<string> GetValidWords(List<string> wordList, string startWord, string endWord);
    }
}
