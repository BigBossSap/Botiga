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
            Console.BackgroundColor = ConsoleColor.DarkBlue;  
            
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
            while (num != "9")
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
                            Console.WriteLine();
                            Console.Write(FormatMenu("Nom del producte: "));
                            string producte = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write(FormatMenu("Preu(00,00€): "));                          
                            string preuProd = (Console.ReadLine());
                            //while ((preuProd<0||preuProd>double.MaxValue) && ValidadorNum(preuProd){ }                          
                            while (!double.TryParse(preuProd, out double preu) || preu < 0)
                            {
                                Console.WriteLine(FormatMenu("Preu Invalid. Torna a intentar: "));
                                Console.Write(FormatMenu("Preu(00,00€): "));
                                preuProd = Console.ReadLine();
                            }

                            double preuProdBo =Convert.ToDouble(preuProd);
                            AfegirProducte(productes, preus, ref nElementsBotiga, producte, preuProdBo);
                            Console.WriteLine();
                            Console.Write(FormatMenu("Vols afegir mes productes s/n"));
                            opt = Console.ReadLine();
                        }
                        break;
                    case "2":
                        ModificarPreu(productes, preus, ref nElementsBotiga);
                        ContadorRetorn();
                        break;
                    case "3":
                        ModificarProducte(productes,  nElementsBotiga);
                        ContadorRetorn();
                        break;
                    case "4":
                        MostrarTaulesBotiga(productes, preus, nElementsBotiga);
                        ContadorRetorn();
                        break;
                    case "5":
                        AmpliarBotiga(ref productes, ref preus);
                        break;
                    case "6":
                       preus= SortArray(preus, 0, preus.Length-1);
                        break;
                    case "7":
                        MostrarTaulesCompra(productes, preus, nElementsBotiga);
                        Console.WriteLine("Producte a comprar: ");
                        string producteComprar = Console.ReadLine();
                        if ((posValorBuscar(productesCistella, nelementsCistella, producteComprar) != -1))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Producte ja comprat, modifica quantitat");
                            nelementsCistella--;
                        }
                        Console.WriteLine();
                        Console.WriteLine("Quantitat: ");
                        string quantitatComprar = Console.ReadLine();
                        while (!quantitatComprar.All(char.IsDigit) || (int.Parse(quantitatComprar) < 0))
                        {
                            Console.WriteLine("Quantitat incorrecta, torna a intentar: ");
                            Console.Write("Quantitat: ");
                            quantitatComprar = Console.ReadLine();
                        }
                        int quantitatComprarBo = int.Parse(quantitatComprar);
                        AfegirCistella(preus, preusCistella, productes, productesCistella,quantitat, producteComprar, nElementsBotiga, ref nelementsCistella, quantitatComprarBo, diners);
                        ContadorRetorn();
                        break;
                    case "8":
                        MostrarTaulesCistella(productesCistella, quantitat,preusCistella, nelementsCistella, ref diners);
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
            string ampliar = Console.ReadLine();
            while (!ampliar.All(char.IsDigit) || (int.Parse(ampliar) < 0))
            {
                Console.WriteLine("Num Invalid. Torna a intentar: ");
                Console.Write("Capacitat a ampliar: ");
                ampliar = (Console.ReadLine());
            }
            int ampliarValid = Convert.ToInt32(ampliar);

            string[] copiaProd = new string[prod.Length+ampliarValid];
            for (int i=0; i<prod.Length;i++)
            {
                copiaProd[i] = prod[i];
            }
            prod = copiaProd;
            double[] copiaPreu = new double[preu.Length + ampliarValid];
            for (int j = 0; j < preu.Length; j++)
            {
                copiaPreu[j] = preu[j];
            }
            preu = copiaPreu;
        }
        static void ModificarPreu(string[]prod, double[]preu, ref int nElements)
        {

            if (nElements == 0)
            {
                Console.WriteLine();
                Console.WriteLine(FormatMenu("No hi ha productes a la botiga"));
                
            }
            else
            {
                MostrarTaulesCompra(prod, preu, nElements);
                Console.WriteLine();
                Console.Write(FormatMenu("Producte a modificar: "));
                string producteBuscar = Console.ReadLine();
                Console.WriteLine();
                Console.Write(FormatMenu("Nou preu: "));
                string nouPreu = Console.ReadLine();
                while (!double.TryParse(nouPreu, out double preuValid) || preuValid < 0)
                {
                    Console.WriteLine(FormatMenu("Preu Invalid. Torna a intentar: "));
                    Console.Write(FormatMenu("Preu(00,00€): "));
                    nouPreu = Console.ReadLine();
                }

                double nouPreuValid = double.Parse(nouPreu);
                int pos = posValorBuscar(prod, nElements, producteBuscar);
                preu[pos] = nouPreuValid;
            }
        }
        static void ModificarProducte(string[]prod, int nElements)
        {

            if (nElements == 0)
            {
                Console.WriteLine();
                Console.WriteLine(FormatMenu("No hi ha productes a la botiga"));
                ContadorRetorn();
            }
            else
            {
                MostrarTaulesCompra(prod, nElements);
                Console.WriteLine();

                Console.WriteLine(FormatMenu("Producte a modificar: "));
                string producteMod = Console.ReadLine();
                for (int i = 0; i < nElements; i++)
                {
                    if (producteMod == prod[i])
                    {
                        Console.WriteLine(FormatMenu("Nom nou: "));
                        prod[i] = Console.ReadLine();
                    }
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
                taulaProd += FormatMenu($"En l'index {i} trobem el producte <{prod[i]}> a un preu de {Convert.ToString(preus[i])} euros\n\n");
            }
            Console.WriteLine();
            Console.WriteLine(taulaProd);
            Console.WriteLine();
            Console.WriteLine(FormatMenu($"Tenim un total de {nElements} productes a la botiga"));
        }
        static void MostrarTaulesCompra(string[] prod, double[] preus, int nElements)
        {
            string taulaProd = "";
            for (int i = 0; i < nElements; i++)
            {
                taulaProd += $"<{prod[i]}> a un preu de {Convert.ToString(preus[i])}€\n\n";
            }
            Console.WriteLine(taulaProd);
            Console.WriteLine();
            Console.WriteLine($"Tenim un total de {nElements} productes a la botiga");
        }

        static void MostrarTaulesCompra(string[] prod, int nElements)
        {
            string taulaProd = "";
            for (int i = 0; i < nElements; i++)
            {
                taulaProd += FormatMenu($"{i} <{prod[i]}>\n");
            }
            Console.WriteLine(taulaProd);
            Console.WriteLine();
            Console.WriteLine(FormatMenu($"Tenim un total de {nElements} productes a la botiga"));
        }
        static void MostrarTaules(string[] prod, double[] preus)
        {
            string taulaProd = "";
            for (int i = 0; i < prod.Length; i++)
            {
                taulaProd += $"<{prod[i]}> a un preu de {Convert.ToString(preus[i])} euros\n\n";
            }
            Console.WriteLine(taulaProd);
            Console.WriteLine();          
        }
        static void MostrarTaulesCistella(string[] prod, double[] quantitat, double[]preusCistella, double nElementsBotiga, ref double diners)
        {
            string taulaProd = "";
            double total = 0;
            for (int i = 0; i < nElementsBotiga; i++)
            {
                Console.WriteLine();
                taulaProd += FormatMenu($"<{prod[i]}> preu unitari: {preusCistella[i]}€ x {quantitat[i]} = {calcularTotal(quantitat,preusCistella,i, ref diners)}€ \n\n");
                total += calcularTotal(quantitat, preusCistella, i, ref diners);
            }        
            Console.WriteLine(taulaProd);
            Console.WriteLine();
            Console.WriteLine(FormatMenu($"Total: {Math.Round(total,2)}€ "));
            Console.WriteLine(FormatMenu($"Diners restants: {diners}€"));
        }
        static double calcularTotal(double[] quantitat, double[] preuscistella, int i, ref double diners)
        {
            double total = quantitat[i] * preuscistella[i];
            diners -= total;
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
        static void AfegirCistella(double[]preus, double[]preusCistella, string[]productesBotiga, string[] productesCistella, double[] quantitatCistella, string producteComprat, int nElements, ref int nElementsCistella, int quantitat, double diners)
        {
            if (diners > 0)
            {
                int pos = posValorBuscar(productesBotiga, nElements, producteComprat);

                
                if (pos != -1)
                {
                    productesCistella[pos] = producteComprat;
                    quantitatCistella[pos] = quantitat;
                    preusCistella[pos] = preus[pos];
                    nElementsCistella++;
                    Console.WriteLine(FormatMenu($"{diners}€ restants"));
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }

            else
                Console.WriteLine("no Funds");
        }
        static string Menu()
        {
            Console.Clear();
            string[] menu = {
            ">1. Afegir producte\n",
            ">2. Modificar el preu  d'un producte\n",
            ">3. Modificar un producte\n",
            ">4. Mostrar taules\n",
            ">5. Ampliar capacitat de la botiga\n",
            ">6. Quicksort\n",
            " \n",
            " CISTELLA",
            " \n",
            ">7. Afegir productes a la cistella.\n",
            ">8. Mostrar cistella i total\n",
            " \n",          
            ">9. SORTIR",

            };               
            Console.WriteLine(new String('*', Console.WindowWidth));
            Console.WriteLine();                                                                            
            Console.WriteLine(@"
                ██████╗  ██████╗ ████████╗██╗ ██████╗  █████╗ 
                ██╔══██╗██╔═══██╗╚══██╔══╝██║██╔════╝ ██╔══██╗
                ██████╔╝██║   ██║   ██║   ██║██║  ███╗███████║
                ██╔══██╗██║   ██║   ██║   ██║██║   ██║██╔══██║
                ██████╔╝╚██████╔╝   ██║   ██║╚██████╔╝██║  ██║
                ╚═════╝  ╚═════╝    ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═╝
                                              
");
            foreach (string opcio in menu)
            {
                Console.ForegroundColor = ConsoleColor.White;
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
            Console.WriteLine(FormatMenu("Presiona qualsevol tecla per continuar..."));
            Console.ReadKey();
            for (int i = 3; i >= 1; i--)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(FormatMenu($"Tornant al menu en {i} segons..."));
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
            text = new string(' ', ((Console.WindowWidth - (text.Length)) / 2))+ text;
            return text;
        }

        static bool ValidadorNum(string num)
        {
            int esnum;
            return Int32.TryParse(num, out esnum);
        }
    }
}
