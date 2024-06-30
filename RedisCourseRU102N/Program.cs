// See https://aka.ms/new-console-template for more information
using RedisCourseRU102N;
using RedisCourseRU102N.ConnectingAndPing;
using RedisCourseRU102N.Controller;
using StackExchange.Redis;

Console.WriteLine("Hello, World!");

var newConfig = new ConfigurationOptions()
{
    EndPoints = { SensitiveInformation.GetConnectionString() }, //local class not in repo
    Password = SensitiveInformation.GetPassword()
};


var pinger = new AAppController(newConfig);
//pinger.RunPingApp();
pinger.RunBasicGetSetStringWithTTL();