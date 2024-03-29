﻿using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModelResolver
{
    public interface IViewModelResolver
    {
        VM Resolve<VM>(System.Type viewModel, Action<VM> modifier = null) where VM : IViewModel;
        VM Resolve<VM>(Action<VM> modifier = null) where VM : IViewModel;
        VM BuildUp<VM>(VM instance) where VM : IViewModel;
    }
}
