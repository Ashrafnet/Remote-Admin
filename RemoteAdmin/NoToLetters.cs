using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteAdmin
{
    class NoToLetters
    
    {
        /// <summary>
        /// ÊİŞíØ ÑŞã æÊÍæíá ÃÑŞÇãå Åáì ÍÑæİ
        /// </summary>
        /// <param name="Number">ÇáÑŞã ÇáãÑÇÏ ÊİŞíØå</param>
        /// <param name="Currency">áíÑÉ Ãæ ÏíäÇÑ Ãæ ÏæáÇÑ</param>
        /// <param name="Currencys">áíÑÇÊ Ãæ ÏäÇäíÑ Ãæ ÏæáÇÑÇÊ</param>
        /// <returns>äÊíÌÉ ÇáÊİŞíØ</returns>
        public static string ConvertToLetters(decimal Number, string Currency, string Currencys)
        {
            if (Number > 999999999999 || Number < 0)
                throw new OverflowException("áÇ íãßä ÅÏÎÇá ÑŞã ÃßÈÑ ãä '999,999,999,999' Ãæ ÃÕÛÑ ãä ÇáÕİÑ.");

            string[] Result = new string[4];
            string S1 = "", S2 = "";

            string strNumber = Convert.ToInt64(Number).ToString();
            if ((int)(strNumber.Length / 3) != (decimal)strNumber.Length / 3)
                strNumber = strNumber.PadLeft(((int)(strNumber.Length / 3) + 1) * 3, '0');

            for (int i = 0; i < strNumber.Length; i += 3)
            {
                if (i / 3 == 0) { S1 = Currency; S2 = Currencys; }
                if (i / 3 == 1) { S1 = "Ãáİ"; S2 = "ÂáÇİ"; }
                if (i / 3 == 2) { S1 = "ãáíæä"; S2 = "ãáÇííä"; }
                if (i / 3 == 3) { S1 = "ãáíÇÑ"; S2 = "ãáíÇÑÇÊ"; }
                string strCurNumber = strNumber.Substring(strNumber.Length - 3 - i, 3);
                if (Convert.ToInt32(strCurNumber) > 0) Result[i / 3] = LettersFrom3Numbers(Convert.ToInt32(strCurNumber.ToString()), S1, S2);
            }

            string Results = string.Empty;
            for (int i = Result.Length - 1; i >= 0; i--)
                if (Result[i] != null)
                    if (Result[i].Length > 0)
                        if (Results.Length > 0)
                            Results += " æ" + Result[i];
                        else
                            Results += Result[i];

            //ÍÓÇÈ ÇáİæÇÕá
            S1 = Currency;
            S2 = Currencys;

            if (Number.ToString().IndexOf('.') > 0)
                strNumber = Number.ToString().Substring(Number.ToString().IndexOf('.'), Number.ToString().Length - Number.ToString().IndexOf('.')).PadRight(3, '0');
            if (strNumber == ".00") return Results;
            string strDecimal = LettersFromDecimal(Convert.ToDecimal(strNumber.ToString()), S1);

            if (Results.Length > 0 && strDecimal != null)
                Results += " æ" + strDecimal;
            else
                Results += strDecimal;

            return Results;
        }

        private static string LettersFromDecimal(decimal Number, string S1)
        {
            if (Number * 100 == 1)
                return "ÌÒÁ İí ÇáãÆÉ" + ((S1 != null) ? " ãä Çá" + S1 : null);
            else if (Number * 100 == 2)
                return "ÌÒÁÇä İí ÇáãÆÉ" + ((S1 != null) ? " ãä Çá" + S1 : null);
            else if (Number * 100 >= 3 && Number * 100 <= 10)
                return LettersFromNumber(Convert.ToInt32(Number * 100)) + " ÃÌÒÇÁ İí ÇáãÆÉ" + ((S1 != null) ? " ãä Çá" + S1 : null);
            else if (Number * 100 > 10 && Number * 100 <= 99)
                return LettersFromNumber(Convert.ToInt32(Number * 100)) + " ÌÒÁÇ İí ÇáãÆÉ" + ((S1 != null) ? " ãä Çá" + S1 : null);
            else
                return null;
        }

        private static string LettersFrom3Numbers(int Number, string S1, string S2)
        {
            string Result = string.Empty;

            int N1 = Convert.ToInt32(Number.ToString().PadLeft(3, '0').Substring(0, 1));
            int N2 = Convert.ToInt32(Number.ToString().PadLeft(3, '0').Substring(1, 2));

            string Letter1 = null, Letter2 = null;
            if (N1 > 0) Letter1 = LettersFromNumber(N1 * 100);
            if (N1 == 2 && N2 == 0) Letter1 = Letter1.Remove(Letter1.Length - 1) + " ";
            Letter2 = LettersFromNumber(N2);

            Result += Letter1;
            if (Letter1 != null && Letter2 != null) Result += " æ";
            if (N2 >= 3) Result += Letter2;
            if (N2 <= 1 || N2 > 10) Result += " " + S1;
            else if (N2 == 2) Result += " " + S1.Replace('É', 'Ê') + "Çä";
            else if (N2 >= 3 || N2 <= 10) Result += " " + S2;

            return Result;
        }

        private static string LettersFromNumber(int Number)
        {
            switch (Number)
            {
                //case 0: return "ÕİÑ";
                case 1: return "æÇÍÏ";
                case 2: return "ÇËäÇä";
                case 3: return "ËáÇËÉ";
                case 4: return "ÃÑÈÚÉ";
                case 5: return "ÎãÓÉ";
                case 6: return "ÓÊÉ";
                case 7: return "ÓÈÚÉ";
                case 8: return "ËãÇäíÉ";
                case 9: return "ÊÓÚÉ";
                case 10: return "ÚÔÑÉ";
                case 11: return "ÃÍÏ ÚÔÑ";
                case 12: return "ÅËäÇ ÚÔÑ";
                default:
                    if (Number >= 13 && Number <= 99)
                    {
                        int N1 = Convert.ToInt32(Number.ToString()[0].ToString());
                        int N2 = Convert.ToInt32(Number.ToString()[1].ToString());
                        string S = null;
                        if (N2 != 0) S = " æ";
                        switch (N1)
                        {
                            case 1: return LettersFromNumber(N2) + " ÚÔÑ";
                            case 2: return LettersFromNumber(N2) + S + "ÚÔÑæä";
                            case 3: return LettersFromNumber(N2) + S + "ËáÇËæä";
                            case 4: return LettersFromNumber(N2) + S + "ÃÑÈÚæä";
                            case 5: return LettersFromNumber(N2) + S + "ÎãÓæä";
                            case 6: return LettersFromNumber(N2) + S + "ÓÊæä";
                            case 7: return LettersFromNumber(N2) + S + "ÓÈÚæä";
                            case 8: return LettersFromNumber(N2) + S + "ËãÇäæä";
                            case 9: return LettersFromNumber(N2) + S + "ÊÓÚæä";
                            default: return null;
                        }
                    }
                    else if (Number >= 100 && Number <= 900)
                    {
                        int N = Convert.ToInt32(Number.ToString()[0].ToString());
                        switch (N)
                        {
                            case 1: return " ãÆÉ";
                            case 2: return " ãÆÊÇä";
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9: return LettersFromNumber(N).Remove(LettersFromNumber(N).Length - 1) + "ãÆÉ";
                            default: return null;
                        }
                    }
                    else return null;
                    break;
            }
        }
    }
}
