
namespace KNN
{
    class Obj
    {
        /// <summary>
        /// 样本ID
        /// </summary>
        private string id;
        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        /// <summary>
        /// 样本类标签
        /// </summary>
        private string class_label;
        public string Class_label
        {
            get
            {
                return this.class_label;
            }
            set
            {
                this.class_label = value;
            }
        }

        /// <summary>
        /// 样本属性
        /// </summary>
        private IAttribute attributes;
        public IAttribute Attributes
        {
            get
            {
                return this.attributes;
            }
            set
            {
                this.attributes = value;
            }
        }

        /// <summary>
        /// 记录与其他样本间的距离
        /// </summary>
        private double sim;
        public double Sim
        {
            get
            {
                return this.sim;
            }
            set
            {
                this.sim = value;
            }
        }
    }
}
