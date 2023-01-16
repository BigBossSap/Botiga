using System;
using System.Threading; 
namespace Botiga
{
    class Program
    {
        static void Main(string[] args)
        {
            int nElementsBotiga = 0;
            
            string[] productes = new string[40];
            double[] preus = new double[40];

            string num = Menu();
            Console.Clear();

            while (num != "20")
            {



                switch (num)
                {
                    case "1":

                        AfegirProducte(productes, preus, ref nElementsBotiga);
                        
                        break;



                    case "2":
                        ModificarPreu(productes, preus, ref nElementsBotiga);


                        break;

                    case "3":

                        
                        ModificarProducte(productes,  nElementsBotiga);


                        break;

                    case "4":
                        MostrarTaules(productes, preus, nElementsBotiga);


                        break;

                }

                num = Menu();
                Console.Clear();
            }


        }


        static void AfegirProducte(string[]prod, double[]preu, ref int nElements)
        {
          

            
            
                Console.WriteLine("Nom del producte: ");
                string producte = Console.ReadLine();

                Console.WriteLine("Preu: 00,00 €");
                double preuProd = Convert.ToDouble(Console.ReadLine());

                prod[nElements] = producte;
                preu[nElements] = preuProd;
                nElements++;

            Console.WriteLine("Vols afegir mes productes s/n");
            string opt = Console.ReadLine();
            if (opt == "s")
                AfegirProducte(prod, preu, ref nElements);



        }

        static void ModificarPreu(string[]prod, double[]preu, ref int nElements)
        {
            Console.WriteLine("Producte a buscar: ");
            string producteBuscar = Console.ReadLine();
            Console.WriteLine("Nou preu: ");
            double nouPreu = Convert.ToDouble(Console.ReadLine());
            int pos = posValorBuscar(prod, nElements, producteBuscar);
            preu[pos] = nouPreu;

        }

        static void ModificarProducte(string[]prod, int eElements)
        {
            Console.WriteLine("Producte a modificar: ");
            string producteMod = Console.ReadLine();
            for(int i=0; i<eElements;i++)
            {
                if (producteMod == prod[i])
                {
                    Console.WriteLine("Nom nou: ");
                    prod[i] = Console.ReadLine();

                }
            }


        }
        static int posValorBuscar(string[] prod, int nElements, string buscar)
        {

            int cont = 0;
            int pos = 0;

            for (int i = 0; i < nElements; i++)
            {

                if (buscar == prod[i])
                    pos = cont;

                cont++;
            }

            return pos;




        }
        static void MostrarTaules (string[] prod, double[] preus, int nElements)
        {
            string taulaProd = "";
           

            for (int i = 0; i < nElements; i++)
            {
                taulaProd += $"En l'index {i} trobem el producte <{prod[i]}> a un preu de {Convert.ToString(preus[i])} euros\n\n";

            }

            Console.WriteLine(taulaProd);
            Console.WriteLine();
            Console.WriteLine($"Tenim un total de {nElements} productes a la botiga");

            

          
        }


    


        static string Menu()
        {


            string[] menu = {
                "1. Afegir prod.\n",
            "2.Modificar prod\n"};
                


            Console.WriteLine(new String('*', Console.WindowWidth));

            Console.WriteLine();
            
                                                                    

            Console.WriteLine();



            foreach (string opcio in menu)
            {




                Console.WriteLine(FormatMenu(opcio));
            }



            Console.WriteLine();
            Console.WriteLine(new String('*', Console.WindowWidth));

            Console.Write(FormatMenu("Selecciona una opció: "));

            string num = Console.ReadLine();



            return num;
        }
        static void ContadorRetorn()
        {

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(Format("Presiona qualsevol tecla per continuar..."));
            Console.ReadKey();

            for (int i = 3; i >= 1; i--)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(Format($"Tornant al menu en {i} segons..."));
               

            }
            Console.Clear();
        }

        static string FormatMenu(string text)
        {
            text = new string(' ', 23) + text;
            return text;
        }

        static string Format(string text)
        {

            text = new string(' ', ((Console.WindowWidth - (text.Length)) / 2)) + text;
            return text;
        }
    }
}
