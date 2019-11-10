﻿using Caliburn.Micro;
using Common.FlashMessages;
using Common.ViewModelResolver;
using Public.Views;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace CmTest
{
    public class Bootstrapper : BootstrapperBase
    {
        static Mutex mutex = new Mutex(false, "34515d3d-cdda-4d87-aa0c-eeaab04ba20a");


        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }


        protected override void Configure()
        {
            var config = new TypeMappingConfiguration()
            {
                DefaultSubNamespaceForViews = "Public.Views",
                DefaultSubNamespaceForViewModels = "Public.Views"
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);


            // -----


            _container = new SimpleContainer();
            _container.Instance(_container);


            // Common
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IFlashMessagesManager, FlashMessagesManager>();
            _container.Singleton<IViewModelResolver, ViewModelResolver>();


            // Services


            // Factories

            // Facades


            // Windows
            _container.Singleton<MainWindowViewModel>();


            // ViewModels


            // Subscribers
        }


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            /*if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false)) {
                System.Windows.Application.Current.Shutdown();
            }*/

            try
            {

                var vm = _container.GetInstance<MainWindowViewModel>();
                _container.BuildUp(vm);
                _container.GetInstance<IWindowManager>().ShowWindowAsync(vm);

                /*} catch (StorageError ex) {
                    ro = new ResultObject<object>(false);
                    ro.AddMessage("Nelze načíst Vaše data.");*/

            }
            catch (Exception ex)
            {
                //ro = new ResultObject<object>(false);
                //ro.AddMessage("Při spouštění aplikace došlo k neočekávané chybě");
            }

            //if (!ro.Success) {// todo
            /*StartupErrorWindowViewModel errw = _container.GetInstance<StartupErrorWindowViewModel>();
            errw.Text = ro.GetLastMessage().Text;
            _container.GetInstance<IWindowManager>().ShowDialog(errw);*/
            //}


            // Subscribers must be instantiated beforehand
            // but after all dependencies are set /created
            PrepareSubscribers();
        }


        private void PrepareSubscribers()
        {
        }


        protected override void OnExit(object sender, EventArgs e)
        {
            //mutex.ReleaseMutex();
        }


        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {
                GetType().Assembly,
                typeof(MainWindowView).Assembly
            };
        }


        protected override IEnumerable<object> GetAllInstances(System.Type service)
        {
            return _container.GetAllInstances(service);
        }


        protected override object GetInstance(System.Type service, string key)
        {
            return _container.GetInstance(service, key);
        }


        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}