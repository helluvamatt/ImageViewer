﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageViewer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfInt xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <int>10</int>
  <int>25</int>
  <int>50</int>
  <int>75</int>
  <int>100</int>
  <int>110</int>
  <int>125</int>
  <int>150</int>
  <int>200</int>
  <int>400</int>
  <int>600</int>
  <int>800</int>
</ArrayOfInt>")]
        public int[] ZoomLevels {
            get {
                return ((int[])(this["ZoomLevels"]));
            }
            set {
                this["ZoomLevels"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection LibraryPaths {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["LibraryPaths"]));
            }
            set {
                this["LibraryPaths"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool LibraryAutoScan {
            get {
                return ((bool)(this["LibraryAutoScan"]));
            }
            set {
                this["LibraryAutoScan"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool LibraryBrowserShowFolders {
            get {
                return ((bool)(this["LibraryBrowserShowFolders"]));
            }
            set {
                this["LibraryBrowserShowFolders"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool LibraryBrowserSyncNav {
            get {
                return ((bool)(this["LibraryBrowserSyncNav"]));
            }
            set {
                this["LibraryBrowserSyncNav"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool FullscreenAutoPlay {
            get {
                return ((bool)(this["FullscreenAutoPlay"]));
            }
            set {
                this["FullscreenAutoPlay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5000")]
        public int FullscreenAutoPlayTimeout {
            get {
                return ((int)(this["FullscreenAutoPlayTimeout"]));
            }
            set {
                this["FullscreenAutoPlayTimeout"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color FullscreenBackColor {
            get {
                return ((global::System.Drawing.Color)(this["FullscreenBackColor"]));
            }
            set {
                this["FullscreenBackColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LibraryFullScan {
            get {
                return ((bool)(this["LibraryFullScan"]));
            }
            set {
                this["LibraryFullScan"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LibraryBrowserDrawImageBorder {
            get {
                return ((bool)(this["LibraryBrowserDrawImageBorder"]));
            }
            set {
                this["LibraryBrowserDrawImageBorder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color LibraryBrowserImageBorderColor {
            get {
                return ((global::System.Drawing.Color)(this["LibraryBrowserImageBorderColor"]));
            }
            set {
                this["LibraryBrowserImageBorderColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("White")]
        public global::System.Drawing.Color LibraryBrowserImageBackColor {
            get {
                return ((global::System.Drawing.Color)(this["LibraryBrowserImageBackColor"]));
            }
            set {
                this["LibraryBrowserImageBackColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public global::ImageViewer.Controls.ViewMode LibraryBrowserViewMode {
            get {
                return ((global::ImageViewer.Controls.ViewMode)(this["LibraryBrowserViewMode"]));
            }
            set {
                this["LibraryBrowserViewMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("64")]
        public int LibraryBrowserIconSize {
            get {
                return ((int)(this["LibraryBrowserIconSize"]));
            }
            set {
                this["LibraryBrowserIconSize"] = value;
            }
        }
    }
}
