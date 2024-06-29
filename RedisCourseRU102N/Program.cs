// See https://aka.ms/new-console-template for more information
using RedisCourseRU102N.ConnectingAndPing;
using RedisCourseRU102N.Controller;
using StackExchange.Redis;

Console.WriteLine("Hello, World!");

var newConfig = new ConfigurationOptions()
{
    EndPoints = {"redis-13915.c53.west-us.azure.redns.redis-cloud.com:13915"},
    Password = "uU0PMXsltK1CYaQiJay0Oser106obXY2",
};


var pinger = new Executor(newConfig);
pinger.RunPingApp();