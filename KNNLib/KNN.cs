using System.Collections.Generic;
using System.Linq;
using System;

namespace KNNLib
{
    public class KNN
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
        private Obj[] test_pic;
        public Obj[] Test_obj
        {
            set
            {
                this.test_pic = value;
            }
        }

        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="training_set_pic"></param>
        /// <param name="test_obj"></param>
        /// <param name="k"></param>
        public KNN(ref Obj[] training_set_pic, ref Obj[] test_obj, int k)
        {
            this.training_set = training_set_pic;
            this.test_pic = test_obj;
            this.k = k;
        }

        /// <summary>
        /// 执行KNN算法，得到测试样本的类别标签
        /// </summary>
        /// <returns></returns>
        public double KNNCluster()
        {
            double [ ]n =new double [training_set.Length];
            int i = 0;
            for (int j = 0; j < test_pic.Length; j++)
            {
                this.SimCalc(test_pic[j]);
                Obj[] query = this.training_set.OrderBy(obj => obj.Sim).ToArray();
                n[i] = query[query.Length-1].Sim;/////一张图片和41张图片比较，取出这41张图片的最大差值
                i++;
            }
             n = n.OrderBy(x => x).ToArray();/////将测试物体的41张图片的最大差值比较，再次取出最大值
            return n[n.Length-1];
        }

       

        /// <summary>
        /// 令测试样本与所有训练集计算距离
        /// 这里把距离计算延迟给了属性值对象
        /// </summary>
        private void SimCalc(Obj obj)
        {
            for (int i = 0; i < training_set.Length; i++)
            {
                this.training_set[i].Sim = this.training_set[i].Attributes.Sim(obj.Attributes);
            }
        }
    }
}
