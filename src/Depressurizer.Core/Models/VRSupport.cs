using System.Collections.Generic;

namespace Depressurizer.Core.Models
{
    public sealed class VRSupport
    {
        #region Fields

        private List<string> _headsets;

        private List<string> _input;

        private List<string> _playArea;

        #endregion

        #region Public Properties

        /// <summary>
        ///     List containing all supported virtual reality headsets.
        /// </summary>
        public List<string> Headsets
        {
            get => _headsets ?? (_headsets = new List<string>());
            set => _headsets = value;
        }

        /// <summary>
        ///     List containing all supported input devices.
        /// </summary>
        public List<string> Input
        {
            get => _input ?? (_input = new List<string>());
            set => _input = value;
        }

        /// <summary>
        ///     List containing all supported play area's.
        /// </summary>
        public List<string> PlayArea
        {
            get => _playArea ?? (_playArea = new List<string>());
            set => _playArea = value;
        }

        #endregion
    }
}
