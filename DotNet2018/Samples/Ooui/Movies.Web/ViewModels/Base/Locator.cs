using System;
using  Movies.Web.Services.Movies;
using  Movies.Web.Services.People;
using  Movies.Web.Services.Request;
using  Movies.Web.Services.TVShow;
using Autofac;
using Movies.Web.Services.Navigation;

namespace  Movies.Web.ViewModels.Base
{
    public class Locator
    {
        private static IContainer _container;

        private static readonly Locator _instance = new Locator();

        public static Locator Instance
        {
            get
            {
                return _instance;
            }
        }

        protected Locator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<RequestService>().As<IRequestService>();
            builder.RegisterType<MoviesService>().As<IMoviesService>();
            builder.RegisterType<PeopleService>().As<IPeopleService>();
            builder.RegisterType<TVShowService>().As<ITVShowService>();

            builder.RegisterType<DetailViewModel>();
            builder.RegisterType<HomeViewModel>();

            if (_container != null)
            {
                _container.Dispose();
            }

            _container = builder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}