using DB2Entity.Util;
using DB2Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Entity.Entity.ProgramEntity
{
    public class CSharpEntity
    {
        /// <summary>
        /// 类修饰符
        /// </summary>
        public List<string> AccessModifier { get; set; } = new List<string>();

        /// <summary>
        /// class关键字
        /// </summary>
		public string ClassKeyword { get; set; } = "class";

        /// <summary>
        /// 类名称
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// 类字段
        /// </summary>
		public List<FieldEntity> Fields { get; set; } = new List<FieldEntity>();

        /// <summary>
        /// 类方法
        /// </summary>
		public List<MethodEntity> Methods { get; set; } = new List<MethodEntity>();
       

        /// <summary>
        /// 重写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            
            str.Append( FileUtil.ReadTemplate());

            str.Append(StrUtil.Separator + "[DataContract]" + StrUtil.NewlineCharacter);
            
            foreach (string tmp in this.AccessModifier)
            {
                str.Append(tmp + StrUtil.Separator);
            }

            str.Append(this.ClassKeyword + StrUtil.Separator + this.Name + "{" + StrUtil.NewlineCharacter);
           
            foreach(FieldEntity item in this.Fields)
            {
                str.Append(StrUtil.NewlineCharacter);
                str.Append(StrUtil.Separator + StrUtil.Separator + "/// <summary>" + StrUtil.NewlineCharacter);
                str.Append(StrUtil.Separator + StrUtil.Separator + "/// " + item.Annotation+ StrUtil.NewlineCharacter);
                str.Append(StrUtil.Separator + StrUtil.Separator + "/// <summary>" + StrUtil.NewlineCharacter);
                str.Append(StrUtil.Separator + StrUtil.Separator + "[DataMember]" + StrUtil.NewlineCharacter);
                str.Append(StrUtil.Separator + StrUtil.Separator + "public " + item.DataType + StrUtil.Separator + StrUtil.InitUpper( item.Name) + " {get;set;}");
                str.Append(StrUtil.NewlineCharacter);


            }
            str.Append(StrUtil.NewlineCharacter + "}\r\n}");
           
            return str.ToString();
        }
       
    }
}
