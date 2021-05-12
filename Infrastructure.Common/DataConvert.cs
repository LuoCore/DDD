using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Infrastructure.Common
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/6 11:27:20
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public static class DataConvert
    {



        public static T StringToEnum<T>(this string str)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), str);
            }
            catch (Exception)
            {

               return default(T);
            }
           
        }
        public static T IntToEnum<T>(this int value)
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value); 
            }
            catch (Exception)
            {

                return default(T);
            }
          
        }

        public static string EnumToString<T>(this T value)
        {
            try
            {
                return Enum.GetName(typeof(T), value);
            }
            catch (Exception)
            {

                return string.Empty;
            }
           
        }

        public static int EnumToInt<T>(this T value)where T:Enum
        {
            try
            {
                return (int)Enum.ToObject(typeof(T), value);
            }
            catch (Exception)
            {

                return -1;
            }

        }



        /*
         * string TO guid
         */
        public static Guid StringToGuid(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) { return Guid.Empty; }
            Match m = Regex.Match(str, @"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", RegexOptions.IgnoreCase);
            Guid gv = new Guid();
            if (m.Success)
            {
                gv = new Guid(str);
            }
            if (gv != Guid.Empty)
            {
                return gv;
            }
            else
            {
                return gv;
            }
        }


        public static int ObjToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        public static Int64 ObjToLong(this object thisValue)
        {
            long reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && Int64.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        public static int ObjToInt(this object thisValue, int errorValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static double ObjToMoney(this object thisValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }

        public static double ObjToMoney(this object thisValue, double errorValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static string ObjToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }

        public static string ObjToString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return errorValue;
        }

        public static Decimal ObjToDecimal(this object thisValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }

        public static Decimal ObjToDecimal(this object thisValue, decimal errorValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static DateTime ObjToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }
            return reval;
        }

        public static DateTime ObjToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static bool ObjToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }



        /// <summary>  
        /// 将对象属性转换为字典 
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        public static Dictionary<String, String> ObjToDictionary(this Object o)
        {
            Dictionary<String, String> map = new Dictionary<String, String>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new Object[] { }).ObjToString());
                }
            }

            return map;

        }

        #region IList 转DataTable

        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, prop.PropertyType);

            return table;
        }

        /// <summary>
        /// IList 转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(this IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item);
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTable(this IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }


        /// <summary>
        ///  List<DataRow>转 Model List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<T> ListDataRowToList<T>(this IList<DataRow> rows)
        {
            IList<T> list = null;
            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = DataRowToModel<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// DataTable 转 IList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(this DataTable table)
        {
            if (table == null)
                return null;

            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                rows.Add(row);

            return ListDataRowToList<T>(rows);
        }
        /// <summary>
        /// DataRow 转 Model 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T DataRowToModel<T>(this DataRow row)
        {
            string columnName;
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    columnName = column.ColumnName;
                    PropertyInfo prop = obj.GetType().GetProperty(columnName);
                    try
                    {
                        object value = (row[columnName].GetType() == typeof(DBNull))
                        ? null : row[columnName];
                        //非泛型
                        if (!prop.PropertyType.IsGenericType)
                            value = string.IsNullOrEmpty(value + "") ? null : Convert.ChangeType(value, prop.PropertyType);
                        else//泛型
                        {
                            Type genericTypeDefinition = prop.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                value = string.IsNullOrEmpty(value + "") ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(prop.PropertyType));
                            }
                        }
                        if (prop.CanWrite)    //判断其是否可写
                            prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return obj;
        }

        #endregion






        /// <summary>
        /// Bitmap转Byte数组
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] BitmapToByte(this Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
        /// <summary>
        /// 获取全拼
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFull(this string str)
        {
            string PYstr = "";
            foreach (char item in str.ToCharArray())
            {
                if (ChineseChar.IsValidChar(item))
                {
                    ChineseChar cc = new ChineseChar(item);
                    PYstr += cc.Pinyins[0].Substring(0, cc.Pinyins[0].Length - 1);
                }
                else
                {
                    PYstr += item.ToString();
                }
            }
            return PYstr;
        }
        /// <summary>
        /// 返回字符串的简拼
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GetSimple(this string inputTxt)
        {
            string shortR = "";
            foreach (char c in inputTxt.Trim())
            {
                ChineseChar chineseChar = new ChineseChar(c);
                shortR += chineseChar.Pinyins[0].Substring(0, 1).ToArray();
            }
            return shortR.ToUpper();

        }
        /// <summary>
        /// 获取首字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFrist(this string str)
        {
            string PYstr = "";
            foreach (char item in str.ToCharArray())
            {
                if (ChineseChar.IsValidChar(item))
                {
                    ChineseChar cc = new ChineseChar(item);
                    PYstr += cc.Pinyins[0][0];
                }
                else
                {
                    PYstr += item.ToString()[0];
                }
                break;
            }
            return PYstr;
        }
    }
}
