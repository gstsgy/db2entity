using DB2Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Util
{
    public class CSharpClass
    {
        public List<string> st = new List<string>();
        public string ty;
        public string name;
        public List<JavaField> ljf = new List<JavaField>();
        public List<JavaMethod> ljm = new List<JavaMethod>();
        public CSharpClass()
        {
        }
        public override string ToString()
        {
            string diff = " ";
            string tab = "    ";
            string ent = "\r\n"+diff;
            string str = "";
            str = FileUtil.ReadTemplate();

            str += diff+"[DataContract]"+ent;
            foreach (string tmp in this.st)
            {
                str += tmp + diff;
            }
            str += ty + diff + name + "{"+ent;
            foreach(JavaField item in ljf)
            {
                str +=diff+diff+ "[DataMember]" + ent;
                str += diff + diff + "public " + item.javaType + diff + item.name + " {set;get;}" + diff + diff + diff + "/*   "+item.annotation+"   */";
            }
            
            str += ent + "}\r\n}";
            return str;
        }
       
    }
}
