namespace TextReaderApplication {
    internal class FileImporter : IFileImporter {


        /// <summary>
        /// Gets all of the words that are in the file and adds them to a list
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<List<string>> GetFileWords(string filePath) {
            List<string> lines = await Task.Run(() => File.ReadAllLines(filePath).ToList());
            return lines;
        }

        /// <summary>
        /// Writes words to specified txt file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="wordList"></param>
        /// <returns></returns>
        public async Task WriteWordsToFile(string filePath, List<string> wordList) {
            await File.WriteAllLinesAsync(filePath, wordList);
        }





    }
}
