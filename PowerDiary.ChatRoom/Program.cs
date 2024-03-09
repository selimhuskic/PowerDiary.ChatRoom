// See https://aka.ms/new-console-template for more information
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PowerDiary.ChatRoom;
using PowerDiary.ChatRoom.Application.Commands;
using PowerDiary.ChatRoom.Infrastructure.Assembly;
using PowerDiary.ChatRoom.Infrastructure.Commands;
using PowerDiary.ChatRoom.Infrastructure.Queries;

Console.WriteLine("Power Diary Chat Room!");

var serviceCollection = new ServiceCollection();

serviceCollection
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly))
    .ConfigureRepositories()
    .ConfigureServices();

using var serviceProvider = serviceCollection.BuildServiceProvider();

var mediator = serviceProvider.GetService<IMediator>();

var now = DateTime.UtcNow;

var chatRoomId = await mediator!.Send(new CreateChatRoomCommand());
await mediator.Send(new AddParticipantToChatRoomCommand(chatRoomId, "Bob", now.AddMinutes(-70)));
await mediator.Send(new AddParticipantToChatRoomCommand(chatRoomId, "Kate", now.AddMinutes(-65)));
await mediator.Send(new AddCommentCommand(chatRoomId, "Bob", "Hey, Kate - high five?", now.AddMinutes(-15)));
await mediator.Send(new AddReactionCommand(chatRoomId, "Kate", "Bob", 10, now.AddMinutes(-13)));
await mediator.Send(new RemoveParticipantFromChatRoomCommand(chatRoomId, "Bob", now.AddMinutes(-10)));
await mediator.Send(new AddCommentCommand(chatRoomId, "Kate", "Oh, typical", now.AddMinutes(-7)));
await mediator.Send(new RemoveParticipantFromChatRoomCommand(chatRoomId, "Kate", now.AddMinutes(-5)));

var chatPerMinute = await mediator.Send(new GetChatRoomEventsQuery(chatRoomId, 10));

Console.WriteLine();
Console.WriteLine("======== Events per minute =======");
Console.WriteLine();
chatPerMinute.Records.ForEach(Console.WriteLine);

var chatPerHour = await mediator.Send(new GetChatRoomEventsQuery(chatRoomId, 20));

Console.WriteLine();
Console.WriteLine("======== Events per hour =======");
chatPerHour.Records.ForEach(Console.WriteLine);
