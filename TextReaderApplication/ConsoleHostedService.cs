using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FluentValidation.Results;
using FluentValidation;

namespace TextReaderApplication;

internal sealed class ConsoleHostedService : IHostedService {
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IFileImporter _fileImporter;
    private readonly IWordProcessor _wordProcessor;
    private Task? _applicationTask;
    private int? _exitCode;
    private readonly IValidator<string> _fileValidator;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IFileImporter fileImporter,
        IWordProcessor wordProcessor,
        IValidator<string> fileValidator
        ) {
        _logger = logger;
        _appLifetime = appLifetime;
        _fileImporter = fileImporter;
        _wordProcessor = wordProcessor;
        _fileValidator = fileValidator;
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

        CancellationTokenSource? _cancellationTokenSource = null;

        _appLifetime.ApplicationStarted.Register(() => {
            _logger.LogDebug("Application has started");
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _applicationTask = Task.Run(async () => {
                try {
                    Console.WriteLine("Welcome to the test reader Application!");
                    Console.WriteLine("Please enter a valid file for import:");
                    string importFilePath = "C:\\Users\\Ben Sztucki\\Desktop\\words-english.txt";

                    bool validImportFilePath = true;
                    while (validImportFilePath == false) {
                        ValidationResult result = _fileValidator.Validate(importFilePath);
                        if (result.IsValid) {
                            validImportFilePath = true;
                        }
                        else {
                            foreach (ValidationFailure error in result.Errors) {
                                Console.WriteLine(error);
                            }
                            importFilePath = Console.ReadLine() ?? "";
                        }
                    }

                    List<string> wordList = await _fileImporter.GetFileWords(importFilePath);


                    //filter out all of the words that dont have 4 characters 
                    wordList = wordList.Where(s => s.Length == 4).ToList();
                    wordList.Sort();

                    Console.WriteLine("Please enter a start word:");
                    string startWord =  "bend";

                    Console.WriteLine("Please enter a end word:");
                    string endWord = "prod";


                    Console.WriteLine("please enter a valid file path for an existing .txt file for output");
                    string outputFilePath = "C:\\Users\\Ben Sztucki\\Desktop\\test.txt";

                    bool validOutputFilePath = true;
                    while (validOutputFilePath == false) {
                        ValidationResult result = _fileValidator.Validate(outputFilePath);
                        if (result.IsValid) {
                            validOutputFilePath = true;
                        }
                        else {
                            foreach (ValidationFailure error in result.Errors) {
                                Console.WriteLine(error);
                            }
                            outputFilePath = Console.ReadLine() ?? "";
                        }
                    }


                    List<string> validWords = _wordProcessor.GetValidWords(wordList, startWord, endWord);

                    List<string> wordsToOutput = _wordProcessor.ProcessWords(validWords, startWord, endWord);

                   await _fileImporter.WriteWordsToFile(outputFilePath, wordsToOutput);
                }
                catch (TaskCanceledException) {
                    // This means the application is shutting down, so just swallow this exception
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "Unhandled exception!");
                    _exitCode = 1;
                }
                finally {
                    // Stop the application once the work is done
                    _appLifetime.StopApplication();
                }
            });
        });

        _appLifetime.ApplicationStopping.Register(() => {
            _logger.LogDebug("Application is stopping");
            _cancellationTokenSource?.Cancel();
        });

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken) {
        // Wait for the application logic to fully complete any cleanup tasks.
        // Note that this relies on the cancellation token to be properly used in the application.
        if (_applicationTask != null) {
            await _applicationTask;
        }

        _logger.LogDebug($"Exiting with return code: {_exitCode}");

        // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
    }
}