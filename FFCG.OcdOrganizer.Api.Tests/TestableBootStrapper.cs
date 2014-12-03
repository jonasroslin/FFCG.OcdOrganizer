using System;
using System.Collections.Generic;
using Moq;
using Nancy;
using Nancy.TinyIoc;

namespace FFCG.OcdOrganizer.Api.Tests
{
    public class TestableBootStrapper : DefaultNancyBootstrapper
    {
        private readonly Dictionary<Type, Type> _register = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Object> _mocks = new Dictionary<Type, Object>();

        public void Register<T, T2>()
        {
            _register.Add(typeof(T), typeof(T2));
        }

        public Mock<T> RegisterMock<T>() where T : class
        {
            var mock = new Mock<T>();
            _mocks.Add(typeof(T), mock.Object);
            return mock;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            foreach (var reg in _register)
                container.Register(reg.Key, reg.Value);

            foreach (var mock in _mocks)
                container.Register(mock.Key, mock.Value);
        }
    }
}