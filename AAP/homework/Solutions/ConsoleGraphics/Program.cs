using System;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("====== Программа ЁЛЫ ======");
        Console.Write("M=");
        int rowsCount = int.Parse(Console.ReadLine()!);
        Console.Write("N=");
        int columnsCount = int.Parse(Console.ReadLine()!);
        Console.Write("K=");
        int treeHeight = int.Parse(Console.ReadLine()!);

        DrawForest(rowsCount, columnsCount, treeHeight);
    }

    private static void DrawForest(int rowsCount, int columnsCount, int treeHeight)
    {
        for (int i = 0; i < rowsCount; i++)
        {
            DrawTreeRow(columnsCount, treeHeight);
            Console.WriteLine();
        }
    }

    private static void DrawTreeRow(int columns, int treeHeight)
    {
        int trunkIndent = treeHeight - 1;
        int indents = trunkIndent;
        int leaves = 1;

        const int leafIncrement = 2;
        const int indentIncrement = -1;

        for (int layer = 0; layer < treeHeight; layer++)
        {
            DrawLeavesRow(indents, leaves, columns);
            indents += indentIncrement;
            leaves += leafIncrement;
        }

        const int trunk = 1;
        DrawLeavesRow(trunkIndent, trunk, columns);
    }

    private static void DrawLeavesRow(int indents, int leaves, int columns)
    {
        DrawIndents(indents);
        DrawLeaves(leaves);

        for (int tree = 1; tree < columns; tree++)
        {
            DrawIndents(2 * indents + 1);
            DrawLeaves(leaves);
        }

        Console.WriteLine();
    }

    private static void DrawIndents(int indentCount)
    {
        for (int _ = 0; _ < indentCount; _++)
            Console.Write(" ");
    }

    private static void DrawLeaves(int leafCount)
    {
        const char leaf = '#';
        for (int _ = 0; _ < leafCount; _++)
            Console.Write(leaf);
    }
}
