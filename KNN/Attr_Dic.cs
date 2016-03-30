using System;
using System.Collections.Generic;

namespace KNN
{
    class Attr_Dic : IAttribute
    {
        /// <summary>
        /// 存放向量的字典
        /// </summary>
        private Dictionary<string, object> values;
        public Dictionary<string, object> Values
        {
            get
            {
                return this.values;
            }
        }

        /// <summary>
        /// 构建函数，奇数位置为键，偶数位置为值，需成对出现
        /// </summary>
        /// <param name="para"></param>
        public Attr_Dic(params object[] para)
        {
            values = new Dictionary<string, object>();
            bool ifOdd = false;
            string key = string.Empty;
            try
            {
                for (int i = 0; i < para.Length; i++)
                {
                    if (!ifOdd)
                    {
                        key = (string)para[i];
                    }
                    else
                    {
                        this.values.Add(key, para[i]);
                    }
                }
            }
            catch (Exception e)
            {
                //对参数的要求较高
                throw new Exception("请输入正确的初始化参数" + e.Message);
            }
        }

        /// <summary>
        /// 利用词典计算高维稀疏数据属性间的相似度，可以通过重写更换公式
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public virtual double Sim(IAttribute attr)
        {
            Attr_Dic attr_dic = (Attr_Dic)attr;
            //TODO: 完成余弦相似度计算
            return 0;
        }
    }
}
