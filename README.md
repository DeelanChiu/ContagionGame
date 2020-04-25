# Contagion
Hello! This is a game called Contagion that I built as a personal project using Unity, C# and Paint (for the 'artwork').
It asks players to save as many people as possible amidst a pandemic by preventing a deadly virus from spreading
across every town.

To play, simply clone the repository and execute the file for your respective operating system (BuildForWindows/Contagion.exe for Windows and BuildForMacOS.app for Mac OS). The game's intended resolution is 1024 x 768.

## How to Play
![Demo](/demo.PNG)

During each level, the player will be given the layout of a city, which is populated by towns that are connected by roads.
After a few seconds, a town/towns will be infected. When a town is **infected**, it becomes **red** and its population starts **evacuating** to connected towns that are healthy. However, the longer an evacuation, the more risky it becomes for the connected towns to be infected as well. The towns in **danger** of being infected are **yellow** and the **rising chance of infection at a given time** is displayed. Essentially, every time the chance increases, a dice is rolled to see if the town gets infected. To stop the evacuation, the player can **block** a road by **clicking** on it, protecting a previously connected town from further exposure. However, the number of blocks the player can use is **limited**. In the case where the population of an infected town has no connected healthy towns to evacuate to, the towns goes **offline** and the trapped population **dies**. The goal then is to contain the pandemic while making the necessary sacrifices to keep as many people alive as possible.

