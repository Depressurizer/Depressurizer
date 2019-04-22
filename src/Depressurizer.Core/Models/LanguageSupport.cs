using System.Collections.Generic;
using JetBrains.Annotations;

namespace Depressurizer.Core.Models
{
    public sealed class LanguageSupport
    {
        #region Fields

        private List<string> _fullAudio;

        private List<string> _interface;

        private List<string> _subtitles;

        #endregion

        #region Public Properties

        /// <summary>
        ///     List containing supported audio languages.
        /// </summary>
        [NotNull]
        public List<string> FullAudio
        {
            get => _fullAudio ?? (_fullAudio = new List<string>());
            set => _fullAudio = value;
        }

        /// <summary>
        ///     List containing supported interface languages.
        /// </summary>
        [NotNull]
        public List<string> Interface
        {
            get => _interface ?? (_interface = new List<string>());
            set => _interface = value;
        }

        /// <summary>
        ///     List containing supported subtitle languages.
        /// </summary>
        [NotNull]
        public List<string> Subtitles
        {
            get => _subtitles ?? (_subtitles = new List<string>());
            set => _subtitles = value;
        }

        #endregion
    }
}
