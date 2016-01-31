using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Курсовая_ТФЯТ
{
    //примеры
    //program var dim er1 , er2 real begin for er1 :=  14 + 87  to 14 + er1 do er1 :=  14 + er2 ; end

    //program var dim er1 , er2 real begin by ( er1 <= er2 )  er1 :=  14 + 78  else  er1 :=  14 + 78  ;  end

    //program var dim er1 , er2 real begin input  er1 , er2 ; end

    //program var dim er1 , er2 real begin output  er1 + 14 ; end

    //program var dim er1 , er2 real begin er1 :=  14 + 78  ; end

    //program var dim er1 , er2 real begin do while ( er1 < er2 ) er1 :=  14 + 78  loop ; end
    /* program var dim er1 , er2 real 
 begin 
    do while (  er1 <= 1 ) 
       begin
            input er1 , er2 ;
            er1 := 14 + er2 ;
            by ( er2 < 0 ) 
                    begin
                           er1 := er1 + 1 ;
                           output er1 + 0 ;
                   end ;
      end 
 loop ;
 end*/
 

    public partial class Form1 : Form
    {
        
        List Id = new List();
        List Const = new List();
        List TableLexAnalysis = new List();
        List LabelList = new List();//таблица меток

        int k = 0;//для массива masTable
        int g = 0;//для перевода в полиз начинаем с begin
        int m = 0;//номер метки
        int numberId=0;//количество известных id
        int countWordPoliz = 0;

        bool flagBegin = false; //для проверки на повтор id
        bool flagOperation = true;// определяет правильность операции
        string resultsPolizString = "";//результирующая срока для полиза
        
       
        public Form1()
        {
            InitializeComponent();
        }

        public void Clear() //чистим форму
        {
            IdBox.Items.Clear();
            ConstBox.Items.Clear();
            AnalysisBox.Items.Clear();
            errorsTextBox.Clear();
            generationBox.Clear();
            Id.delete();
            Const.delete();
            TableLexAnalysis.delete();
            LabelList.delete();
            resultsPolizString = "";
            m = 0;
            k = 0;
            g = 0;
            countWordPoliz = 0;
            numberId = 0;
            flagBegin = false;
            flagOperation = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
            
            if (programString.Text != "")
            {
                string[] words = programString.Text.Split(' ', '\n'); //делим на лексемы и вносим в массив

                if (LexAnalysis(words))
                {
                    if (SintaxAnalysis(TableLexAnalysis.getData(), 0))
                    {
                        errorsTextBox.Text = "Программа успешно выполнена";
                        TranslateToPoliz(TableLexAnalysis.getData());
                        TranslateToGenarationCode();
                   }
                }
            }
         }

        public bool LexAnalysis(string[] words) //функция для раскидывания по таблицам лексем
        {
            try
            {
                for (int j = 0; j < words.Length; j++)
                {
                    string word = words[j];

                    if (word == "")
                        continue;

                    if (word == "\n")
                        continue;

                    if (word == "/*")//пропускаем комментарий
                    {
                        j++;
                        while (words[j] != "*/")j++;
                        continue;
                    }

                    if (isDutyWord(word)) //служебное слово?
                    {
                        if (word == "begin")
                            flagBegin = true;//слово begin уже прошли

                        for (int i = 0; i < DutyWordBox.Items.Count; i++)
                            if (word == Convert.ToString(DutyWordBox.Items[i]))
                                TableLexAnalysis.add(i, 1);
                    }

                    else
                    if (isDevidedWord(word))
                        {
                            for (int i = 0; i < DevidedWordBox.Items.Count; i++)
                                if (word == Convert.ToString(DevidedWordBox.Items[i]))
                                    TableLexAnalysis.add(i, 2);
                        }
                     else
                    if (isDigit(word))//если число
                    {
                        if (!Const.isRepeat(word))
                        {
                            Const.add(word);
                            TableLexAnalysis.add(Const.getPos(), 3);
                        }
                        else
                            TableLexAnalysis.add(Const.findData(word), 3);
                    }

                    else//если идентификатор
                    {
                        if (isId(word))
                           {
                            if (flagBegin == false)//для семантики
                                numberId++;

                            if (!Id.isRepeat(word))
                            {
                                Id.add(word);
                                TableLexAnalysis.add(Id.getPos(), 4);
                            }
                                else
                               TableLexAnalysis.add(Id.findData(word), 4);
                          }
                    else
                    {
                        errorsTextBox.Text = "Ошибка: Неверный идентификатор " + word;
                        return false;
                    }
                }
                }
           
            //вывод
           
                foreach (Node data in Id.getData())
                    IdBox.Items.Add(data.Data);

                 foreach (Node data in Const.getData())
                    ConstBox.Items.Add(data.Data);

                foreach (Node obj in TableLexAnalysis.getData()) 
                    AnalysisBox.Items.Add("(" + obj.table + "," + obj.position + ")");
            }
          catch 
            {
                errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении лексического анализа.";
            }
            
            return true;
        }
        
        bool SintaxAnalysis(Node[] masTableLexAnalysis, int condition) //функция, которая проводит синтаксический анализ
        {
            try
            {
                string word = getWord(masTableLexAnalysis[k]);

                switch (condition)
                {
                    case 0: 
                        if (word == "program")
                                {
                                    condition++;
                                    k++;
                                }
                            else
                                {
                                    errorsTextBox.Text = "Ошибка: пропущено слово program в начале программы";
                                    flagOperation = false;
                                    return false;
                                }
                     break;

                    case 1:
                        if (word == "var")
                            {
                                condition++;
                                k++;
                            }
                        else
                        {
                            errorsTextBox.Text = "Ошибка: пропущено слово var";
                            flagOperation = false;
                            return false;
                        }
                    break;

                    case 2:
                        if (word == "dim")
                            {
                                condition++;
                                k++;
                            }
                        else
                            {
                                errorsTextBox.Text = "Ошибка: описание идентификаторов должно начинаться со слова dim";
                                flagOperation = false;
                                return false;
                            }
                        break;

                    case 3: 
                        if (isId(word))
                            {
                                condition++;
                                k++;
                            }
                        else
                            {
                                errorsTextBox.Text = "Ошибка: неправильный  id" + word;
                                flagOperation = false;
                                return false;
                            }
                        break;
                    //описание
                    case 4: 
                        if (word == ",")
                            {
                                condition--;
                                k++;
                            }
                        else
                        if ((word == "integer") || (word == "real") || (word == "boolean"))
                                {
                                    condition++;
                                    k++;
                                }
                            else
                                {
                                    errorsTextBox.Text = "Ошибка: пропущена , или тип";
                                    flagOperation = false;
                                    return false;
                                }


                     break; //конец описания

                    case 5: 
                        if (word == "begin")
                            {
                                condition++;
                                g = k;
                                k++;
                            }
                        else
                            {
                                errorsTextBox.Text = "Ошибка: пропущено begin";
                                flagOperation = false;
                                return false;
                            }
                        break;
                    case 6: 
                        if (whatOperator(getWord(masTableLexAnalysis[k]), masTableLexAnalysis))//какой оператор?
                            {
                                condition++;
                                k++;
                            }
                        else 
                            { 
                                flagOperation = false; 
                                return false;
                            }
                        break;

                    case 7: 
                        try
                        {
                            if ((word == ";") && (getWord(masTableLexAnalysis[k + 1]) == "end"))
                                    return true;
                            else
                            if (word == ";")
                                {
                                    condition--;
                                    k++;
                                }
                            else
                                {
                                    errorsTextBox.Text += "Ошибка: нет либо ; либо пропущено слово end"+"\n";
                                    flagOperation = false;
                                    return false;
                                }
                            break;
                        }
                        catch
                            {
                                errorsTextBox.Text += "Ошибка:  пропущено слово end в конце программы "+"\n";
                                flagOperation = false;
                                return false;
                            }
                } }
            catch 
            {
                errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа.";
            }

                SintaxAnalysis(masTableLexAnalysis, condition);
                if (flagOperation == true) return true;
                else return false;
           }
       
        void TranslateToPoliz(Node[] masTableLexAnalysis) 
        {
            try
            {
                g++;
                for (int i = g; g < masTableLexAnalysis.Length; g++)//начиная со следующего после begin
                {
                    string word = getWord(masTableLexAnalysis[g]);

                    foreach (string s in IdBox.Items)
                        if (word == s)
                        {
                            word = "id";
                            break;
                        }

                    switch (word)
                    {
                        case ("id"): polizAssigned(masTableLexAnalysis); 
                            break;
                        case ("by"): polizIf(masTableLexAnalysis); 
                            break;
                        case ("for"): polizFor(masTableLexAnalysis);
                            break;
                        case ("do"): polizWhile(masTableLexAnalysis); 
                            break;
                        case ("input"): polizRead(masTableLexAnalysis);
                            break;
                        case ("output"): polizWrite(masTableLexAnalysis); 
                            break;

                    }
                }
            }
            catch
            {
                errorsTextBox.Text = "Возникла неизвестная ошибка при переводе в полиз";
            }
            resultsPolizString += "end";
            polizBox.Text = resultsPolizString;

        }

        string getAdr (string M , Node[] masLabel )
        {
            for (int i = 0; i < masLabel.Length; i++)
            {
                
                if (masLabel[i].M == M)
                    return masLabel[i].adress;
            }
            return "0";
        }

       void TranslateToGenarationCode() 
        {
            try
            {
                int coutntString = 1;//Количество строк в генерации
                int adr = 500;
                string[] poliz = resultsPolizString.Split(' ', '\n');
                Stack stack = new Stack();
                
                Node[] masLabel = LabelList.getData();

                for (int i = 0; i < poliz.Length; i++)
                {
                    
                    string s = poliz[i];
                    
                    if (s == "")
                        continue;
                    else
                        if (isDigit(s))
                        {
                            stack.push(s, 0);
                        }
                        else
                            if (isId(s))
                            {
                               stack.push(s, adr);
                                adr++;
                            }
                            else
                                if (isArifSign(s)) //арифметическая операция
                                {
                                    NodeGen b = stack.popGen();
                                    NodeGen a = stack.popGen();

                                    if (isId(a.value))
                                    {
                                        adr--;
                                        generationBox.AppendText(coutntString+") "+"RD " + a.adr + "\n");
                                        coutntString++;
                                    }
                                    else if (isDigit(a.value))
                                    {
                                        generationBox.AppendText(coutntString + ") " + "RD #" + a.value + "\n");
                                        coutntString++;
                                    }

                                    if (isId(b.value))
                                    {
                                        adr--;
                                        switch (s)
                                        {
                                            case ("-"): generationBox.AppendText(coutntString + ") " + "SUB " + b.adr + "\n"); break;
                                            case ("+"): generationBox.AppendText(coutntString + ") " + "ADD " + b.adr + "\n"); break;
                                            case ("*"): generationBox.AppendText(coutntString + ") " + "MUL " + b.adr + "\n"); break;
                                            case ("/"): generationBox.AppendText(coutntString + ") " + "DIV " + b.adr + "\n"); break;
                                        }
                                        coutntString++;
                                    }
                                    else if (isDigit(b.value))
                                    {
                                        switch (s)
                                        {
                                            case ("-"): generationBox.AppendText(coutntString + ") " + "SUB #" + b.value + "\n"); break;
                                            case ("+"): generationBox.AppendText(coutntString + ") " + "ADD #" + b.value + "\n"); break;
                                            case ("*"): generationBox.AppendText(coutntString + ") " + "MUL #" + b.value + "\n"); break;
                                            case ("/"): generationBox.AppendText(coutntString + ") " + "DIV #" + b.value + "\n"); break;
                                        }
                                        coutntString++;
                                    }
                                    generationBox.AppendText(coutntString + ") " + "WR 700 \n");
                                    coutntString++;
                                }

                                else
                                    if (s == ":=")
                                    {
                                        generationBox.AppendText(coutntString + ") " + "RD 700 \n");
                                        coutntString++;
                                        generationBox.AppendText(coutntString + ") " + "WR " + stack.popGen().adr + "\n");
                                        coutntString++;
                                    }
                                    else
                                        if (isLogSing(s))//смотрим знак
                                        {
                                            NodeGen b = stack.popGen();
                                            NodeGen a = stack.popGen();

                                            if (isId(a.value))
                                            {
                                                adr--;
                                                generationBox.AppendText(coutntString + ") " + "RD " + a.adr + "\n");
                                                coutntString++;
                                            }
                                            else
                                                if (isDigit(a.value))
                                                {
                                                    generationBox.AppendText(coutntString + ") " + "RD #" + a.value + "\n");
                                                    coutntString++;
                                                }
                                            if (isId(b.value))
                                            {
                                                adr--;
                                                generationBox.AppendText(coutntString + ") " + "SUB " + b.adr + "\n");
                                                coutntString++;
                                                switch (s)
                                                {
                                                    case ">": generationBox.AppendText(coutntString + ") " + "JS " + getAdr(poliz[i + 1], masLabel) + "\n"); i += 2; break;
                                                    case "<": generationBox.AppendText(coutntString + ") " + "JNS " + getAdr(poliz[i + 1], masLabel) + "\n"); i += 2; break;
                                                    case "!=": generationBox.AppendText(coutntString + ") " + "JZ " + getAdr(poliz[i + 1], masLabel) + "\n"); i += 2; break;
                                                    case "==": generationBox.AppendText(coutntString + ") " + "JNZ " + getAdr(poliz[i + 1], masLabel) + "\n"); i += 2; break;
                                                    case ">=": 
                                                               generationBox.AppendText(coutntString + ") " + "JS " + getAdr(poliz[i + 1], masLabel) + "\n");
                                                               coutntString++;
                                                               generationBox.AppendText(coutntString + ") " + "JNZ " + getAdr(poliz[i + 1], masLabel) + "\n");
                                                               i += 2;
                                                        break;
                                                    case "<=":
                                                                generationBox.AppendText(coutntString + ") " + "JNS " + getAdr(poliz[i + 1], masLabel) + "\n");
                                                                coutntString++;
                                                                generationBox.AppendText(coutntString + ") " + "JNZ " + getAdr(poliz[i + 1], masLabel) + "\n");
                                                                i += 2;
                                                        break;
                                                }
                                                coutntString++;

                                            }
                                            else
                                                if (isDigit(b.value))
                                                {
                                                    generationBox.AppendText(coutntString + ") " + "SUB #" + b.value + "\n");
                                                    switch (s)
                                                    {
                                                        case ">": generationBox.AppendText(coutntString + ") " + "JS " + getAdr(poliz[i + 1], masLabel) + "\n"); i += 2; break;
                                                        case "<": generationBox.AppendText(coutntString + ") " + "JNS " + getAdr(poliz[i + 1], masLabel) + "\n"); i += 2; break;
                                                        case "!=": generationBox.AppendText(coutntString + ") " + "JZ " + getAdr(poliz[i + 1], masLabel) + "\n"); i += 2; break;
                                                        case "==": generationBox.AppendText(coutntString + ") " + "JNZ " + getAdr(poliz[i + 1], masLabel) + "\n"); i += 2; break;
                                                        case ">=": generationBox.AppendText(coutntString + ") " + "JS " + getAdr(poliz[i + 1], masLabel) + "\n");
                                                                   coutntString++;
                                                                   generationBox.AppendText(coutntString + ") " + "JNZ " + getAdr(poliz[i + 1], masLabel) + "\n"); 
                                                                   i += 2;
                                                                   break;
                                                        case "<=": generationBox.AppendText(coutntString + ") " + "JNS " + getAdr(poliz[i + 1], masLabel) + "\n");
                                                                   coutntString++;
                                                                   generationBox.AppendText(coutntString + ") " + "JNZ " + getAdr(poliz[i + 1], masLabel) + "\n"); 
                                                                    i += 2;
                                                                    break;
                                                    }
                                                    coutntString++;
                                                }
                                        }
                                        else
                                            if (s == "!")
                                            {
                                                generationBox.AppendText(coutntString + ") " + "JMP " + getAdr(poliz[i - 1], masLabel) + "\n");
                                                coutntString++;
                                            }
                                            
                                                else
                                                    if (s == "READ")
                                                    {
                                                        generationBox.AppendText(coutntString + ") " + "WR " + stack.popGen().adr + "\n");
                                                        coutntString++;
                                                        for (int j = 1; j < Convert.ToInt16(poliz[i + 1]); j++)
                                                        {
                                                            generationBox.AppendText(coutntString + ") " + "WR " + stack.popGen().adr + "\n");
                                                            coutntString++;
                                                        }
                                                        i++;
                                                    }
                                                    else
                                                        if (s == "WRITE")
                                                        {
                                                            generationBox.AppendText(coutntString + ") " + "RD 700 \n");
                                                            coutntString++;
                                                        }
                                                        else if (s == "end")
                                                        {
                                                            generationBox.AppendText(coutntString + ") " + "HLT");
                                                            coutntString++;
                                                        }




                }
                
            } 
            catch
            {
                errorsTextBox.Text = "Возникла неизвестная ошибка при генерации кода.";
            }
        }

        void whatOperatorForPoliz(Node[] masTableLexAnalysis)
        {
            string word = getWord(masTableLexAnalysis[g]);
            foreach (string s in IdBox.Items)
                    if (word == s)
                    {
                        word = "id";
                        break;
                    }

                switch (word)
                {
                    case ("id"): polizAssigned(masTableLexAnalysis);
                        break;
                   case ("by"): polizIf(masTableLexAnalysis);
                        break;
                   case ("for"):  polizFor(masTableLexAnalysis);
                        break;
                   case ("do"): polizWhile(masTableLexAnalysis);
                        break;
                   case ("input"): polizRead(masTableLexAnalysis);
                        break;
                   case ("output"): polizWrite(masTableLexAnalysis);
                        break;
                   case ("begin"): polizComplex(masTableLexAnalysis);
                        break;
                    

                }
        }

        bool whatOperator(string word, Node[] TableLexAnalysis)
        {
           

           foreach (string s in IdBox.Items)
           if(word == s)
            {
                 word="id";
                 break;
            }

            switch (word) 
            {
                case ("id"): checkAssigned(TableLexAnalysis, 0); if (flagOperation == true)  return true; 
                    break;
                case ("by"): k++; checkIf(TableLexAnalysis, 0); if ( flagOperation== true) return true;
                    break;
                case ("for"): k++; checkFixLoop(TableLexAnalysis, 0); if (flagOperation == true) return true;
                    break;
                case ("do"): k++; checkIfLoop(TableLexAnalysis, 0); if ( flagOperation== true) return true;
                    break;
                case ("input"): k++; checkIn(TableLexAnalysis, 0); if ( flagOperation== true) return true;
                    break;
                case ("output"): k++; checkOut(TableLexAnalysis, 0); if (flagOperation == true) return true;
                break;
                case ("begin"): k++; checkComplex(TableLexAnalysis, 0); if (flagOperation == true) return true;
                break;
                default: errorsTextBox.Text += "Ошибка : неизвестный оператор" + "\n"; return false;
                    
            }
            return false;
        }

        string getWord (Node Table) 
        {
            int numeberTable=Table.table;
            int pos=Table.position;
            string word="";

            switch (numeberTable)
            {
                case 1: word = Convert.ToString(DutyWordBox.Items[pos]);
                    break;
                case 2: word = Convert.ToString(DevidedWordBox.Items[pos]);
                    break;
                case 3: word = Convert.ToString(ConstBox.Items[pos]);
                    break;
                case 4: word = Convert.ToString(IdBox.Items[pos]);
                    break;
                
            }
            return word;
        }

        bool checkComplex(Node[] masTableLexAnalysis, int condition)
        {
            try
            {
                string word = getWord(masTableLexAnalysis[k]);

                switch (condition)
                {
                    case 0: if (whatOperator(word, masTableLexAnalysis))
                        {
                            condition++;
                            k++;

                        }
                        else
                        {
                            errorsTextBox.Text += "Ошибка: Неверный составной оператор" + "\n";
                            flagOperation = false;
                            return false;

                        }
                        break;
                    case 1:
                        if ((word == ";") && (getWord(masTableLexAnalysis[k + 1]) == "end"))
                        {
                            flagOperation = true;
                            k++;
                            return true;
                        }
                        else
                            if (word == ";")
                            {

                                condition--;
                                k++;

                            }
                            else
                            {
                                errorsTextBox.Text += "Ошибка: В составном операторе пропущено либо ; либо end" + "\n";
                                flagOperation = false;
                                return false;
                            }
                        break;
                }
            }
            catch { errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в составном операторе." + "\n"; }
                checkComplex(masTableLexAnalysis, condition);
                return true;
            
        } 
        
       bool checkAssigned(Node[] masTableLexAnalysis, int condition) //присваивание
       {
           try
           {
               string word = getWord(masTableLexAnalysis[k]);

               switch (condition)
               {
                   case 0:
                       if (isIdFamous(word) == true)
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: неизвестный id " + word + "\n";
                           flagOperation = false;
                           return false;
                       }
                       break;
                   case 1: if (word == ":=")
                       {
                           condition++;
                           k++;

                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: пропущено :=" + "\n";
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 2:
                       isArifTerm(masTableLexAnalysis, 0);
                       if (flagOperation == true)
                       {
                           flagOperation = true;
                           return true;
                       }


                       else
                       {
                           flagOperation = false;
                           return false;
                       }

               }
           }
           catch 
           {
               errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в операторе присваивания." + "\n";
           }
           checkAssigned(masTableLexAnalysis, condition);
           return true;
        }

       bool checkIf(Node[] masTableLexAnalysis, int condition)// условный  переход
       {
           try
           {
               string word = getWord(masTableLexAnalysis[k]);

               switch (condition)
               {
                   case 0:
                       isLogTerm(masTableLexAnalysis, 0);
                       if (flagOperation == true)
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           flagOperation = false;
                           return false;

                       }
                       break;


                   case 1:
                       if (whatOperator(word, masTableLexAnalysis) == true)
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 2:
                       if (getWord(masTableLexAnalysis[k]) == "else")
                       {
                           k++;
                           condition++;

                       }
                       else
                       {
                           k--;
                           flagOperation = true;
                           return true;
                       }
                       break;

                   case 3:
                       if (whatOperator(word, masTableLexAnalysis) == true)
                       {
                           flagOperation = true;
                           return true;
                       }
                       else
                       {
                           k--;
                           errorsTextBox.Text += "Нет оператора после else" + "\n";
                           flagOperation = false;
                           return false;
                       }
               }
           }
           catch { errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в условном операторе." + "\n"; }
            checkIf(masTableLexAnalysis, condition);
           return true;
       }

       bool checkFixLoop(Node[] masTableLexAnalysis, int condition)//фиксироанный цикл
       {
           try
           {
               string word = getWord(masTableLexAnalysis[k]);

               switch (condition)
               {
                   case 0:
                       checkAssigned(masTableLexAnalysis, 0);
                       if (flagOperation == true)
                       {
                           k++;
                           condition++;
                       }
                       else
                           return false;
                       break;

                   case 1: if (word == "to")
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: Пропущено to" + "\n";
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 2:
                       isArifTerm(masTableLexAnalysis, 0);
                       if (flagOperation == true)
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 3: if (word == "do")
                       {
                           k++;
                           condition++;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: Пропущено do" + "\n";
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 4: if (whatOperator(word, masTableLexAnalysis) == true)
                           return true;
                       else
                       {
                           flagOperation = false;
                           return false;
                       }
               }
           }
           catch
           {
               errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в фиксированном цикле." + "\n";
           }
           checkFixLoop(masTableLexAnalysis, condition);
           return true;
            
       }

       bool checkIfLoop(Node[] masTableLexAnalysis, int condition) //условный цикл
       {
           try
           {
               string word = getWord(masTableLexAnalysis[k]);
               switch (condition)
               {
                   case 0:
                       if (word == "while")
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: пропущено while" + "\n";
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 1:
                       isLogTerm(masTableLexAnalysis, 0);
                       if (flagOperation == true)
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           flagOperation = false;
                           return false;
                       }
                       break;
                   case 2:
                       if (whatOperator(word, masTableLexAnalysis) == true)
                       {
                           condition++;
                           k++;

                       }
                       else
                       {
                           flagOperation = false;
                           return false;

                       }
                       break;

                   case 3:
                       if (word == "loop")
                       {
                           flagOperation = true;
                           return true;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: пропущен loop" + "\n";
                           flagOperation = false;
                           return false;
                       }

               }
           } catch 
           {
               errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в условном цикле." + "\n";
           }
           checkIfLoop(masTableLexAnalysis, condition);
           return true;
       }

       bool checkIn(Node[] masTableLexAnalysis, int condition) //ввод
        {
            try
            {
                string word = getWord(masTableLexAnalysis[k]);
                switch (condition)
                {
                    case 0:
                        if (isIdFamous(word) == true)
                        {
                            condition++;
                            k++;
                        }
                        else
                        {
                            errorsTextBox.Text += "Ошибка: неизвестный идентификатор " + word + "\n";
                            flagOperation = false;
                            return false;
                        }
                        break;

                    case 1:
                        if (word == ",")
                        {
                            condition--;
                            k++;
                        }
                        else
                            if (word == ";")
                            {
                                k--;
                                flagOperation = true;
                                return true;
                            }
                            else 
                            {
                                errorsTextBox.Text += "Ошибка: в input пропущена либо , либо ; \n";
                                flagOperation = false;
                                return false;
                            }
                        break;
                }
            }
            catch 
            {
                errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в  операторе ввода." + "\n";
            }
            checkIn(masTableLexAnalysis, condition);
            return true;
        }

       bool checkOut(Node[] masTableLexAnalysis, int condition) //вывод
       {
           try
           {
               string word = getWord(masTableLexAnalysis[k]);
               switch (condition)
               {
                   case 0:
                       isArifTerm(masTableLexAnalysis, 0);
                       if (flagOperation == true)
                       {
                           condition++;
                           k++;
                       }
                       else
                           return false;
                       break;

                   case 1:
                       if (word == ";")
                       {
                           k--;
                           return true;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: пропущена ; в операторе output \n";
                           flagOperation = false;
                           return false;
                       }
                       break;
               }
           }
           catch
           {
               errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в операторе ввода." + "\n";
           }
           checkOut(masTableLexAnalysis, condition);
           return true;
       }

       bool isArifTerm(Node[] masTableLexAnalysis, int condition)//арифметическое выражение
       {
           try
           {
               string word = getWord(masTableLexAnalysis[k]);

               switch (condition)
               {

                   case 0:

                       if ((isIdFamous(word)) || (isDigit(word)))
                       {
                           condition++;
                           k++;

                       }
                       else
                       {
                           if (!isDigit(word) && !isIdFamous(word))
                               errorsTextBox.Text += "Ошибка: неизвестный идентификатор " + word+ "\n";
                           else
                           errorsTextBox.Text += "Ошибка: неверное арифметическое выражение, возможно пропущен идентификатор или константа"+"\n";
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 1:
                       if (isArifSign(word) == true)
                       {
                           condition++;
                           k++;

                       }
                       else
                       {
                           if (!isDigit(word) && !isIdFamous(word))
                               errorsTextBox.Text += "Ошибка: неизвестный идентификатор " + word + "\n";
                           else
                           errorsTextBox.Text += "Ошибка: пропущен знак в арифметическом выражение" + "\n";

                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 2:
                       if ((isIdFamous(word)) || (isDigit(word)))
                       {
                           flagOperation = true;
                           return true;

                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: неверное арифметическое выражение, возможно пропущен идентификатор или константа" + "\n";
                           flagOperation = false;
                           return false;
                       }
               }
           }
           catch
           {
               errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в арифметичеком выражении." + "\n";
           }
           isArifTerm(masTableLexAnalysis, condition);
           return true;
                    }
    
       bool isLogTerm(Node[] masTableLexAnalysis, int condition)
       {
           try
           {
               string word = getWord(masTableLexAnalysis[k]);

               switch (condition)
               {
                   case 0:
                       if (word == "(")
                       {
                           k++;
                           condition++;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: пропущено (";
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 1:
                       if ((isIdFamous(word)) || (isDigit(word)))
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           if (!isDigit(word) && !isIdFamous(word))
                               errorsTextBox.Text += "Ошибка: неизвестный идентификатор " + word + "\n";
                           else
                           errorsTextBox.Text += "Ошибка: неверное логическое выражение, пропущен индентификатор или константа";
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 2:
                       if (isLogSing(word))
                       {
                           condition++;
                           k++;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: пропущен знак в логическом выражении";
                           flagOperation = false;
                           return false;
                       }
                       break;

                   case 3:
                       if ((isIdFamous(word)) || (isDigit(word)))
                       {
                           condition++;
                           k++;

                       }
                       else
                       {
                           if (!isDigit(word) && !isIdFamous(word))
                               errorsTextBox.Text += "Ошибка: неизвестный идентификатор " + word + "\n";
                           else
                           errorsTextBox.Text += "Ошибка: неверное логическое выражение, пропущен индентификатор или константа" + word;
                           flagOperation = false;
                           return false;
                       }

                       break;

                   case 4:
                       if (word == ")")
                       {
                           flagOperation = true;
                           return true;
                       }
                       else
                       {
                           errorsTextBox.Text += "Ошибка: пропущено )";
                           flagOperation = false;
                           return false;
                       }

               }
           }
           catch 
           {
               errorsTextBox.Text += "Возникла неизвестная ошибка при выполнении синтаксического анализа в логическом выражении.";
           }
           isLogTerm(masTableLexAnalysis, condition);
           return true;
       }

        bool isArifSign(string word) //знак
        {
            switch (word) 
            {
                case ("+"): return true; 
                case ("-"): return true;
                case ("/"): return true;
                case ("*"): return true;
                default: return false;
            }
        }

        bool isLogSing(string word) 
        {
            switch (word)
            {
                case ("<"): return true;
                case (">"): return true;
                case ("!="): return true;
                case ("=="): return true;
                case ("<="): return true;
                case (">="): return true;
                default: return false;
            }
        } 

        bool isDigit(string s) //если число
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c)) return false;
            }
            return true;
        }

        bool isDutyWord(string s)//служебное слово
        {
            for (int i = 0; i < DutyWordBox.Items.Count; i++)
                if (s == Convert.ToString(DutyWordBox.Items[i]))
                    return true;
            return false;

        }

        bool isDevidedWord(string s)//разделитель
        {
            for (int i = 0; i < DevidedWordBox.Items.Count; i++)
                if (s == Convert.ToString(DevidedWordBox.Items[i]))
                    return true;
            return false;

        }

        bool isId(string s) //проверка, является ли строка идентификатором
        {
            int i = 1;
            foreach (char c in s)
            {
                if (((i == 1) || (i == 2)) && (!Char.IsLetter(c)))
                    return false;

                if ((i >= 3) && (Char.IsLetter(c)))
                    return false;
                i++;
            }
            return true;

        }

        bool isIdRepeat(string s) //повтор id
        {
                foreach (string s1 in IdBox.Items)
                    if (s == s1)
                        return true;

                return false;
            

        }

        bool isIdFamous(string s) //объявленный ранее id или нет
        {
            for (int i = 0; i < numberId; i++)
                if (s == IdBox.Items[i])
                    return true;
            return false;
        }

        void polizAssigned(Node[] masTableLexAnalysis) 
        {
            try
            {
                resultsPolizString += getWord(masTableLexAnalysis[g]) + " ";//id
                string wordAss = getWord(masTableLexAnalysis[g + 1]) + " ";//:=
                g = g + 2;
                polizArifTerm(masTableLexAnalysis);
                resultsPolizString += wordAss;
                countWordPoliz += 2;
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }
        }

        void polizArifTerm(Node[] masTableLexAnalysis)
        {
            try
            {
                resultsPolizString += getWord(masTableLexAnalysis[g]) + " " + getWord(masTableLexAnalysis[g + 2]) + " " + getWord(masTableLexAnalysis[g + 1]) + " ";
                g = g + 2;
                countWordPoliz += 3;
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }
            }

        void polizLogTerm(Node[] masTableLexAnalysis, int i) 
        {
            try
            {
                resultsPolizString += getWord(masTableLexAnalysis[i + 1]) + " ";
                string wordIf = getWord(masTableLexAnalysis[i + 2]);
                resultsPolizString += getWord(masTableLexAnalysis[i + 3]) + " ";
                resultsPolizString += wordIf + " ";
                g = i + 4;//пропускаем )
                countWordPoliz += 3;
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }
        }

        private int getPriority(string s)//приоритет
        {
            switch (s)
            {
           
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
               default:
                    return 3;
            }
        }

        void polizIf(Node[] masTableLexAnalysis) 
        {
            try
            {
                bool flagElse = false;
                List listM = new List();
                List listA = new List();
                for (int i = g; g < masTableLexAnalysis.Length; g++)
                {
                    string word = getWord(masTableLexAnalysis[g + 1]);

                    if (word == "(")
                    {
                        polizLogTerm(masTableLexAnalysis, g + 1);
                        m++;
                        listM.add("M" + m);
                        resultsPolizString += "M" + m + " !F ";
                        countWordPoliz += 2;
                        g++;
                        whatOperatorForPoliz(masTableLexAnalysis);

                        if (getWord(masTableLexAnalysis[g + 1]) == "else")
                        {
                            listA.add(Convert.ToString(countWordPoliz + 2));
                            flagElse = true;
                        }
                        else
                            listA.add(Convert.ToString(countWordPoliz + 1));
                        g = g - 1;
                    }
                    else
                        if (word == ",")
                        {
                            g += 2;
                            whatOperatorForPoliz(masTableLexAnalysis);
                            g = g - 1;

                        }
                        else
                            if (word == "else")
                            {
                                g += 2;
                                m++;
                                listM.add("M" + m);
                                resultsPolizString += "M" + m + " ! ";
                                countWordPoliz += 2;

                                whatOperatorForPoliz(masTableLexAnalysis);
                                listA.add(Convert.ToString(countWordPoliz - 3));
                                break;
                            }
                            else
                                if (word == ";")
                                    break;

                }
                if (flagElse == true)
                {
                    LabelList.add(listM.pop(), listA.pop());
                    LabelList.add(listM.pop(), listA.pop());
                }
                else
                    LabelList.add(listM.pop(), listA.pop());
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }
        }

        void polizFor(Node[] masTableLexAnalysis)
        {
            try
            {

                List listA = new List();
                Stack listM = new Stack();

                string id = getWord(masTableLexAnalysis[g + 1]);

                for (int i = g; g < masTableLexAnalysis.Length; g++)
                {
                    string word = getWord(masTableLexAnalysis[g]);

                    if (word == "for")
                    {
                        g++;
                        polizAssigned(masTableLexAnalysis);
                    }
                    else
                        if (word == "to")
                        {
                            listA.add(Convert.ToString(countWordPoliz + 1));
                            g++;
                            polizArifTerm(masTableLexAnalysis);
                            m++;

                            listM.push("M" + m);
                            resultsPolizString += "M" + m + " !F ";
                            countWordPoliz += 2;
                        }
                        else
                            if (word == "do")
                            {
                                g++;
                                whatOperatorForPoliz(masTableLexAnalysis);
                                m++;

                                resultsPolizString += id + " " + id + " " + "1 + := " + "M" + m + " ! ";
                                countWordPoliz += 7;
                                listM.push("M" + m);
                            }
                            else
                                if (word == ";")
                                    break;
                }
                listA.add(Convert.ToString(countWordPoliz));
                LabelList.add(listM.pop(), listA.pop());
                LabelList.add(listM.pop(), listA.pop());

            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }
        }

        void polizWhile(Node[] masTableLexAnalysis) 
        {
            try
            {
                List listA = new List();
                Stack listM = new Stack();

                for (int i = g; g < masTableLexAnalysis.Length; g++)
                {
                    string word = getWord(masTableLexAnalysis[g]);

                    if (word == "do")
                    {
                        listA.add(Convert.ToString(countWordPoliz + 1));
                        polizLogTerm(masTableLexAnalysis, g + 2);

                        m++;
                        listM.push("M" + m);
                        resultsPolizString += "M" + m + " !F ";
                        countWordPoliz+=2;

                        g++;
                        whatOperatorForPoliz(masTableLexAnalysis);

                        
                        m++;
                        listM.push("M" + m);
                        resultsPolizString += "M" + m + " ! ";
                        countWordPoliz += 2;
                        listA.add(Convert.ToString(countWordPoliz + 1));
                    }
                    else
                        if (word == "loop")
                        {
                            g++;
                            break;
                        }
                }
                LabelList.add(listM.pop(), listA.pop());
                LabelList.add(listM.pop(), listA.pop());
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }
        }

        void polizRead(Node[] masTableLexAnalysis)
        {
            try
            {
                int count = 0;
                for (int i = g; g < masTableLexAnalysis.Length; g++)
                {
                    string word = getWord(masTableLexAnalysis[g]);

                    if (isId(word))
                    {
                        countWordPoliz++;
                        resultsPolizString += word + " ";
                        count++;
                    }
                    else
                        if (word == ",")
                            continue;
                        else
                            if (word == ";")
                            {
                                g--;
                                break;
                            }

                }
                resultsPolizString += "READ " + count+ " ";
                countWordPoliz++;
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }
        }

        void polizWrite(Node[] masTableLexAnalysis)
        {
            try
            {
                for (int i = g; g < masTableLexAnalysis.Length; g++)
                {
                    string word = getWord(masTableLexAnalysis[g]);

                    if (isId(word) || isDigit(word))
                    {

                        polizArifTerm(masTableLexAnalysis);

                    }

                    else
                        if (word == ";")
                        {
                            g--;
                            break;
                        }

                }
                resultsPolizString += " WRITE ";
                countWordPoliz++;
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }
        }

        void polizComplex(Node[] masTableLexAnalysis) 
        {
            try
            {
                for (int i = g; g < masTableLexAnalysis.Length; g++)
                {
                    string word = getWord(masTableLexAnalysis[g]);

                    if (word == "begin")
                    {
                        g++;
                        whatOperatorForPoliz(masTableLexAnalysis);
                       
                    }
                    else
                        if (word == ";" && getWord(masTableLexAnalysis[g + 1]) == "end")
                        { g++;
                            break;
                        }
                        else
                            if (word == ";")
                            {
                                g++;
                                whatOperatorForPoliz(masTableLexAnalysis);
                               
                                continue;
                            }
                            else
                                
                            {
                                g++;
                                whatOperatorForPoliz(masTableLexAnalysis);
                                
                            }
                }
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при выполнении перевода в полиз"; }

        }

        private void DevidedWordBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
          openFile.FileName.Length > 0)
                {
                    programString.LoadFile(openFile.FileName);
                }
            }
            catch { errorsTextBox.Text = "Возникла неизвестная ошибка при открытии файла с тестом. Убедитесь, что файл имеет расширение .rtf"; }
        }

        

       

    
    }
}
