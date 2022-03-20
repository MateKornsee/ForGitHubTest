using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace forgithubtest
{
    class Program
    {
        static void HosszuFeladat(CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine("Fut(ezt 'enter' zárja be)");
                Thread.Sleep(500);
                if (token.IsCancellationRequested)
                {
                    //ha megszakítás érkezik, akkor kilépünk.
                    break;
                }
                //ez is megszakít, de kivétellel:
                //token.ThrowIfCancellationRequested();
            }
        }
        static void MasikHosszuFeladat(CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine("Fut(ezt 'a' karakter zárja be)");
                Thread.Sleep(500);
                if (token.IsCancellationRequested)
                {
                    //ha megszakítás érkezik, akkor kilépünk.
                    break;
                }
                //ez is megszakít, de kivétellel:
                //token.ThrowIfCancellationRequested();
            }
        }

        static void Main(string[] args)
        
        {
                var tokenSource = new CancellationTokenSource();
                var tokenSourcea = new CancellationTokenSource();
                var t = new Task(() => HosszuFeladat(tokenSource.Token));
                var a = new Task(() => MasikHosszuFeladat(tokenSourcea.Token));
                a.Start();
                t.Start();
            bool x=false; bool y=false;
                while (true)
                {
                    var consoleKey = Console.ReadKey();
                    if (consoleKey.Key == ConsoleKey.Enter)
                    {
                        //megszakítjuk a HosszuFeladat futását
                        tokenSource.Cancel();
                        //2mp várakozás, hogy lássuk a kimenetet
                        Thread.Sleep(2000);
                    x = true;
                    //Kilép a fő szál is
                    }
                    if (consoleKey.Key == ConsoleKey.A)
                    {
                        //megszakítjuk a HosszuFeladat futását
                        tokenSourcea.Cancel();
                        //2mp várakozás, hogy lássuk a kimenetet
                        Thread.Sleep(2000);
                    y = true;
                    //Kilép a fő szál is
                    }
                    if (x ==true && y == true)
                    {
                    break;
                    }
                }           
        }
    }
}
