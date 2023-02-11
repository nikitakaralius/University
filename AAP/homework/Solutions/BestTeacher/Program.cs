using System;

internal class Program
{
    public static void Main(string[] args)
    {
        double[,] marks =
        {
            {3.6, 3.1, 2.8, 1, 4, 3.3, 3.2, 3},
            {3.5, 3.6, 4.1, 3.9, 3.5, 5, 4, 5},
            {2.2, 2.7, 3.1, 3, 4.5, 2.2, 3.1, 3.7},
            {4.2, 3.4, 3, 4.3, 4.1, 4.6, 4.4, 4.5},
            {4.7, 4.1, 3.6, 2.1, 2.7, 2, 2.5, 2.7}
        };

        int bestTeacherIndex = FindBestTeacher(marks, out double averageMark);

        if (bestTeacherIndex == -1)
        {
            Console.WriteLine("Невозможно определить лучшего преподавателя");
        }

        Console.WriteLine($"{bestTeacherIndex} {averageMark:F2}");
    }

    private static int FindBestTeacher(double[,] marks, out double averageMark)
    {
        int bestTeacherIndex = -1;
        double highestAverageMark = 0;

        for (int i = 0; i < marks.GetLength(0); i++)
        {
            int marksPerTeacherCount = marks.GetLength(1);

            if (marksPerTeacherCount == 0)
            {
                continue;
            }

            double maxTeacherMark = marks[i, 0];
            double minTeacherMark = marks[i, 0];
            double teacherMarksSum = 0;

            for (int j = 0; j < marksPerTeacherCount; j++)
            {
                maxTeacherMark = Math.Max(maxTeacherMark, marks[i, j]);
                minTeacherMark = Math.Min(minTeacherMark, marks[i, j]);
                teacherMarksSum += marks[i, j];
            }

            double teacherAverageMark = marksPerTeacherCount > 2
                ? (teacherMarksSum - maxTeacherMark - minTeacherMark) / (marksPerTeacherCount - 2)
                : teacherMarksSum / marksPerTeacherCount;

            if (teacherAverageMark > highestAverageMark)
            {
                highestAverageMark = teacherAverageMark;
                bestTeacherIndex = i;
            }
        }

        averageMark = highestAverageMark;

        return bestTeacherIndex;
    }
}
