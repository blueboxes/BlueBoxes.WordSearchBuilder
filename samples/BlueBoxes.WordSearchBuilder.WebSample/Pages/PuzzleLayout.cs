namespace BlueBoxes.WordSearchBuilder.WebSample.Pages;

public class PuzzleLayout
{
    public int PuzzleCellSize { get; set; } = 40;
    public int PuzzleFontSize { get; set; } = 20;
    public int SolutionCellSize { get; set; } = 20;
    public int SolutionFontSize { get; set; } = 10;
    public int GridSize { get; set; } = 40;
    public int TitleSize { get; set; } = 15;
    public string FontFamily { get; set; } = "'Montserrat', sans-serif";
}

public class EasyPuzzleLayout : PuzzleLayout
{
    public EasyPuzzleLayout()
    {
        //15 * 40 = 600
        GridSize = 15;
        PuzzleCellSize = 40;
        PuzzleFontSize = 20;
        SolutionCellSize = 20;
        SolutionFontSize = 20 - (20 / 3);

        TitleSize = 50;
    }
}

public class EasyPuzzleLLargePrintLayout : PuzzleLayout
{
    public EasyPuzzleLLargePrintLayout()
    {
        //15 * 40 = 600
        GridSize = 15;
        PuzzleCellSize = 40;
        PuzzleFontSize = 24;
        SolutionCellSize = 20;
        SolutionFontSize = 20 - (20 / 3);
        FontFamily = "Helvetica";
        TitleSize = 50;
    }
}

public class EasyPuzzleGiantPrintLayout : PuzzleLayout
{
    public EasyPuzzleGiantPrintLayout()
    {
        //15 * 40 = 600
        GridSize = 10;
        PuzzleCellSize = 60;
        PuzzleFontSize = 30;//equal to 18pt
        SolutionCellSize = 20;
        SolutionFontSize = 20 - (20 / 3);
        FontFamily = "Helvetica";
        TitleSize = 50;
    }
}

public class MediumPuzzleLayout : EasyPuzzleLayout
{

}

public class HardPuzzleLayout : PuzzleLayout
{
    public HardPuzzleLayout()
    {
        GridSize = 20;
        PuzzleCellSize = 30;
        PuzzleFontSize = 17;
        SolutionCellSize = 12;
        SolutionFontSize = 10;
        TitleSize = 50;
    }
}