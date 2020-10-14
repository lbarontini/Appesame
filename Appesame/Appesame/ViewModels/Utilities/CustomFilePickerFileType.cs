using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Appesame.ViewModels.Utilities
{
    public class CustomFilePickerFileType : FilePickerFileType
    {
        public static readonly FilePickerFileType Pdf = AndroidPdfFileType();
        public static readonly FilePickerFileType Audio = AndroidAudioFileType();
        public static FilePickerFileType AndroidPdfFileType() =>
            new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "application/pdf" } }
            });
        private static FilePickerFileType AndroidAudioFileType() =>
            new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "audio/*" } }
            });
    }
}
