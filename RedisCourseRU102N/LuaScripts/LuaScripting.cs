
using Providers.ScriptProviderImplementations;
using RedisCourseRU102N.Controller;

namespace RedisCourseRU102N.LuaScripts
{
    public class LuaScripting
    {
        private IRedisCommandExecutor _redisCommandExecutor;

        public LuaScripting(IRedisCommandExecutor commandExecutor)
        {
            _redisCommandExecutor = commandExecutor;
        }

        public void RunBasicSetScriptApp()
        {
            var basicScript = ScriptProvider.BasicSetStringCommand();

            _redisCommandExecutor.PrepareScript(basicScript);

            var resultOfScript = _redisCommandExecutor.EvaluateScript(new ScriptProvider.BasicSetStringCommandVals("autoIncrement", "A String Value1"));
            var resultOfScript2 = _redisCommandExecutor.EvaluateScript(new ScriptProvider.BasicSetStringCommandVals("autoIncrement", "A String Value2"));

            Console.WriteLine($"First key: {resultOfScript}");
            Console.WriteLine($"Second key: {resultOfScript2}");
        }
    }
}
