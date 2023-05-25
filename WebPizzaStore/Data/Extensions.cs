namespace WebPizzaStore.Data;

public static class Extensions
{
    // The CreateDbIfNotExists method is defined as an extension of IHost.
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PizzaContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }
        }
    }
}