﻿using RedisCourseRU102N.ConnectingAndPing;
using RedisCourseRU102N.Controller;
using RedisCourseRU102N.List;
using RedisCourseRU102N.Pipeline;
using RedisCourseRU102N.Providers;
using RedisCourseRU102N.Sets;
using RedisCourseRU102N.Strings;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N
{
    public class AAppController
    {
        private IRedisCommandExecutor _redisCommandExecutor;

        public AAppController(ConfigurationOptions options)
        {
            _redisCommandExecutor = new RedisCommandExecutor(options);    
        }

        public void RunPingApp()
        {
            new Pinger(_redisCommandExecutor).Ping();
        }

        public async Task RunUnpipelinedApp()
        {
            await new Pipeliner(_redisCommandExecutor).UnpipelinedApp();
        }

        public async Task RunPipelinedApp()
        {
            await new Pipeliner(_redisCommandExecutor).ImplicitPipelined();
        }

        public async Task RunExplicitPipelineApp()
        {
            await new Pipeliner(_redisCommandExecutor).ExplicitPipeline();
        }

        public void RunBasicGetSetString()
        {
            new RedisSetterAndGetter(_redisCommandExecutor).BasicSetAndGetString();
        }

        public void RunBasicGetSetStringWithTTL()
        {
            new RedisSetterAndGetter(_redisCommandExecutor).BasicSetAndGetWithTTL();
        }

        public void RunBasicPushLeftAndRightList()
        {
            new PushLeftRight(_redisCommandExecutor).RunPushRightAndLeftApp();
        }

        public void RunEnumerateABasicList()
        {
            new EnumeratingLists(_redisCommandExecutor).EnumeratingTheList();
        }

        public void RunActiveAndInactiveSets()
        {
            new ActiveAndInactive(_redisCommandExecutor, new RedisKeyProvider()).RunSetsApp();
        }
    }
}
