using System;
using System.Collections.Generic;

namespace DividerApp
{
    public static class StringDivider
    {
        /// <summary>
        /// Разбивает строку на блоки заданной длины.
        /// Если длина строки не кратна blockLength — последний блок
        /// дополняется нулевыми символами '\0'.
        /// </summary>
        public static List<string> Divider(string str, int blockLength)
        {
            // Проверка на null
            if (str == null)
                throw new ArgumentNullException(nameof(str), "Строка не может быть null.");

            // Проверка на некорректную длину блока
            if (blockLength <= 0)
                throw new ArgumentException("Длина блока должна быть больше нуля.", nameof(blockLength));

            List<string> Blocks = new List<string>(str.Length / blockLength + 1);

            for (int i = 0; i < str.Length; i += blockLength)
            {
                if (str.Length - i > blockLength)
                {
                    Blocks.Add(str.Substring(i, blockLength));
                }
                else
                {
                    Blocks.Add(str.Substring(i, str.Length - i)
                        + new string('\0', blockLength - (str.Length - i)));
                }
            }

            return Blocks;
        }
    }
}