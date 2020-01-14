using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.Instrumentals
{
    public class TrackerDataResult
    {
        private readonly Dictionary<Type, Object> map = new Dictionary<Type, Object>();

        public void AddValue(Type type, Object data) 
        {
            map.Add(type, data);
        }

        public List<T> GetList<T>()
        {
            if (!this.map.ContainsKey(typeof(T))) this.map.Add(typeof(T), new List<T>());
            return (List<T>)this.map[typeof(T)];
        }
    }
}
