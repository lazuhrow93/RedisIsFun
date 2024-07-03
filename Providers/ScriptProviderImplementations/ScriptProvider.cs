﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.ScriptProviderImplementations
{
    public static class ScriptProvider
    {
        public static string BasicSetStringCommand()
            => @"
                local id = redis.call('incr', @id_key)
                local key = 'key:' .. id
                redis.call('set', key, @value)
                return key
            ";

        public record BasicSetStringCommandVals(string IdKey, string Value)
        {
            public RedisKey id_key => new RedisKey(IdKey);
            public RedisValue value => new RedisValue(Value);

        }
    }
}