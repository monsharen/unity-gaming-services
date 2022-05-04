# Unity Gaming Services Demo

This is a demo application highlighting a few of the available Unity gaming services. To keep the project and codebase easily digestable it's been separated into three scenes. Each scene integrates one Unity gaming service.

The following Unity gaming services are currently implemented:
- Authentication
- Analytics
- Lobby

The game also features some basic game mechanics:
- Fuel consumption
- Buying/selling star dust

# Scenes
The project consists of the following three scenes: 
- Start scene (Authentication)
- World Map scene (Analytics)
- Trading Hub scene (Lobby)

## Start Scene (Authentication)
This scene implements Unity Authentication. This is a great place to add authentication related UI in case we later wish to extend the project with custom authentication flows.

## World Map Scene (Analytics)
Travel to nearby planets. This scene implements Analytics to help us identify which planets players travel to.

## Trading Hub Scene (Lobby)
This scene uses Lobby to add a social aspect to the game and provide a sense of a living universe.

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
