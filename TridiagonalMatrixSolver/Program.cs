using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите размер матрицы (n):");
        int n = int.Parse(Console.ReadLine());

        double[] lower = new double[n - 1];
        double[] main = new double[n];
        double[] upper = new double[n - 1];
        double[] d = new double[n];

        Console.WriteLine("Введите элементы главной диагонали (размерность {0}):", n);
        for (int i = 0; i < n; i++)
        {
            Console.Write($"b[{i}]: ");
            main[i] = double.Parse(Console.ReadLine());
        }

        if (n > 1)
        {
            Console.WriteLine("Введите элементы нижней диагонали (размерность {0}):", n - 1);
            for (int i = 0; i < n - 1; i++)
            {
                Console.Write($"a[{i}]: ");
                lower[i] = double.Parse(Console.ReadLine());
            }

            Console.WriteLine("Введите элементы верхней диагонали (размерность {0}):", n - 1);
            for (int i = 0; i < n - 1; i++)
            {
                Console.Write($"c[{i}]: ");
                upper[i] = double.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("Введите элементы вектора правых частей (размерность {0}):", n);
        for (int i = 0; i < n; i++)
        {
            Console.Write($"d[{i}]: ");
            d[i] = double.Parse(Console.ReadLine());
        }

        double[] result = SolveTridiagonal(lower, main, upper, d);

        Console.WriteLine("Решение системы:");
        for (int i = 0; i < result.Length; i++)
        {
            Console.WriteLine($"x[{i}] = {result[i]}");
        }
    }

    static double[] SolveTridiagonal(double[] lower, double[] main, double[] upper, double[] d)
    {
        int n = main.Length;
        double[] x = new double[n];
        double[] cPrime = new double[n];
        double[] dPrime = new double[n];

        // Прямой ход
        if (n > 1)
        {
            cPrime[0] = upper[0] / main[0];
            dPrime[0] = d[0] / main[0];

            for (int i = 1; i < n; i++)
            {
                double m = 1.0 / (main[i] - lower[i - 1] * cPrime[i - 1]);
                cPrime[i] = (i == n - 1) ? 0 : upper[i] * m;
                dPrime[i] = (d[i] - lower[i - 1] * dPrime[i - 1]) * m;
            }
        }
        else
        {
            cPrime[0] = 0;
            dPrime[0] = d[0] / main[0];
        }

        // Обратный ход
        x[n - 1] = dPrime[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            x[i] = dPrime[i] - cPrime[i] * x[i + 1];
        }

        return x;
    }
}
