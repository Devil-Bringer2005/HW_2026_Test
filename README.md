# Doofus Adventure Game

A Unity implementation of the **Doofus Adventure Game** assignment.

Guide Doofus across disappearing green Pulpits and try to reach as many platforms as possible before they disappear.

## Features

- WASD / Arrow Key movement
- Player speed loaded from `doofus_diary.json`
- Dynamic Pulpit generation
- Random Pulpit lifetime
- Maximum of 2 active Pulpits
- World-space Pulpit countdown timer
- Score increases when reaching a new Pulpit
- Start screen
- Game Over screen
- Retry functionality
- Rigidbody-based falling and collision detection

## Levels Completed

### Level 1
- Character movement
- Pulpit placement
- JSON-based configuration

### Level 2
- Score updates when reaching a new Pulpit

### Level 3
- Start screen
- Game Over screen
- Retry system

## Configuration

Gameplay values are loaded from:

`Assets/Resources/doofus_diary.json`


Tech Stack
Unity
C#
Unity Input System
TextMeshPro
Rigidbody Physics
JSON Configuration


How to Play
Start the game.
Click Start.
Move Doofus between the Pulpits.
Reach new Pulpits to increase your score.
Avoid falling when a Pulpit disappears.
Try to reach 50 Pulpits.
