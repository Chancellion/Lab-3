using System;
using System.Collections.Generic;

public class Компютер
{
    public string IP { get; set; }
public string Потужність { get; set; }
public string ТипОС { get; set; }
public Компютер(string ip, string потужність, string типОС)
    {
    IP = ip;
    Потужність = потужність;
    ТипОС = типОС;
}
public override string ToString()
{
    return $"IP: {IP}, Потужність: {Потужність}, Тип ОС: {ТипОС}";
}
}
public class Сервер : Компютер
{
    public Сервер(string ip, string потужність, string типОС) : base(ip, потужність, типОС) { }
}
public class РобочаСтанція : Компютер
{
    public РобочаСтанція(string ip, string потужність, string типОС) : base(ip, потужність, типОС) { }
}
public class Маршрутизатор : Компютер
{
    public Маршрутизатор(string ip, string потужність, string типОС) : base(ip, потужність, типОС) { }
}
public interface IConnectable
{
    void Підключитися(Компютер компютер);
    void Відключитися(Компютер компютер);
    void ПередатиДані(Компютер компютер, string дані);
}
public class Мережа : IConnectable
{
    private List<Компютер> компютери = new List<Компютер>();
    public void ДодатиКомпютер(Компютер компютер)
    {
        компютери.Add(компютер);
        Console.WriteLine($"Додано: {компютер}");
    }
    public void Підключитися(Компютер компютер)
    {
        Console.WriteLine($"{компютер} підключено до мережі.");
    }
    public void Відключитися(Компютер компютер)
    {
            Console.WriteLine($"{компютер} відключено від мережі.");
    }
    public void ПередатиДані(Компютер компютер, string дані)
    {
                Console.WriteLine($"Дані '{дані}' передано до {компютер}.");
    }
}
public class Програма
    {
        public static void Main(string[] args)
        {
            Мережа мережа = new Мережа();
        Console.WriteLine("Данiїл Iванченко, КIб-1-23-4.0д");
        Сервер сервер = new Сервер("192.168.1.1", "Висока", "Linux");
            РобочаСтанція робочаСтанція = new РобочаСтанція("192.168.1.2", "Середня", "Windows");
            Маршрутизатор маршрутизатор = new Маршрутизатор("192.168.1.3", "Низька", "Firmware");

            мережа.ДодатиКомпютер(сервер);
            мережа.ДодатиКомпютер(робочаСтанція);
            мережа.ДодатиКомпютер(маршрутизатор);
    
        мережа.Підключитися(сервер);
            мережа.ПередатиДані(робочаСтанція, "Привіт, світ!");
            мережа.Відключитися(маршрутизатор);
        }
    }