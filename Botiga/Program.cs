using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
namespace Botiga
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = 37;
            Console.WindowWidth = 85;
            double diners = 1000;
            int nElementsBotiga = 0;
            int nelementsCistella = 0;
            string[] productesCistella = new string[100];
            double[] quantitat = new double[100];
            double[] preusCistella = new double[100];
            string[] productes = new string[2];
            double[] preus = new double[2];
            string num = Menu();
            Console.Clear();
            while (num != "20")
            {
                switch (num)
                {
                    case "1":
                        string opt = "s";
                        while (opt == "s")
                        {
                            if (nElementsBotiga >= productes.Length)
                                AmpliarBotiga(ref productes, ref preus);
                            Console.WriteLine();
                            
                            Console.Write("Nom del producte: ");
                            string producte = Console.ReadLine();                            
                            Console.Write("Preu(00,00€): ");                          
                            string preuProd = (Console.ReadLine());
                            //while ((preuProd<0||preuProd>double.MaxValue) && ValidadorNum(preuProd){ }                          
                            while (!preuProd.All(char.IsDigit) || (double.Parse(preuProd) < 0))
                            {
                                Console.WriteLine("Preu Invalid. Torna a intentar: ");
                                Console.Write("Preu(00,00€): ");
                                preuProd = (Console.ReadLine());
                            }
                            double preuProdBo=Convert.ToDouble(preuProd);
                            AfegirProducte(productes, preus, ref nElementsBotiga, producte, preuProdBo);
                            Console.WriteLine(Format("Vols afegir mes productes s/n"));
                            opt = Console.ReadLine();
                        }
                        break;
                    case "2":
                        ModificarPreu(productes, preus, ref nElementsBotiga);
                        break;
                    case "3":
                        ModificarProducte(productes,  nElementsBotiga);
                        break;
                    case "4":
                        MostrarTaulesBotiga(productes, preus, nElementsBotiga);
                        break;
                    case "5":
                        AmpliarBotiga(ref productes, ref preus);
                        break;
                    case "6":
                       preus= SortArray(preus, 0, preus.Length-1);
                        break;
                    case "7":
                        Console.WriteLine("Producte a comprar: ");
                        string producteComprar = Console.ReadLine();
                        Console.WriteLine("Quantitat: ");
                        string quantitatComprar = Console.ReadLine();
                        while (!quantitatComprar.All(char.IsDigit) || (int.Parse(quantitatComprar) < 0))
                        {
                            Console.WriteLine("Quantitat incorrecta, torna a intentar: ");
                            Console.Write("Quantitat: ");
                            quantitatComprar = Console.ReadLine();
                        }
                        int quantitatComprarBo = int.Parse(quantitatComprar);
                        AfegirCistella(preus, preusCistella, productes, productesCistella,quantitat, producteComprar, nElementsBotiga, ref nelementsCistella, quantitatComprarBo);
                        ContadorRetorn();
                        break;
                    case "8":
                        MostrarTaulesCistella(productesCistella, quantitat,preusCistella, nelementsCistella);
                        ContadorRetorn();
                        break;
                }
                num = Menu();
                Console.Clear();
            }
        }
        static void AfegirProducte(string[]productes, double[]preus, ref int nElementsBotiga, string producte, double preuProducte)
        {                    
                productes[nElementsBotiga] = producte;
                preus[nElementsBotiga] = preuProducte;
                nElementsBotiga++;
            
        }
        static void AmpliarBotiga(ref string[]prod, ref double[]preu)
        {
            Console.WriteLine("No hi ha prou espai a la botiga: ");
            Console.WriteLine("Capacitat a ampliar: ");
            int ampliar = Convert.ToInt32(Console.ReadLine());
            string[] copiaProd = new string[prod.Length+ampliar];
            for (int i=0; i<prod.Length;i++)
            {
                copiaProd[i] = prod[i];
            }
            prod = copiaProd;
            double[] copiaPreu = new double[preu.Length + ampliar];
            for (int j = 0; j < preu.Length; j++)
            {
                copiaPreu[j] = preu[j];
            }
            preu = copiaPreu;
        }
        static void ModificarPreu(string[]prod, double[]preu, ref int nElements)
        {
            Console.WriteLine("Producte a buscar: ");
            string producteBuscar = Console.ReadLine();
            Console.WriteLine("Nou preu: ");
            string nouPreu = Console.ReadLine();
            while (!nouPreu.All(char.IsDigit) || (double.Parse(nouPreu) < 0))
            {
                Console.WriteLine("Preu Invalid. Torna a intentar: ");
                Console.Write("Nou preu: ");
                nouPreu = (Console.ReadLine());
            }
            double nouPreuValid = int.Parse(nouPreu);
            int pos = posValorBuscar(prod, nElements, producteBuscar);
            preu[pos] = nouPreuValid;
        }
        static void ModificarProducte(string[]prod, int nElements)
        {
            Console.WriteLine("Producte a modificar: ");
            string producteMod = Console.ReadLine();
            for(int i=0; i<nElements;i++)
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
            int pos = -1;
            for (int i = 0; i < nElements; i++)
            {
                if (buscar == prod[i])
                    pos = cont;
                cont++;
            }
            return pos;
        }
        static void MostrarTaulesBotiga (string[] prod, double[] preus, int nElements)
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
        static void MostrarTaules(string[] prod, double[] preus)
        {
            string taulaProd = "";
            for (int i = 0; i < prod.Length; i++)
            {
                taulaProd += $"En l'index {i} trobem el producte <{prod[i]}> a un preu de {Convert.ToString(preus[i])} euros\n\n";
            }
            Console.WriteLine(taulaProd);
            Console.WriteLine();          
        }
        static void MostrarTaulesCistella(string[] prod, double[] quantitat, double[]preusCistella, double nElementsBotiga)
        {
            string taulaProd = "";
            double total = 0;
            for (int i = 0; i < nElementsBotiga; i++)
            {
                Console.WriteLine();
                taulaProd += Format($"<{prod[i]}> preu unitari: {preusCistella[i]}€ x {quantitat[i]} = {calcularTotal(quantitat,preusCistella,i)}€ \n\n");
                total += calcularTotal(quantitat, preusCistella, i);
            }        
            Console.WriteLine(taulaProd);
            Console.WriteLine();
            Console.WriteLine(Format($"Total: {Math.Round(total,2)}€ "));
        }
        static double calcularTotal(double[] quantitat, double[] preuscistella, int i)
        {
            double total = quantitat[i] * preuscistella[i];
            return total;
        }
        static double[] SortArray(double[] array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }
                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    double temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }
            if (leftIndex < j)
                SortArray(array, leftIndex, j);
            if (i < rightIndex)
                SortArray(array, i, rightIndex);
            return array;
        }
        static void AfegirCistella(double[]preus, double[]preusCistella, string[]productesBotiga, string[] productesCistella, double[] quantitatCistella, string producteComprat, int nElements, ref int nElementsCistella, int quantitat)
        {
            int pos = posValorBuscar(productesBotiga, nElements, producteComprat);
            if (pos != -1)
            {
                productesCistella[pos] = producteComprat;
                quantitatCistella[pos] = quantitat;
                preusCistella[pos] = preus[pos];
                nElementsCistella++;
            }           
            else
            {
                Console.WriteLine("Error");
            }                          
        }
        static string Menu()
        {
            string[] menu = {
            "1. Afegir prod.\n",
            "2. Modificar preu prod\n",
            "3. Modificar producte\n",
            "4. Mostrar taules\n",
            "5. Ampliar capacitat de la botiga\n",
            "6. Quicksort\n",
            " \n",
            "CISTELLA",
            " \n",
            "7. Afegir productes a la cistella.\n",
            "8. Mostrar cistella i total\n",
            };               
            Console.WriteLine(new String('*', Console.WindowWidth));
            Console.WriteLine();                                                                            
            Console.WriteLine(@"
        ██╗      █████╗     ████████╗██╗███████╗███╗   ██╗██████╗  █████╗ 
        ██║     ██╔══██╗    ╚══██╔══╝██║██╔════╝████╗  ██║██╔══██╗██╔══██╗
        ██║     ███████║       ██║   ██║█████╗  ██╔██╗ ██║██║  ██║███████║  
        ██║     ██╔══██║       ██║   ██║██╔══╝  ██║╚██╗██║██║  ██║██╔══██║
        ███████╗██║  ██║       ██║   ██║███████╗██║ ╚████║██████╔╝██║  ██║
        ╚══════╝╚═╝  ╚═╝       ╚═╝   ╚═╝╚══════╝╚═╝  ╚═══╝╚═════╝ ╚═╝  ╚═╝
");
            foreach (string opcio in menu)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(FormatMenu(opcio));
                Console.ResetColor();
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

        static bool ValidadorNum(string num)
        {
            int esnum;
            return Int32.TryParse(num, out esnum);
        }
    }
}
