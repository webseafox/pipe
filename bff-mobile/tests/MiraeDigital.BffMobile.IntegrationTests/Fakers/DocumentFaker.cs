using System;

namespace MiraeDigital.BffMobile.IntegrationTests.Fakers
{
    public static class DocumentFaker
    {
        public static long PersonDocument()
        {
            int sum = 0;
            int[] multiple1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiple2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new();
            string seed = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                sum += int.Parse(seed[i].ToString()) * multiple1[i];

            int diff = sum % 11;
            if (diff < 2)
                diff = 0;
            else
                diff = 11 - diff;

            seed += diff;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(seed[i].ToString()) * multiple2[i];

            diff = sum % 11;

            if (diff < 2)
                diff = 0;
            else
                diff = 11 - diff;

            seed += diff;
            return long.Parse(seed);
        }
    }
}
