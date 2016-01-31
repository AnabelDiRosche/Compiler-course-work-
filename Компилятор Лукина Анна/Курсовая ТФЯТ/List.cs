using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Курсовая_ТФЯТ
{
    public class Node
    {
        public int table;
        public string Data;
        public int position;
        public string M;
        public string adress;
        public Node next;


        public Node(string s)
        {
            Data = s;
        }

        public Node(int pos, int tab)//для анализатора
        {

            position = pos;
            table = tab;
        }

        public Node(string Label, string Adr)//для меток
        {
            M = Label;
            adress = Adr;
        }


    }

    public class List
    { 
        public Node head;
        public Node end;
        public int position;
        public int size; //количество элементво в таблице
    
        public List()
        {
            head=null;
            end=null;
            position=-1;
            size=0;
        }

        public void add(string s)
        {
             Node buf = new Node(s);
                buf.next = null;
                if (head == null)
                    head = end = buf;
                else
                    end.next = buf;
                end = buf;
               position++;
               size++;
        }

        public void add(string M, string A)
        {
            Node buf = new Node(M,A);
            buf.next = null;
            if (head == null)
                head = end = buf;
            else
                end.next = buf;
            end = buf;
            position++;
            size++;
        }

        public void add(int pos, int tab)//для анализатора
        {
            Node buf = new Node(pos, tab);
            buf.next = null;
            if (head == null)
                head = end = buf;
            else
                end.next = buf;
            end = buf;
            size++;
        }

        public Node[] getData ()//получить массив данных
        {
            Node buf = head;
            Node[] masBuf = new Node[size];
            int i = 0;
            while (buf != null)
            {
                masBuf[i] = buf;
                buf = buf.next;
                i++;
             }
            return masBuf;
        }

        public string pop()
        {
            Node buf = head;
            head = head.next;
            return buf.Data;
        }
        
        public void delete()
        {
            head = null;
            end = null;
            size = 0;
            position = -1;
        }
        
        public int findData(string s) //поиск позиции ID
        {
            Node buf = head;
            int i = -1;
            while (buf.Data != s) 
            {
                buf = buf.next;
                i++;
            }
            return ++i;
        }

        public string findPos(int i) 
        {
            Node buf = head;
            while (buf.position != i)
                buf = buf.next;
            return buf.Data;
        }
        
        public bool isRepeat(string s)//проверка на повтор
        {
            Node buf = head;

            while (buf != null)
            {
                if (buf.Data == s)
                    return true;
                else buf = buf.next;
              
            }
            return false;
        }

        public int getPos()
        {
            return position;
        }
    }
    



     
}
