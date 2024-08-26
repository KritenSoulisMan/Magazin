using System;
using System.IO;
using System.Threading.Tasks;

namespace Magazin
{
    // Начало программы
    #region
    class Program
    {
        static void Main(string[] args) 
        {
            // Задача: создать подобие работы БД магазина, используя массивы, как хранилище данных. Мы выступаем в роли покупателя и продовца одновременно.
            // В задаче должно присутствовать 2 массива, 1 цена и 2 название товара, сопоставление товара и цены должны быть в ID 1 к 1.

            // Старт
            #region
            Console.WriteLine("Старт програмы...");
            Console.Write("Програма находится на Раней стадии разработки...\n" +
                "Напишите 'Старт' для входа в систему: ");

            string n = Console.ReadLine();
            if (n == "Старт")
            {
                Magazin magazin = new Magazin();
                magazin.MainMenu();
            }
            else
            {
                Console.WriteLine("Проверьте ошибки и повторите попытку позже...");
            }
            #endregion
        }
    }
    #endregion

    // Тело магазина
    #region
    class Magazin
    {
        List<Item> Items = new List<Item>();
        List<Cart> СartItems = new List<Cart>();


        // Главное меню магазина
        #region
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Успешный старт!\n" +
                    "1. Список продуктов\n" +
                    "2. Добавить продукт\n" +
                    "3. Купить продукт\n" +
                    "4. Корзина товаров\n" +
                    "Напишите номер пункта действия:\n ");

                switch (int.Parse(Console.ReadLine()))
                {
                    case 1: // Вывод списка
                        ShowProduct();
                        break;

                    case 2: // Добавление продукции в список или массив
                        AddProcedur();
                        break;

                    case 3: // *Покупка* продукта
                        BuyProduct();
                        break;

                    case 4: // Корзина
                        CartList();
                        break;

                    default: // Отсутствующий вариант
                        Console.WriteLine("Такой вариант не риализован!");
                        break;
                }
            }
        }
        #endregion

        // Просмотр всех продуктов в магазине.
        #region
        void ShowProduct()
        {
            if (Items.Count > 0)
            {
                Console.WriteLine("Продукты доступные сейчас: ");
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine($"Название: {Items[i].NAME}. Цена: {Items[i].COST}руб.");
                }
            }
            else
            {
                Console.WriteLine("ВНИМАНИЕ: Товары времено отсутствуют!");
                Console.WriteLine("Желаете перейти в главное меню? (Да/Нет)");
                string selector = Console.ReadLine();
                switch (selector)
                {
                    case "Да" or "да":
                        MainMenu();
                        break;
                }
            }
        }
        #endregion

        // Добавление, удаление, изминение продуктов в магазине.
        #region
        void AddProcedur()
        {
            Console.Clear();
            Console.WriteLine("Напишите название продукта: ");
            string Name = Console.ReadLine();

            Console.WriteLine("Напишите цену продукта: ");
            int Cost = int.Parse(Console.ReadLine());

            AddProduct(Name, Cost);
        }

        void AddProduct(string Name, int Cost)
        {
            var item = new Item();
            item.NAME = Name;
            item.COST = Cost;

            Items.Add(item);

            Console.WriteLine("Продукт успешно добавлен!");
            Console.WriteLine("Желаете продолжить?");

            switch (Console.ReadLine())
            {
                case "Да" or "Нет":
                    AddProcedur();
                    break;
                case "Нет" or "нет":
                    MainMenu();
                    break;
            }
        }
        #endregion

        // Сохранение в корзину.
        #region
        void BuyProduct()
        {
            Console.Clear();
            Console.WriteLine("Продукты доступные сейчас: ");
            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"Название: {Items[i].NAME}. Цена: {Items[i].COST}руб.");
            }

            try
            {
                // Получаем номер продукта. Отсчёт в массиве начинается с 0
                Console.WriteLine("Напишите номер продукта, который хотите купить: \n");
                int nump = int.Parse(Console.ReadLine()) - 1;

                // Выбор продукта из списка Items.
                var product = Items[nump];
                Console.Write("Сколько кг? ");
                float kg = float.Parse(Console.ReadLine());

                var productkg = kg * product.COST;
                Console.WriteLine($"Цена: {productkg}руб.");

                Console.WriteLine();
                Console.WriteLine("Напишите 'Да', если хотите продолжить покупки.\n" +
                    "'Нет', если хотите перейти к оплате. ");
            }
            catch
            {
                Console.WriteLine("Повторите попытку позже...");
                Task.Delay(5000);
                MainMenu();
            }

            switch (Console.ReadLine())
            {
                case "Да" or "да":
                    Console.Clear();
                    CartList();
                    break;

                case "Нет" or "нет": // В разработке...
                    Console.Clear();
                    break;
            }
        }
        #endregion

        // Оплата покупки.
        #region
        async void OrderBuyList()
        {
            Console.WriteLine("Согласны с этим списком продуктов?");
            for (int i = 0; i < СartItems.Count; i++)
            {
                Console.WriteLine(СartItems[i]);
            }

            switch (Console.ReadLine())
            {
                case "Да" or "да":
                    Console.Clear();
                    Console.WriteLine("Продукты: ");
                    for (int i = 0; i < СartItems.Count; i++)
                    {
                        Console.WriteLine(СartItems[i]);
                    }
                    int sum = 0;
                    for (int money = 0; money < СartItems.Count; money++)
                    {
                        sum += СartItems[money].COST;
                    }

                    Console.WriteLine($"Итого к оплате: {sum}руб.");
                    break;

                case "Нет" or "нет":
                    Console.WriteLine();
                    Task.Delay(5000);
                    Console.Clear();
                    break;
            }
        }
        #endregion

        // Просмотр корзины.
        #region
        void CartList()
        {
            if (СartItems.Count > 0)
            {
                Console.WriteLine($"Сейчас у вас в корзине имеется: ");
                for (int i = 0; i < СartItems.Count; i++)
                {
                    Console.WriteLine(СartItems[i]);
                }
            }
            else
            {
                Console.WriteLine("Ваша корзина пуста...");
                Console.WriteLine("Желаете перейти в главное меню? (Да/Нет)");

                switch (Console.ReadLine())
                {
                    case "Да" or "да":
                        MainMenu();
                        break;
                    case "Нет" or "нет":
                        Console.WriteLine("Ну тогда оставайтесь тут.");
                        break;
                }
            }
        }
        #endregion
    }
    #endregion

    // Классы для списков
    #region
    class Item // Попытка 1, сделать через List
    {
        public string NAME { get; set; }
        public int COST {  get; set; } 
    }

    class Cart
    {
        public string NAME { get; set; }
        public int KG { get; set; }
        public int COST { get; set; }
    }
    #endregion
}
