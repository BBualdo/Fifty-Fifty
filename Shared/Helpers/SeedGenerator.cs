﻿using Domain.Entities;

namespace Shared.Helpers;

internal class SeedGenerator
{
    public static List<TaskTemplate> GenerateTemplateTasks() => [
    new() { Id = 1, Title = "Do the dishes", Score = 10 },
    new() { Id = 2, Title = "Take out the trash", Score = 5 },
    new() { Id = 3, Title = "Vacuum the floor", Score = 15 },
    new() { Id = 4, Title = "Clean the bathroom", Score = 20 },
    new() { Id = 5, Title = "Mow the lawn", Score = 25 },
    new() { Id = 6, Title = "Wash the car", Score = 30 },
    new() { Id = 7, Title = "Walk the dog", Score = 10 },
    new() { Id = 8, Title = "Feed the cat", Score = 5 },
    new() { Id = 9, Title = "Water the plants", Score = 5 },
    new() { Id = 10, Title = "Cook dinner", Score = 20 },
    new() { Id = 11, Title = "Do the laundry", Score = 15 },
    new() { Id = 12, Title = "Clean the windows", Score = 10 },
    new() { Id = 13, Title = "Organize the closet", Score = 15 },
    new() { Id = 14, Title = "Dust the furniture", Score = 10 },
    new() { Id = 15, Title = "Take out recycling", Score = 5 },
    new() { Id = 16, Title = "Wipe down kitchen counters", Score = 5 },
    new() { Id = 17, Title = "Sweep the porch", Score = 10 },
    new() { Id = 18, Title = "Change bed sheets", Score = 15 },
    new() { Id = 19, Title = "Empty the dishwasher", Score = 5 },
    new() { Id = 20, Title = "Fold and put away laundry", Score = 10 },
    new() { Id = 21, Title = "Clean out the fridge", Score = 20 },
    new() { Id = 22, Title = "Take care of compost", Score = 5 },
    new() { Id = 23, Title = "Shovel snow", Score = 30 },
    new() { Id = 24, Title = "Weed the garden", Score = 20 },
    new() { Id = 25, Title = "Clean up after a meal", Score = 10 },
    new() { Id = 26, Title = "Sweep the floor", Score = 10 },
    new() { Id = 27, Title = "Tidy up the living room", Score = 10 },
    new() { Id = 28, Title = "Wipe down bathroom mirror", Score = 5 },
    new() { Id = 29, Title = "Take out the mail", Score = 5 },
    new() { Id = 30, Title = "Organize pantry", Score = 15 },
    new() { Id = 31, Title = "Replace toilet paper", Score = 5 },
    new() { Id = 32, Title = "Check and refill pet water bowl", Score = 5 },
    new() { Id = 33, Title = "Polish wooden furniture", Score = 10 },
    new() { Id = 34, Title = "Sanitize door handles", Score = 5 },
    new() { Id = 35, Title = "Clean behind the sofa", Score = 15 },
    new() { Id = 36, Title = "Vacuum the stairs", Score = 15 },
    new() { Id = 37, Title = "Take care of houseplants", Score = 5 },
    new() { Id = 38, Title = "Organize shoes at the entrance", Score = 5 },
    new() { Id = 39, Title = "Wipe down light switches", Score = 5 },
    new() { Id = 40, Title = "Declutter a room", Score = 15 },
    new() { Id = 41, Title = "Clean up kids' toys", Score = 10 },
    new() { Id = 42, Title = "Scrub kitchen sink", Score = 10 },
    new() { Id = 43, Title = "Sort out junk drawer", Score = 10 },
    new() { Id = 44, Title = "Defrost the freezer", Score = 20 },
    new() { Id = 45, Title = "Replace burnt-out light bulbs", Score = 5 },
    new() { Id = 46, Title = "Organize office desk", Score = 10 },
    new() { Id = 47, Title = "Disinfect TV remote and electronics", Score = 5 },
    new() { Id = 48, Title = "Sweep the driveway", Score = 10 }
    ];
}