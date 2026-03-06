
using System.ComponentModel;
using System.Xml.Serialization;
using MathsMethods;


int n = 0;
int m = 0;
Console.WriteLine("введите количество поставщиков: ");
n = int.Parse(Console.ReadLine()!);
Console.WriteLine("введите количество покупателей: ");
 m = int.Parse(Console.ReadLine()!);
int[] arrCustomers = new int[m];
int[] arrStore = new int[n];
int[,] arrRate = new int[n, m];
Console.WriteLine("Заполните массив складов складов: ");
MainFunction.initmas(ref arrStore);
Console.WriteLine("Заполните массив с покупателями: ");
MainFunction.initmas(ref arrCustomers);
Console.WriteLine("Заполнить массив с тарифами: ");
MainFunction.init2mas(ref arrRate);
MainFunction.printmas(arrRate);
int[,] ArrFull;

while (true)
{
    Console.WriteLine("Выберите метод: 1) мин.элемент 2) северо-западный");
    int choice = int.Parse(Console.ReadLine()!);

    Methods.TransportResult res;

    switch (choice)
    {
        case 1:
            res = Methods.MinElemMethod(arrStore, arrCustomers, arrRate);
            break;
        case 2:
            res = Methods.NorthwestCorner(arrStore, arrCustomers, arrRate);
            break;
        default:
            Console.WriteLine("Неверный выбор");
            continue;
    }

    Console.WriteLine("\nТарифы (для проверки):");
    ArrFull = MainFunction.CreateFinalTable(arrRate, arrCustomers, arrStore);
    MainFunction.printmas(ArrFull);

    Console.WriteLine("\nПлан перевозок:");
    MainFunction.printmas(res.Plan);

    Console.WriteLine($"\nЦелевая функция = {res.TotalCost}");
    if(Methods.OptimOrNot(res.Plan) == true)
    {
        Console.WriteLine("план оптимален!");
    }
    else
    {
        Console.WriteLine("План не оптимален! Хотите ввести Более оптимальный");
    }
}
