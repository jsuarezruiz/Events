using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Materials;
using XamarinForms3DCarSample.Components;
using XamarinForms3DCarSampleXamarinForms.Models;

namespace XamarinForms3DCarSample.Helpers
{
    public static class WaveEngineFacade
    {
        private static List<Entity> _cameras;
        private static MyScene _scene;

        public static event EventHandler<EventArgs> Initialized;
        public static event EventHandler<EventArgs> AnimationCompleted;

        public static void Initialize(MyScene scene)
        {
            _scene = scene;
            InitializeCameras();
            SetActiveCamera(AppSettings.DefaultCamera);
        }
        public static void SetActiveCamera(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex > AppSettings.NumberOfCameras)
            {
                return;
            }

            // Change camera position
            var activeCamera = _cameras[cameraIndex];
            var animationCameraComponent = _scene.EntityManager.Find(AppSettings.CameraName)?.
                FindComponent<AnimationCameraComponent>();

            if (activeCamera != null)
            {
                animationCameraComponent.MoveTo(activeCamera);
                animationCameraComponent.AnimationCompleted += OnAnimationCompleted;
            }
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

        private static void InitializeCameras()
        {
            if (_cameras != null && _cameras.Any())
            {
                return;
            }

            _cameras = new List<Entity>();


            var camera1 = _scene.EntityManager.Find("camera1Entity");
            _cameras.Add(camera1);

            var camera2 = _scene.EntityManager.Find("camera2Entity");
            _cameras.Add(camera2);

            var camera3 = _scene.EntityManager.Find("camera3Entity");
            _cameras.Add(camera3);

            Initialized?.Invoke(null, new EventArgs());
        }

        private static void OnAnimationCompleted(object sender, EventArgs e)
        {
            var component = sender as AnimationCameraComponent;
            component.AnimationCompleted -= OnAnimationCompleted;
            AnimationCompleted?.Invoke(sender, e);
        }
    }
}