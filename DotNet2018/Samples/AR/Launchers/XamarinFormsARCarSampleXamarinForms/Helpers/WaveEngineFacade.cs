using System;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Materials;
using XamarinFormsARCarSampleXamarinForms.Models;

namespace XamarinFormsARCarSample.Helpers
{
    public static class WaveEngineFacade
    {
        private static MyScene _scene;

        public static event EventHandler<EventArgs> Initialized;

        public static void Initialize(MyScene scene)
        {
            _scene = scene;

            Initialized?.Invoke(null, new EventArgs());
        }

        public static ScreenContextManager GetScreenContextManager()
        {
            return WaveServices.ScreenContextManager;
        }

        public static Scene GetCurrentScene()
        {
            if (WaveServices.ScreenContextManager.CurrentContext.Count == 0)
                return null;

            return WaveServices.ScreenContextManager.CurrentContext[0];
        }

        public static void UpdateColor(CustomColor color)
        {
            var entity = _scene.EntityManager.Find("car.Plane_036.Plane_041");
            var materialComponent = entity.FindComponent<MaterialComponent>();
            var hex = color.Hex;
            ((StandardMaterial)materialComponent.Material).DiffuseColor = FromHex(hex);
        }

        private static WaveEngine.Common.Graphics.Color FromHex(string color)
        {
            if (color.IndexOf("#") != -1)
                color = color.Replace("#", "");

            float R = int.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255;
            float G = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255;
            float B = int.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255;

            return new WaveEngine.Common.Graphics.Color(R, G, B, 1.0f);
        }
    }
}