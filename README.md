# Blues Clues

## Team:
Adrian Fearon
David Brennan
Stephen Mills
William McLaughlin

## Requirements:
- 30% CA – Group Project
- Demonstration Week 8
- You must form teams and inform me by the end of Week 1 or I will form random teams.
- All Groups implement ASP.NET Web API Azure Hosted backend
- Peer review document to be submitted on final submission
- Video Evidence of Member Participation
- Final Product must include both existing third party Web API calls and self written API calls
- Product must be developed in a Git Team environment
- Looking at possibility of Xamarin cross platform front end application

## Project Outline:
_Technology Used_ - Unity with High Def Render Pipeline, Currently undecided stock image API, Custom API, FMod, ASP.NET application with SignalR and API.

_Initial Concept_ - Multiplayer puzzle game where players must exchange clues using the in-game chat in order to solve the puzzle.

One player sitting at PC screen has access to an assortment of pictures displayed on the screen, one of which is the correct picture to be clicked to solve the puzzle (or possibly move to the next puzzle). 

The second player must search a separate room for clues as to which picture is the correct answer.

### Win Condition:
The searching player must select the correct image.

### Steps to Win:
_Step 1:_ Both players start in separate rooms. One at the computer with the puzzle on screen, the other in the room with the clues.

_Step 2:_ Players relay info about their surroundings to one another over a text chat.

_Step 3:_ Player one must relay the information from the computer screen to player two.

_Step 4:_ Using the information obtained from player one, player two begins looking for hints and relays their findings back to player one.

_Step 5:_ Repeat steps 3 and 4 until player one thinks they have the right answer.

_Step 6:_ Player one chooses the correct image.

### Fail Condition:
The searching player fails to select the correct image.

### Conditions for Failure:
_Condition 1:_ Only one player spawns in. API fails to call images.

_Condition 2:_ Players refuse to communicate.

_Condition 3:_ Player one lies about what is on his screen.

_Condition 4:_ Player two lies about what they find.

_Condition 6:_ Player chooses the incorrect image.

## Mechanics:
_Chat:_ Both players have access to a chat function to communicate. Player one has access to a PC screen with the chat, player two has the following options; portable phone/tablet, PC, wall monitor.

_Player one specific:_ Player one has access to a PC with the puzzle on it. Can move around the room, possible location for easter eggs.

_Player two specific:_ Player two has access to the clues hidden around the room.
