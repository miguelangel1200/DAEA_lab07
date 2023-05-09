using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2023
{
    public class Program
    {

        public static DataClasses1DataContext context = new DataClasses1DataContext();


        static List<Product> products = new List<Product>();
        static void Main(string[] args)
        {
            //int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            //ShowPares(numbers);
            //ShowParesLambda(numbers);

            //InsertProducts();
            //var productsExpensive = products.Where(x => x.Price > 50).ToList();

            //var customers = context.clientes.Where(x => x.NombreContacto.Contains("a")).ToList();

            /*foreach (var item in customers)
            {
                Console.WriteLine(item.NombreContacto);
            }*/


            //DataSource();
            //Filtering();
            //Ordering();
            //Grouping();
            //Grouping2();
            //Joining();

            //DataSourceLambda();
            //FilteringLambda();
            //OrderingLambda();
            //GroupingLambda();
            //Grouping2Lambda();
            JoiningLambda();

            Console.ReadLine();

        }

        static void DataSourceLambda()
        {
            var queryAllCustomers = context.clientes.Select(cust => cust).ToList();

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(cust => cust.Ciudad == "Londres").ToList();

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void OrderingLambda()
        {
            var queryLondonCustomers3 = context.clientes
                .Where(cust => cust.Ciudad == "Londres")
                .OrderBy(cust => cust.NombreCompañia)
                .Select(cust => cust)
                .ToList();
                

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(cust => cust.Ciudad).ToList();
           

            foreach (var item in queryCustomersByCity)
            {
                Console.WriteLine(item.Key);
                foreach (var item1 in item)
                {
                    Console.WriteLine("     {0}", item1.NombreCompañia);
                }
            }
        }

        static void Grouping2Lambda()
        {
            var custQuery = context.clientes
                            .GroupBy(cust => cust.Ciudad)
                            .Where(custGroup => custGroup.Count() > 2)
                            .OrderBy(custGroup => custGroup.Key)
                            .ToList();

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes
                                 .Join(context.Pedidos,
                                    cust => cust.idCliente,
                                    dist => dist.IdCliente,
                                    (cust, dist) => 
                                        new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario })
                                 .ToList();

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }


        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }


        static void Ordering()
        {
            var queryLondonCustomers3 = (from cust in context.clientes
                                        where cust.Ciudad == "Londres"
                                        orderby cust.NombreCompañia ascending
                                        select cust).ToList();

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach (var item in queryCustomersByCity)
            {
                Console.WriteLine(item.Key);
                foreach (var item1 in item)
                {
                    Console.WriteLine("     {0}", item1.NombreCompañia);
                }
            }
        }


        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }

        }




        private static void ShowPares(int[] numbers)
        {
            Console.WriteLine("ShowPares");
            var pares = (from c in numbers
                         where c % 2 == 0
                         select c).ToList();


            foreach (var par in pares) { Console.WriteLine(par); }
        }
        private static void ShowParesLambda(int[] numbers)
        {
            Console.WriteLine("ShowParesLambda");
            var pares = numbers.Where(x => x % 2 == 0).ToList();
            foreach (var par in pares) { Console.WriteLine(par); }
        }

        private static void InsertProducts()
        {
            string[] basicNeeds = { "Leche", "Pan", "Arroz", "Huevos", "Azúcar", "Aceite", "Sal", "Harina", "Pasta", "Jabón", "Papel higiénico", "Detergente", "Cepillo de dientes", "Shampoo", "Cebolla", "Zanahoria", "Papa", "Tomate", "Atún", "Pollo" };

            Random random = new Random();
            for (int i = 1; i <= 100; i++)
            {
                int productId = i;
                string name = basicNeeds[random.Next(0, basicNeeds.Length)];
                int price = random.Next(10, 100); // Genera un precio aleatorio entre 10 y 100
                products.Add(new Product { ProductId = productId, Name = name, Price = price });
            }

            var result = products.Where(p => p.Price < 50 && p.Name.Contains("m"))
                .Select(p => new
                {
                    Nombre = p.Name,
                    PrecioIGV = p.Price*1.18
                })
                .ToList();

            foreach (var item in result)
            {
                Console.Write(item.Nombre + " ");
                Console.Write(item.PrecioIGV);
                Console.WriteLine();
            }


            Console.WriteLine("");

        }


    }
}
