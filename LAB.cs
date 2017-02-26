using System;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Holds a color in CIE L*a*b* format.
	/// </summary>
	/// <remarks>
	/// Because I have attempted to calculate the matrices for conversions from RGB to XYZ, I have noticed that some of my L*a*b* values are slightly different
	/// compared to most converters I have seen online. I'm not sure if my calculations are more accurate or less accurate as a result.
	/// </remarks>
	public struct LAB : IEquatable<LAB>
	{
		/// <summary>Gets the L* value (lightness of the color).</summary>
		public double L { get; }

		/// <summary>Gets the a* value (position between red/magenta and green).</summary>
		public double A { get; }

		/// <summary>Gets the b* value (position between yellow and blue).</summary>
		public double B { get; }

		public LAB(double l, double a, double b)
		{
			this.L = l;
			this.A = a;
			this.B = b;
		}

		/// <summary>The red chromaticity coordinate from ITU-R BT.709.</summary>
		static readonly double[] rxy_chromaticity = { 0.64, 0.33 };
		/// <summary>The green chromaticity coordinate from ITU-R BT.709.</summary>
		static readonly double[] gxy_chromaticity = { 0.30, 0.60 };
		/// <summary>The blue chromaticity coordinate from ITU-R BT.709.</summary>
		static readonly double[] bxy_chromaticity = { 0.15, 0.06 };
		/// <summary>The correlated color temperature for CIE Illuminant D65.</summary>
		const double T = 6500 * 1.4388 / 1.438;
		/// <summary>
		/// The white point chromaticity coordinate for CIE Illuminant D65.
		/// </summary>
		/// <remarks>
		/// The values calculated here are slightly different from the chromaticity coordinate listed here: https://en.wikipedia.org/wiki/Illuminant_D65
		/// </remarks>
		static readonly Lazy<double[]> wxy_chromaticity = new Lazy<double[]>(() =>
		{
			double xD = 0.244063 + 99.11 / LAB.T + 2967800 / (LAB.T * LAB.T) - 4607000000 / (LAB.T * LAB.T * LAB.T);
			return new[] { xD, -3 * xD * xD + 2.87 * xD - 0.275 };
		});

		// The below arrays, the Matrix for rgb_xyz, the calculation of the XYZ_scalar matrix and the calculation of the RGB_TO_XYZ matrix
		// all come from http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html

		static readonly double[] rxyz = { LAB.rxy_chromaticity[0] / LAB.rxy_chromaticity[1], 1, (1 - LAB.rxy_chromaticity[0] - LAB.rxy_chromaticity[1]) / LAB.rxy_chromaticity[1] };
		static readonly double[] gxyz = { LAB.gxy_chromaticity[0] / LAB.gxy_chromaticity[1], 1, (1 - LAB.gxy_chromaticity[0] - LAB.gxy_chromaticity[1]) / LAB.gxy_chromaticity[1] };
		static readonly double[] bxyz = { LAB.bxy_chromaticity[0] / LAB.bxy_chromaticity[1], 1, (1 - LAB.bxy_chromaticity[0] - LAB.bxy_chromaticity[1]) / LAB.bxy_chromaticity[1] };
		static readonly double[] D65 =
		{
			LAB.wxy_chromaticity.Value[0] / LAB.wxy_chromaticity.Value[1],
			1,
			(1 - LAB.wxy_chromaticity.Value[0] - LAB.wxy_chromaticity.Value[1]) / LAB.wxy_chromaticity.Value[1]
		};

		static readonly Matrix<double> rgb_xyz = Matrix<double>.Build.DenseOfArray(new[,]
		{
			{ LAB.rxyz[0], LAB.gxyz[0], LAB.bxyz[0] },
			{ LAB.rxyz[1], LAB.gxyz[1], LAB.bxyz[1] },
			{ LAB.rxyz[2], LAB.gxyz[2], LAB.bxyz[2] }
		});

		static readonly Matrix<double> XYZ_scalar = LAB.rgb_xyz.Inverse() * Matrix<double>.Build.DenseOfColumnMajor(3, 1, LAB.D65);

		static readonly Matrix<double> RGB_TO_XYZ = LAB.rgb_xyz * Matrix<double>.Build.DenseOfDiagonalArray(LAB.XYZ_scalar.ToRowMajorArray());

		/// <summary>
		/// Converts an sRGB value into a linear RGB value.
		/// </summary>
		/// <remarks>
		/// Comes from https://en.wikipedia.org/wiki/SRGB#The_reverse_transformation
		/// </remarks>
		/// <param name="c">The sRGB red, green or blue value to convert.</param>
		/// <returns>The RGB value as linear.</returns>
		static double sRGBToLinear(double c) => c <= 0.04045 ? c / 12.92 : Math.Pow((c + 0.055) / 1.055, 2.4);

		const double delta = 6.0 / 29.0;
		const double deltaSquared = LAB.delta * LAB.delta;
		const double deltaCubed = LAB.deltaSquared * LAB.delta;

		/// <summary>
		/// The f(t) function for the forward transformation from CIE XYZ to CIE L*a*b*.
		/// </summary>
		/// <remarks>
		/// Comes from https://en.wikipedia.org/wiki/Lab_color_space#Forward_transformation
		/// </remarks>
		/// <param name="t">The x, y or z value to convert.</param>
		/// <returns>The converted value.</returns>
		static double XYZf(double t) => t > LAB.deltaCubed ?  Math.Pow(t, 1.0 / 3.0) : t / (3 * LAB.deltaSquared) + 4.0 / 29.0;

		/// <summary>
		/// Converts a color from the sRGB color space to the CIE L*a*b* color space.
		/// </summary>
		/// <param name="sRGB">The color in the sRGB color space.</param>
		/// <returns>The color in the CIE L*a*b* color space.</returns>
		public static LAB FromRGB(Color sRGB)
		{
			// First we convert the color to CIE XYZ
			var XYZ = LAB.RGB_TO_XYZ * Matrix<double>.Build.DenseOfColumnMajor(3, 1, new[] { LAB.sRGBToLinear(sRGB.R / 255.0), LAB.sRGBToLinear(sRGB.G / 255.0), LAB.sRGBToLinear(sRGB.B / 255.0) });

			// Then we apply the f(t) function to the CIE XYZ values with respect to the white point
			double x = LAB.XYZf(XYZ[0, 0] / LAB.D65[0]);
			double y = LAB.XYZf(XYZ[1, 0] / LAB.D65[1]);
			double z = LAB.XYZf(XYZ[2, 0] / LAB.D65[2]);

			// Finally we convert the color to CIE L*a*b*
			return new LAB((116.0 * y) - 16.0, 500.0 * (x - y), 200.0 * (y - z));
		}

		/// <summary>
		/// Calculates the Euclidean distance between two CIE L*a*b* colors.
		/// </summary>
		/// <param name="lab1">The first CIE L*a*b* color.</param>
		/// <param name="lab2">The second CIE L*a*b* color.</param>
		/// <returns>The Euclidean distance between the colors.</returns>
		public static double Distance(LAB lab1, LAB lab2)
		{
			double deltaL = lab1.L - lab2.L;
			double deltaA = lab1.A - lab2.A;
			double deltaB = lab1.B - lab2.B;

			return Math.Sqrt(deltaL * deltaL + deltaA * deltaA + deltaB * deltaB);
		}

		public override bool Equals(object obj) => obj != null && obj is LAB && this.Equals((LAB)obj);

		public bool Equals(LAB other) => this == other;

		public static bool operator==(LAB lab1, LAB lab2) => lab1.L == lab2.L && lab1.A == lab2.A && lab1.B == lab2.B;

		public static bool operator!=(LAB lab1, LAB lab2) => !(lab1 == lab2);

		public override int GetHashCode() => this.L.GetHashCode() ^ this.A.GetHashCode() ^ this.B.GetHashCode();

		public override string ToString() => FormattableString.Invariant($"L: {this.L}, A: {this.A}, B: {this.B}");
	}
}
