using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Interfaces
{
    public interface IRedisObjectKeyProvider
    {
        LazRedisKey ForObject<T>(T? obj, string customKey);
    }
}
