﻿#pragma checksum "..\..\..\show.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "54E4DA92613A3A23A01C3F8A9A0CA60B"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Picturer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Picturer {
    
    
    /// <summary>
    /// Show
    /// </summary>
    public partial class Show : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 5 "..\..\..\show.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Picturer.Show Window;
        
        #line default
        #line hidden
        
        
        #line 6 "..\..\..\show.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\show.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer imageScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\show.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Picturer.GIFImageControl tbShow;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\show.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox randomMode;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\show.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox autoMode;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\show.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TitleText;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\show.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider autoSlider;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Picturer;component/show.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\show.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Window = ((Picturer.Show)(target));
            
            #line 5 "..\..\..\show.xaml"
            this.Window.Unloaded += new System.Windows.RoutedEventHandler(this.Window_Unloaded);
            
            #line default
            #line hidden
            
            #line 5 "..\..\..\show.xaml"
            this.Window.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 5 "..\..\..\show.xaml"
            this.Window.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.Window_MouseWheel);
            
            #line default
            #line hidden
            
            #line 5 "..\..\..\show.xaml"
            this.Window.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            
            #line 5 "..\..\..\show.xaml"
            this.Window.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.imageScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 4:
            this.tbShow = ((Picturer.GIFImageControl)(target));
            return;
            case 5:
            this.randomMode = ((System.Windows.Controls.CheckBox)(target));
            
            #line 10 "..\..\..\show.xaml"
            this.randomMode.Checked += new System.Windows.RoutedEventHandler(this.randomMode_Checked);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\show.xaml"
            this.randomMode.Unchecked += new System.Windows.RoutedEventHandler(this.randomMode_Unchecked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.autoMode = ((System.Windows.Controls.CheckBox)(target));
            
            #line 11 "..\..\..\show.xaml"
            this.autoMode.Checked += new System.Windows.RoutedEventHandler(this.autoMode_Checked);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\show.xaml"
            this.autoMode.Unchecked += new System.Windows.RoutedEventHandler(this.autoMode_Unchecked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TitleText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.autoSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 13 "..\..\..\show.xaml"
            this.autoSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.autoSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

