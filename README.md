# Unity Gaming Services Demo

This is a demo application highlighting a few of the available Unity gaming services. To keep the project and codebase easily digestable it's been separated into three scenes. Each scene integrates one Unity gaming service.

The following Unity gaming services are currently implemented:
- Authentication
- Analytics
- Lobby

The game also features some basic game mechanics:
- Fuel consumption
- Buying/selling space dust

The goal of the game is to make a profit by selling space dust on far away planets and their trading hubs.

# Scenes
The project consists of the following three scenes: 
- Start scene (Authentication)
- World Map scene (Analytics)
- Trading Hub scene (Lobby)

The game will automatically transition between the scenes as you navigate through the game. 

## Start Scene (Authentication)
This scene implements Unity Authentication. This is a great place to add authentication related UI in case we later wish to extend the project with custom authentication flows. In its current implementation it uses anonymous authentication. It will automatically transition to the World Map when the users is authenticated.

## World Map Scene (Analytics)
Travel to nearby planets and visit the trading hubs. The distance you can travel is limited by the amount of fuel you have left. This scene implements Analytics to help us identify which planets players travel to. The game will transition to the Trading hub scene when the space ship reaches a planet.

## Trading Hub Scene (Lobby)
This scene uses Lobby to add a social aspect to the game and provide a sense of a living universe. The player can buy/sell space dust and refuel their ship.

# Extending the demo

This demo can easily be extended. Here are some ideas that could be worth exploring.

## Economy
The game is currently poorly balanced. Integrating Economy could help balancing it with the help of Analytics. This could be done by replacing the hardcoded buy/sell values with configurable ones. 

## Ads
Add interstitials when players arrive to new planets.

## In-App Purchases
Integrating in-app purchases could allow the user to purchase additional in-game credits. Perhaps let the user purchase boosters which make their ship fly faster? Or purchase an ad free experience?

## Relay
Add the ability to chat and trade with other players on the same planet.

# Built and tested with
- Unity 2021.1.21f1
- Macbook Pro (Monterey 12.2.1)
