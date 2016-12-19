using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteAdmin
{
    class NoToLetters
    
    {
        /// <summary>
        /// ����� ��� ������ ������ ��� ����
        /// </summary>
        /// <param name="Number">����� ������ ������</param>
        /// <param name="Currency">���� �� ����� �� �����</param>
        /// <param name="Currencys">����� �� ������ �� �������</param>
        /// <returns>����� �������</returns>
        public static string ConvertToLetters(decimal Number, string Currency, string Currencys)
        {
            if (Number > 999999999999 || Number < 0)
                throw new OverflowException("�� ���� ����� ��� ���� �� '999,999,999,999' �� ���� �� �����.");

            string[] Result = new string[4];
            string S1 = "", S2 = "";

            string strNumber = Convert.ToInt64(Number).ToString();
            if ((int)(strNumber.Length / 3) != (decimal)strNumber.Length / 3)
                strNumber = strNumber.PadLeft(((int)(strNumber.Length / 3) + 1) * 3, '0');

            for (int i = 0; i < strNumber.Length; i += 3)
            {
                if (i / 3 == 0) { S1 = Currency; S2 = Currencys; }
                if (i / 3 == 1) { S1 = "���"; S2 = "����"; }
                if (i / 3 == 2) { S1 = "�����"; S2 = "������"; }
                if (i / 3 == 3) { S1 = "�����"; S2 = "�������"; }
                string strCurNumber = strNumber.Substring(strNumber.Length - 3 - i, 3);
                if (Convert.ToInt32(strCurNumber) > 0) Result[i / 3] = LettersFrom3Numbers(Convert.ToInt32(strCurNumber.ToString()), S1, S2);
            }

            string Results = string.Empty;
            for (int i = Result.Length - 1; i >= 0; i--)
                if (Result[i] != null)
                    if (Result[i].Length > 0)
                        if (Results.Length > 0)
                            Results += " �" + Result[i];
                        else
                            Results += Result[i];

            //���� �������
            S1 = Currency;
            S2 = Currencys;

            if (Number.ToString().IndexOf('.') > 0)
                strNumber = Number.ToString().Substring(Number.ToString().IndexOf('.'), Number.ToString().Length - Number.ToString().IndexOf('.')).PadRight(3, '0');
            if (strNumber == ".00") return Results;
            string strDecimal = LettersFromDecimal(Convert.ToDecimal(strNumber.ToString()), S1);

            if (Results.Length > 0 && strDecimal != null)
                Results += " �" + strDecimal;
            else
                Results += strDecimal;

            return Results;
        }

        private static string LettersFromDecimal(decimal Number, string S1)
        {
            if (Number * 100 == 1)
                return "��� �� �����" + ((S1 != null) ? " �� ��" + S1 : null);
            else if (Number * 100 == 2)
                return "����� �� �����" + ((S1 != null) ? " �� ��" + S1 : null);
            else if (Number * 100 >= 3 && Number * 100 <= 10)
                return LettersFromNumber(Convert.ToInt32(Number * 100)) + " ����� �� �����" + ((S1 != null) ? " �� ��" + S1 : null);
            else if (Number * 100 > 10 && Number * 100 <= 99)
                return LettersFromNumber(Convert.ToInt32(Number * 100)) + " ���� �� �����" + ((S1 != null) ? " �� ��" + S1 : null);
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
            if (Letter1 != null && Letter2 != null) Result += " �";
            if (N2 >= 3) Result += Letter2;
            if (N2 <= 1 || N2 > 10) Result += " " + S1;
            else if (N2 == 2) Result += " " + S1.Replace('�', '�') + "��";
            else if (N2 >= 3 || N2 <= 10) Result += " " + S2;

            return Result;
        }

        private static string LettersFromNumber(int Number)
        {
            switch (Number)
            {
                //case 0: return "���";
                case 1: return "����";
                case 2: return "�����";
                case 3: return "�����";
                case 4: return "�����";
                case 5: return "����";
                case 6: return "���";
                case 7: return "����";
                case 8: return "������";
                case 9: return "����";
                case 10: return "����";
                case 11: return "��� ���";
                case 12: return "���� ���";
                default:
                    if (Number >= 13 && Number <= 99)
                    {
                        int N1 = Convert.ToInt32(Number.ToString()[0].ToString());
                        int N2 = Convert.ToInt32(Number.ToString()[1].ToString());
                        string S = null;
                        if (N2 != 0) S = " �";
                        switch (N1)
                        {
                            case 1: return LettersFromNumber(N2) + " ���";
                            case 2: return LettersFromNumber(N2) + S + "�����";
                            case 3: return LettersFromNumber(N2) + S + "������";
                            case 4: return LettersFromNumber(N2) + S + "������";
                            case 5: return LettersFromNumber(N2) + S + "�����";
                            case 6: return LettersFromNumber(N2) + S + "����";
                            case 7: return LettersFromNumber(N2) + S + "�����";
                            case 8: return LettersFromNumber(N2) + S + "������";
                            case 9: return LettersFromNumber(N2) + S + "�����";
                            default: return null;
                        }
                    }
                    else if (Number >= 100 && Number <= 900)
                    {
                        int N = Convert.ToInt32(Number.ToString()[0].ToString());
                        switch (N)
                        {
                            case 1: return " ���";
                            case 2: return " �����";
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9: return LettersFromNumber(N).Remove(LettersFromNumber(N).Length - 1) + "���";
                            default: return null;
                        }
                    }
                    else return null;
                    break;
            }
        }
    }
}
