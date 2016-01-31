using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Курсовая_ТФЯТ
{
    class NodeGen 
    {
        public string value;
        public int adr;
        public NodeGen nextGen;

         public NodeGen(string s,int a )
        {
            value = s;
            adr=a;

        }
    
    }



    class Node1
    {
        public string value;
        public int adr;
        public Node1 next;

        public Node1(string s)
        {
            value = s;
           
        }

    }

    class Stack
    {
        public Node1 head;
        public NodeGen headGen;

        public Stack()
        {
            head = null;
            headGen = null;
        }

        

        public void push(string s)
        {
            Node1 buf = new Node1(s);
            buf.next = head;
            head = buf;
        }

        public void push(string s, int a)//для генерации
        {
            NodeGen buf = new NodeGen(s,a);
            buf.nextGen = headGen;
            headGen = buf;
        }

        public NodeGen popGen()//для генерации
        {
            NodeGen buf = headGen;
            headGen = headGen.nextGen;
            return buf;
        }

        public string pop()
        {
            Node1 buf = head;
            head = head.next;
            return buf.value;
        }

        public string peek()
        {
            return head.value;
        }
    }
}
