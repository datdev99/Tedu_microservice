﻿using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging;

public static class SeriLogger
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
        (context, configuration) =>
        {
            var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".","-");
            var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

            configuration
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss}  {Level}] {SourceContext}{NewLine}{Message: lj}{NewLine}{Exception}{NewLine}]")
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", environmentName)
                .Enrich.WithProperty("ApplicationName", applicationName)
                .ReadFrom.Configuration(context.Configuration);
        };
}