using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgresssDialog.Wpf.Model
{
    /// <summary>
    /// Represents progress information for a process.
    /// </summary>
    public class ProgressInfo
    {
        /// <summary>
        /// Gets the name of the process.
        /// </summary>
        public string ProcessName { get;set; } = string.Empty;

        /// <summary>
        /// Gets the current value (numerator) of the progress.
        /// </summary>
        public int Numerator { get; set; } = 0;

        /// <summary>
        /// Gets the maximum value (denominator) of the progress.
        /// </summary>
        public int Denominator { get; set; } = 0;

        /// <summary>
        /// Gets the progress percentage (0-100). Returns 0 if Denominator is 0.
        /// </summary>
        public int Progress => (0 != Denominator) ? (int)(Numerator * 100.0 / Denominator) : 0;

        /// <summary>
        /// Gets a value indicating whether the process should continue.
        /// </summary>
        public bool ShouldContinue { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressInfo"/> class by copying values from another instance.
        /// </summary>
        /// <param name="other">The <see cref="ProgressInfo"/> instance to copy from.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="other"/> is null.</exception>
        public ProgressInfo(ProgressInfo other)
        {
            if (null == other)
        {
            throw new ArgumentNullException(nameof(other));
        }

            ProcessName = other.ProcessName;
            Numerator = other.Numerator;
            Denominator = other.Denominator;
            ShouldContinue = other.ShouldContinue;
        }
    }
}
