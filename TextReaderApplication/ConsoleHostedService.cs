﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TextReaderApplication;

internal sealed class ConsoleHostedService : IHostedService {
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IFileImporter _fileImporter;

    private Task? _applicationTask;
    private int? _exitCode;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IFileImporter fileImporter
        ) {
        _logger = logger;
        _appLifetime = appLifetime;
        _fileImporter = fileImporter;
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
                    _fileImporter.ImportFile();


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