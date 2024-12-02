using SISBOV;
using System;
using System.Collections.Generic;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SisbovDto sisbov = new SisbovDto
            {
                Inicial = "0105520298615099",
                Final = "0105520298616099",
                BrincosPorCaixa = 100
            };

            List<TabelaSisbov> sisbovNumeroList = GeradorSisbov(sisbov);
            
            foreach (var item in sisbovNumeroList)
            {
                Console.WriteLine(
                    $"Número: {item.Numero}" +
                    $"\nCaixa: {item.Caixa}" +
                    $"\nAtivo: {item.IsActive}" +
                    $"\n---------------------------");
            }
        }

        public static List<TabelaSisbov> GeradorSisbov(SisbovDto input)
        {
            if (!ValidateInput(input))
            {
                Console.WriteLine("Invalid input data. Please check inicial, final numbers, and brincosPorCaixa.");
                return new List<TabelaSisbov>();
            }

            long sisbovInicial = Convert.ToInt64(input.Inicial);
            long sisbovFinal = Convert.ToInt64(input.Final);
            int brincosPorCaixa = input.BrincosPorCaixa.Value;

            List<TabelaSisbov> sisbovNumeroList = new List<TabelaSisbov>();

            int caixaAtual = 1;

            for (long numeroAtual = sisbovInicial; numeroAtual <= sisbovFinal; numeroAtual++)
            {
                for (int i = 0; i < brincosPorCaixa && numeroAtual <= sisbovFinal; i++)
                {
                    string sisbovCompletoParcial = GetFullSisbovString(input.Inicial, numeroAtual);

                    TabelaSisbov sisbovNumero = new TabelaSisbov
                    {
                        Numero = sisbovCompletoParcial,
                        Caixa = caixaAtual.ToString(),
                        IsActive = true
                    };
                    sisbovNumeroList.Add(sisbovNumero);

                    numeroAtual++;
                }
                caixaAtual++;
            }

            return sisbovNumeroList;
        }

        private static bool ValidateInput(SisbovDto input)
        {
            if (string.IsNullOrEmpty(input.Inicial) || string.IsNullOrEmpty(input.Final))
                return false;

            if (Convert.ToInt64(input.Inicial) > Convert.ToInt64(input.Final))
                return false;

            return input.BrincosPorCaixa > 0;
        }

        private static string GetFullSisbovString(string initialNumber, long currentNumber)
        {
            string pais = initialNumber.Substring(0, 3);
            string uf = initialNumber.Substring(3, 2);
            string codigo3Digitos = initialNumber.Substring(5, 3);
            string codigo6Digitos = currentNumber.ToString("D6");

            return pais + uf + codigo3Digitos + codigo6Digitos;
        }
    }
}