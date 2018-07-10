using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.Common
{
    public class RedisHelper
    {
        public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
        public static IRedisClient redisClent = clientManager.GetClient();
        //入队
        public static void Enqueue(string name, string value)
        {
            redisClent.EnqueueItemOnList(name, value);
        }
        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Dequeue(string name)
        {
            return redisClent.DequeueItemFromList(name);
        }
        /// <summary>
        /// 获得队列中数据的数量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetEqueueCount(string name)
        {
            return redisClent.GetListCount(name);
        }
    }
}
