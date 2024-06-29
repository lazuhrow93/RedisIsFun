// See https://aka.ms/new-console-template for more information
using RedisCourseRU102N.ConnectingAndPing;
using RedisCourseRU102N.Controller;
using StackExchange.Redis;

Console.WriteLine("Hello, World!");

var newConfig = new ConfigurationOptions()
{
    EndPoints = { SensitiveInformation.GetConnectionString() },
    Password = SensitiveInformation.GetPassword()
};


var pinger = new Executor(newConfig);
pinger.RunPingApp();