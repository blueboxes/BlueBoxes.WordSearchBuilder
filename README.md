# BlueBoxes.WordSearchBuilder

This package is a .Net 8 library for creating, solving and exporting word searches in json [iPuz format](https://ipuz.readthedocs.io/en/latest/reading.html#validation-for-wordsearch-puzzles).

Words can be placed at random horizontally, vertically or diagonally, forwards or backwards. The word search can be any size, and the words to be found can be of any length.

Snaking Word Search puzzles where the words can change direction are not supported directly however could easily be added.

## Getting started


### Creating a word search

```csharp
```

### Solving a word search

```csharp

```

### Saving a word search

```csharp
var options = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Converters = { new PlacedWordJsonConverter(), new GridJsonConverter() }
};
var json = JsonSerializer.Serialize(puzzles, options);
await File.WriteAllTextAsync(fileName, json, Encoding.UTF8);
```

### Prerequisites

This package requires the following dependencies:
- .net 8.0

## Usage

Examples about how to use your package by providing code snippets/example images, or samples links on GitHub if applicable. 

- Provide sample code using code snippets
- Include screenshots, diagrams, or other visual help users better understand how to use your package

Further samples can be found in the samples folder of this repository.

## Additional documentation

* WordSearches in [iPuz format](https://ipuz.readthedocs.io/en/latest/reading.html#validation-for-wordsearch-puzzles)

## Future Plans
This code is based on .Net 8 rather than .Net Standard 2.0 as it was part of a larger personal project before making in to a shareable library. This means that it will not work on .Net Framework or older version of Core. I would be open to adding support for .Net Standard 2.0 in the future.

Currently it only supports English character sets although has been built with others in mind. I would like to add support for other languages in the future and would welcome contributions.