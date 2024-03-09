using Microsoft.Extensions.DependencyInjection;
using PowerDiary.ChatRoom.Application.Repositories;
using PowerDiary.ChatRoom.Application.Repositories.Interfaces;
using PowerDiary.ChatRoom.Application.Services;
using PowerDiary.ChatRoom.Application.Services.Interfaces;

namespace PowerDiary.ChatRoom
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IOutputService, OutputService>();
            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IChatRoomRepository, ChatRoomRepository>();
            return services;
        }
    }
}
