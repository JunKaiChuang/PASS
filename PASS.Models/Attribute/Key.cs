using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Models.Attribute
{
    /// <summary>
    /// 作為資料表中，欄位主鍵的屬性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : System.Attribute
    {
    }

    /// <summary>
    /// 作為條件判斷主要依據(資料表欄位中唯一的值)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DapperKey : System.Attribute
    {
    }

    /// <summary>
    /// 排除於資料表中的欄位
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DapperIgnore : System.Attribute
    {
    }
}
