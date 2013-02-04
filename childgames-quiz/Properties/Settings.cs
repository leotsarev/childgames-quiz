namespace ChildGamesQuiz.Properties
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented",
        Justification = "Reviewed. Suppression is OK here.")]
    internal sealed partial class Settings
    {
        // public Settings() {
        //    // // To add event handlers for saving and changing settings, uncomment the lines below:
        //    //
        //    // this.SettingChanging += this.SettingChangingEventHandler;
        //    //
        //    // this.SettingsSaving += this.SettingsSavingEventHandler;
        //    //
        // }

        // private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
        //    // Add code to handle the SettingChangingEvent event here.
        // }

        // private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
        //    // Add code to handle the SettingsSaving event here.
        // }
        public TimeSpan Timeout()
        {
            return new TimeSpan(0, TimeoutMinutes, 0);
        }
    }
}