using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ServiceStation.WebUI.ReactConfig), "Configure")]

namespace ServiceStation.WebUI
{
	public static class ReactConfig
	{
		public static void Configure()
		{
            JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
            JsEngineSwitcher.Current.EngineFactories.AddV8();
        }
	}
}