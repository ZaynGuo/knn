using System.Collections.Generic;
using System.Linq;

namespace KNN
{
    class KNN
    {
        /// <summary>
        /// KNN参数k
        /// </summary>
        private int k;
        public int K
        {
            set
            {
                this.k = value;
            }
        }
        /// <summary>
        /// 训练集
        /// </summary>
        private Obj[] training_set;
        public Obj[] Training_set
        {
            set
            {
                this.training_set = value;
            }
        }

        /// <summary>
        /// 测试样本，可不赋值
        /// 若不赋值，务必使用有参数的主函数
        /// </summary>
        private Obj test_obj;
        public Obj Test_obj
        {
            set
            {
                this.test_obj = value;
            }
        }

        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="training_set"></param>
        /// <param name="test_obj"></param>
        /// <param name="k"></param>
        public KNN(Obj[] training_set, Obj test_obj, int k)
        {
            this.training_set = training_set;
            this.test_obj = test_obj;
            this.k = k;
        }

        /// <summary>
        /// 执行KNN算法，得到测试样本的类别标签
        /// </summary>
        /// <returns></returns>
        public string doKNN()
        {
            this.calcSim();
            //统计k近邻中各类标签的票数
            Dictionary<string, int> class_counter = new Dictionary<string, int>();
            IEnumerable<Obj> query = this.training_set.OrderBy(obj => obj.Sim);
            int counter = 0;
            foreach (Obj obj in query)
            {
                if (counter == k) break;
                if (class_counter.ContainsKey(obj.Class_label))
                {
                    class_counter[obj.Class_label] += 1;
                }
                else
                {
                    class_counter.Add(obj.Class_label, 1);
                }
                counter++;
            }
            //找到票数最多的类标签
            string label = string.Empty;
            int max = 0;
            List<string> keys = new List<string>(class_counter.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                if (class_counter[keys[i]] > max)
                {
                    max = class_counter[keys[i]];
                    label = keys[i];
                }
            }
            return label;
        }

        /// <summary>
        /// 令测试样本与所有训练集计算距离
        /// 这里把距离计算延迟给了属性值对象
        /// </summary>
        private void calcSim()
        {
            for (int i = 0; i < training_set.Length; i++)
            {
                this.training_set[i].Sim = this.training_set[i].Attributes.Sim(test_obj.Attributes);
            }
        }
    }
}
