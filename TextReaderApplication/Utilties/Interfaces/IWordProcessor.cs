namespace TextReaderApplication {
    public interface IWordProcessor {
        List<string> GetValidWords(List<string> wordList, string startWord, string endWord);
        List<string> ProcessWords(List<string> wordsInRange, string startWord, string endWord);
    }
}
