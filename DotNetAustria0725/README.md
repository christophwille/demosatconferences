# Database Updates - Not As Easy As You Think

Event homepage: https://www.meetup.com/dotnet-austria/events/308632588/

Use EF migrations and you are all set, they said. Not so fast! Are your changes compatible with already existing data? Are your changes backward compatible? What about DDL vs DML permissions? And then the elephant in the room: is EF migrations actually the best choice, or is a model-based approach even better? Let's find out what the "It depends" reasons are.

Samples https://github.com/christophwille/poc-oh/tree/main/src/DbPlayground