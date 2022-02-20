using System.Reflection;
using TextReaderApplication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FluentValidation;

//option one has the weird logging but has the weird logging

await Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Warning))
    .ConfigureServices((hostContext, services) => {
        services
            .AddSingleton<IFileImporter, FileImporter>()
            .AddSingleton<IWordProcessor, WordProcessor>()
            .AddHostedService<ConsoleHostedService>()
            .AddValidatorsFromAssemblyContaining<FileValidator>();


    })
    .RunConsoleAsync();