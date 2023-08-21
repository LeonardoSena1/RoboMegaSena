using OpenQA.Selenium.Chrome;
using prmToolkit.Selenium;

namespace RoboMegaSena
{
    /// <summary>
    /// Verificar a versão do google chrome instaldo https://chromedriver.chromium.org/downloads/version-selection
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            List<int> NumberMegaSena = new List<int>();
            string AccumulatedNext = string.Empty;
            string Accumulated = string.Empty;

            var webDriver = new ChromeDriver(Environment.CurrentDirectory + "\\DriveGoogle");

            try
            {
                webDriver.Manage().Window.Maximize();
                webDriver.LoadPage(TimeSpan.FromSeconds(100), "https://loterias.caixa.gov.br/Paginas/Mega-Sena.aspx");

                Task.Delay(1000).Wait();

                int QtdNumber = Convert.ToInt32(webDriver.ExecuteJavaScript("return document.querySelector(\"#ulDezenas\").getElementsByTagName('li').length"));

                for (int i = 0; i < QtdNumber; i++)
                    NumberMegaSena.Add(Convert.ToInt32(webDriver.ExecuteJavaScript($"return document.querySelector(\"#ulDezenas\").getElementsByTagName('li')[{i}].innerHTML")));

                AccumulatedNext = webDriver.ExecuteJavaScript("return document.querySelector(\"#wp_resultados > div.content-section.section-text.with-box.column-left.no-margin-top > div > div > div.totals > p:nth-child(2) > span.value.ng-binding\").innerHTML").ToString();

                Accumulated = webDriver.ExecuteJavaScript("return document.querySelector(\"#wp_resultados > div.content-section.section-text.with-box.column-left.no-margin-top > div > div > div.totals > p:nth-child(3) > span.value.ng-binding\").innerHTML").ToString();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("===========================================================");

                Console.WriteLine($"Os números são: {string.Join(',', NumberMegaSena)}");
                Console.WriteLine("Acumulado próximo concurso " + AccumulatedNext);
                Console.WriteLine("Acumulado para Sorteio Especial Mega da Virada " + Accumulated);

                Console.WriteLine("===========================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                webDriver.Close();
                webDriver.Dispose();
            }
        }
    }
}