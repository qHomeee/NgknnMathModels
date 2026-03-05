
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
int[,] arrFinal = MainFunction.CreateFinalTable(arrRate, arrCustomers, arrStore);
Console.WriteLine();
MainFunction.printmas(arrFinal);

while (true) {
    Console.WriteLine("Выберите какой метод хотите использовать 1) метод минимального элемента: ");
    int choice = int.Parse(Console.ReadLine()!);

    switch (choice)
    {
        case 1:
            Methods.MethodOfMinEl.MainOfMethodMinEl(arrFinal);
            break;
    }

}