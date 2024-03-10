# Power Diary Developer challenge

### Description
A small console app that simulates a chat room and displays events that happened based on time aggregations. Within, simple implementations of DDD and CQRS can be found. 

Events types:
  - enter-the-room
  -leave-the-room
  - comment
  - high-five-another-use
    
### Requirments

.NET 8.

### Installation instructions

Clone the repository and run the code from your VS or other IDE tools. 
Make sure that all nuget packages have been installed, and that the code can be built.

### Project structure

- PowerDiary.ChatRoom.Application: Behavioural/Business layer.
- PowerDiary.ChatRoom.Domain: Core domain where our chat room is modelled, along with its entities, value objects and so on.
- PowerDiary.ChatRoom.Tests.ArchitectureTests: Architecture enforcing tests.
- PowerDiary.ChatRoom.Tests.UnitTests: Unit tests.

### Project status

Development.

### Contribution guidelines

Please squash your commits when creating a PR.

### Contributors

Written and maintained by Selim Huskic: selimhuskic@gmail.com
