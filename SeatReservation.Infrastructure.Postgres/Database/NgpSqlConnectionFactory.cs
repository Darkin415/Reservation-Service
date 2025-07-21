using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace SeatReservation.Infrastructure.Postgres.Repositories;

public class NgpSqlConnectionFactory : IDisposable
{
    private readonly NpgsqlDataSource _dataSource;

    public NgpSqlConnectionFactory(IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("ReservationServiceDb"));
        dataSourceBuilder
            .UseLoggerFactory(CreateLoggerFactory());
        
        _dataSource = dataSourceBuilder.Build();
    }
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        return await _dataSource.OpenConnectionAsync();
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });

    public void Dispose()
    {
        _dataSource.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dataSource.DisposeAsync();
    }
}