namespace MiniKpay.Database.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

    //    services.AddScoped<DepositWithdrawService>();
    //    services.AddScoped<WalletService>();
    //    services.AddScoped<TransactionService>();

        return services;
    }
}

