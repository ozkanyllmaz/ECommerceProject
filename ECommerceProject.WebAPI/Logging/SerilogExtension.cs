using ECommerceProject.Application.Features.Auth.Commands.Login;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;

namespace ECommerceProject.WebAPI.Logging
{
    public static class SerilogExtension
    {
        public static void AddSerilogExtension(this IServiceCollection services, string connectionString)
        {
            var columnOptions = new ColumnOptions();


            columnOptions.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                {
                    ColumnName = "User",
                    DataType = System.Data.SqlDbType.NVarChar,
                    DataLength = -1,
                    AllowNull = true,
                }
            };

            Log.Logger = new LoggerConfiguration()
                .Destructure.ByTransforming<LoginCommandRequest>(r => new
                {
                    Email = r.Email,
                    Password = "****"
                })
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("LuckyPennySoftware", Serilog.Events.LogEventLevel.Error)
                .Enrich.FromLogContext()

                .WriteTo.Logger(requestlog => requestlog
                    .Filter.ByIncludingOnly(Matching.FromSource("ECommerceProject.Application.Behaviors.LoggingBehavior"))
                    .WriteTo.MSSqlServer(
                        connectionString: connectionString,
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "RequestLogs",
                            AutoCreateSqlTable = true
                        },
                        columnOptions: columnOptions))

                .WriteTo.Logger(exceptionlog => exceptionlog
                    .Filter.ByIncludingOnly(e => e.Exception != null)
                    .WriteTo.MSSqlServer(
                        connectionString: connectionString,
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "ExceptionLogs",
                            AutoCreateSqlTable = true,
                        },
                        columnOptions: columnOptions))
                .CreateLogger();

            services.AddLogging(logginghbuilder => logginghbuilder.AddSerilog(dispose: true));
        }
    }
}
