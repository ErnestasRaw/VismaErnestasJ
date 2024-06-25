using Microsoft.Extensions.DependencyInjection;
using VismaInternshipErnestasJ.Commands;
using VismaInternshipErnestasJ.Data;
using VismaInternshipErnestasJ.Middleware;
using VismaInternshipErnestasJ.Models;
using VismaInternshipErnestasJ.Services;
using VismaInternshipErnestasJ.Services.VismaInternshipErnestasJ.Services;

namespace VismaInternshipErnestasJ
{
    class Program
    {
        static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDataStorage, JsonDataStorage>()
                .AddSingleton<IShortageService, ShortageService>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<UserContext>()
                .BuildServiceProvider();

            var userContext = serviceProvider.GetRequiredService<UserContext>();
            var userService = serviceProvider.GetRequiredService<IUserService>();


            var middleware = new AuthenticationMiddleware(async () =>
            {
                var command = SelectCommand(serviceProvider, userContext);
                command.Execute();
            }, userService);
            while (userContext.ExitRequested == false) await middleware.Invoke(userContext);
        }

        private static ICommand SelectCommand(IServiceProvider serviceProvider, UserContext userContext)
        {
            Console.WriteLine("Select command:\n1. Register Shortage\n2. Delete Shortage\n3. List Shortages\n4. Filter Shortages\n5. Exit");
            var commandOption = Console.ReadLine();

            return commandOption switch
            {
                "1" => new RegisterShortageCommand(serviceProvider.GetRequiredService<IShortageService>(), userContext),
                "2" => new DeleteShortageCommand(serviceProvider.GetRequiredService<IShortageService>()),
                "3" => new ListShortagesCommand(serviceProvider.GetRequiredService<IShortageService>()),
                "4" => new FilterShortagesCommand(serviceProvider.GetRequiredService<IShortageService>()),
                "5" => new ExitCommand(userContext),
                _ => throw new InvalidOperationException("Invalid command.")
            };
        }
    }
}
