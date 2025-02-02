using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Nvidia.TextureTools
{
    #region Enums

    #region public enum Format
    /// <summary>
    /// Compression format.
    /// </summary>
    public enum Format
    {
        // No compression.
        RGB,
        RGBA = RGB,

        // DX9 formats.
        DXT1,
        DXT1a,
        DXT3,
        DXT5,
        DXT5n,

        // DX10 formats.
        BC1 = DXT1,
        BC1a = DXT1a,
        BC2 = DXT3,
        BC3 = DXT5,
        BC3n = DXT5n,
        BC4,
        BC5,

        DXT1n,   // Not supported on CPU yet.
        CTX1,    // Not supported on CPU yet.

        BC6,
        BC7,     // Not supported yet.

        DXT1_Luma,
    }
    #endregion

    #region public enum Quality
    /// <summary>
    /// Quality modes.
    /// </summary>
    public enum Quality
    {
        Fastest,
        Normal,
        Production,
        Highest,
    }
    #endregion

    #region public enum WrapMode
    /// <summary>
    /// Wrap modes.
    /// </summary>
    public enum WrapMode
    {
        Clamp,
        Repeat,
        Mirror,
    }
    #endregion

    #region public enum TextureType
    /// <summary>
    /// Texture types.
    /// </summary>
    public enum TextureType
    {
        Texture2D,
        TextureCube,
    }
    #endregion

    #region public enum InputFormat
    /// <summary>
    /// Input formats.
    /// </summary>
    public enum InputFormat
    {
        BGRA_8UB
    }
    #endregion

    #region public enum MipmapFilter
    /// <summary>
    /// Mipmap downsampling filters.
    /// </summary>
    public enum MipmapFilter
    {
        Box,
        Triangle,
        Kaiser
    }
    #endregion

    #region public enum ColorTransform
    /// <summary>
    /// Color transformation.
    /// </summary>
    public enum ColorTransform
    {
        None,
        Linear
    }
    #endregion

    #region public enum RoundMode
    /// <summary>
    /// Extents rounding mode.
    /// </summary>
    public enum RoundMode
    {
        None,
        ToNextPowerOfTwo,
        ToNearestPowerOfTwo,
        ToPreviousPowerOfTwo
    }
    #endregion

    #region public enum AlphaMode
    /// <summary>
    /// Alpha mode.
    /// </summary>
    public enum AlphaMode
    {
        None,
        Transparency,
        Premultiplied
    }
    #endregion

    #region public enum Error
    /// <summary>
    /// Error codes.
    /// </summary>
    public enum Error
    {
        InvalidInput,
        UserInterruption,
        UnsupportedFeature,
        CudaError,
        Unknown,
        FileOpen,
        FileWrite,
    }
    #endregion

    #endregion

    #region public class InputOptions
    /// <summary>
    /// Input options.
    /// </summary>
    public class InputOptions : IDisposable
    {
        #region Bindings
        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static IntPtr nvttCreateInputOptions();

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttDestroyInputOptions(IntPtr inputOptions);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsTextureLayout(IntPtr inputOptions, TextureType type, int w, int h, int d);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttResetInputOptionsTextureLayout(IntPtr inputOptions);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static bool nvttSetInputOptionsMipmapData(IntPtr inputOptions, IntPtr data, int w, int h, int d, int face, int mipmap);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsFormat(IntPtr inputOptions, InputFormat format);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsAlphaMode(IntPtr inputOptions, AlphaMode alphaMode);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsGamma(IntPtr inputOptions, float inputGamma, float outputGamma);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsWrapMode(IntPtr inputOptions, WrapMode mode);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsMipmapFilter(IntPtr inputOptions, MipmapFilter filter);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsMipmapGeneration(IntPtr inputOptions, bool generateMipmaps, int maxLevel);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsKaiserParameters(IntPtr inputOptions, float width, float alpha, float stretch);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsNormalMap(IntPtr inputOptions, bool b);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsConvertToNormalMap(IntPtr inputOptions, bool convert);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsHeightEvaluation(IntPtr inputOptions, float redScale, float greenScale, float blueScale, float alphaScale);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsNormalFilter(IntPtr inputOptions, float small, float medium, float big, float large);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsNormalizeMipmaps(IntPtr inputOptions, bool b);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsColorTransform(IntPtr inputOptions, ColorTransform t);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsLinearTransfrom(IntPtr inputOptions, int channel, float w0, float w1, float w2, float w3);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsMaxExtents(IntPtr inputOptions, int d);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetInputOptionsRoundMode(IntPtr inputOptions, RoundMode mode);
        #endregion

        internal IntPtr _handle;

        internal InputOptions()
        {
            _handle = nvttCreateInputOptions();
        }

        public void SetTextureLayout(TextureType type, int w, int h, int d)
        {
            nvttSetInputOptionsTextureLayout(_handle, type, w, h, d);
        }
        public void ResetTextureLayout()
        {
            nvttResetInputOptionsTextureLayout(_handle);
        }

        public void SetMipmapData(IntPtr data, int width, int height, int depth, int face, int mipmap)
        {
            nvttSetInputOptionsMipmapData(_handle, data, width, height, depth, face, mipmap);
        }

        public void SetFormat(InputFormat format)
        {
            nvttSetInputOptionsFormat(_handle, format);
        }

        public void SetAlphaMode(AlphaMode alphaMode)
        {
            nvttSetInputOptionsAlphaMode(_handle, alphaMode);
        }

        public void SetGamma(float inputGamma, float outputGamma)
        {
            nvttSetInputOptionsGamma(_handle, inputGamma, outputGamma);
        }

        public void SetWrapMode(WrapMode wrapMode)
        {
            nvttSetInputOptionsWrapMode(_handle, wrapMode);
        }

        public void SetMipmapFilter(MipmapFilter filter)
        {
            nvttSetInputOptionsMipmapFilter(_handle, filter);
        }

        public void SetMipmapGeneration(bool enabled)
        {
            nvttSetInputOptionsMipmapGeneration(_handle, enabled, -1);
        }

        public void SetMipmapGeneration(bool enabled, int maxLevel)
        {
            nvttSetInputOptionsMipmapGeneration(_handle, enabled, maxLevel);
        }

        public void SetKaiserParameters(float width, float alpha, float stretch)
        {
            nvttSetInputOptionsKaiserParameters(_handle, width, alpha, stretch);
        }

        public void SetNormalMap(bool b)
        {
            nvttSetInputOptionsNormalMap(_handle, b);
        }

        public void SetConvertToNormalMap(bool convert)
        {
            nvttSetInputOptionsConvertToNormalMap(_handle, convert);
        }

        public void SetHeightEvaluation(float redScale, float greenScale, float blueScale, float alphaScale)
        {
            nvttSetInputOptionsHeightEvaluation(_handle, redScale, greenScale, blueScale, alphaScale);
        }

        public void SetNormalFilter(float small, float medium, float big, float large)
        {
            nvttSetInputOptionsNormalFilter(_handle, small, medium, big, large);
        }

        public void SetNormalizeMipmaps(bool b)
        {
            nvttSetInputOptionsNormalizeMipmaps(_handle, b);
        }

        public void SetColorTransform(ColorTransform t)
        {
            nvttSetInputOptionsColorTransform(_handle, t);
        }

        public void SetLinearTransfrom(int channel, float w0, float w1, float w2, float w3)
        {
            nvttSetInputOptionsLinearTransfrom(_handle, channel, w0, w1, w2, w3);
        }

        public void SetMaxExtents(int dim)
        {
            nvttSetInputOptionsMaxExtents(_handle, dim);
        }

        public void SetRoundMode(RoundMode mode)
        {
            nvttSetInputOptionsRoundMode(_handle, mode);
        }

        ~InputOptions()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            if (_handle != IntPtr.Zero)
                nvttDestroyInputOptions(_handle);
            _handle = IntPtr.Zero;
        }
    }
    #endregion

    #region public class CompressionOptions
    /// <summary>
    /// Compression options.
    /// </summary>
    public class CompressionOptions : IDisposable
    {
        #region Bindings
        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static IntPtr nvttCreateCompressionOptions();

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttDestroyCompressionOptions(IntPtr compressionOptions);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetCompressionOptionsFormat(IntPtr compressionOptions, Format format);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetCompressionOptionsQuality(IntPtr compressionOptions, Quality quality);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetCompressionOptionsColorWeights(IntPtr compressionOptions, float red, float green, float blue, float alpha);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetCompressionOptionsPixelFormat(IntPtr compressionOptions, uint bitcount, uint rmask, uint gmask, uint bmask, uint amask);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetCompressionOptionsQuantization(IntPtr compressionOptions, bool colorDithering, bool alphaDithering, bool binaryAlpha, int alphaThreshold);
        #endregion

        internal IntPtr _handle;

        internal CompressionOptions()
        {
            _handle = nvttCreateCompressionOptions();
        }

        public void SetFormat(Format format)
        {
            nvttSetCompressionOptionsFormat(_handle, format);
        }
        
        public void SetQuality(Quality quality)
        {
            nvttSetCompressionOptionsQuality(_handle, quality);
        }

        public void SetColorWeights(float red, float green, float blue)
        {
            nvttSetCompressionOptionsColorWeights(_handle, red, green, blue, 1.0f);
        }

        public void SetColorWeights(float red, float green, float blue, float alpha)
        {
            nvttSetCompressionOptionsColorWeights(_handle, red, green, blue, alpha);
        }

        public void SetPixelFormat(uint bitcount, uint rmask, uint gmask, uint bmask, uint amask)
        {
            nvttSetCompressionOptionsPixelFormat(_handle, bitcount, rmask, gmask, bmask, amask);
        }

        public void SetQuantization(bool colorDithering, bool alphaDithering, bool binaryAlpha)
        {
            nvttSetCompressionOptionsQuantization(_handle, colorDithering, alphaDithering, binaryAlpha, 127);
        }

        public void SetQuantization(bool colorDithering, bool alphaDithering, bool binaryAlpha, int alphaThreshold)
        {
            nvttSetCompressionOptionsQuantization(_handle, colorDithering, alphaDithering, binaryAlpha, alphaThreshold);
        }

        ~CompressionOptions()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            if (_handle != IntPtr.Zero)
                nvttDestroyCompressionOptions(_handle);
            _handle = IntPtr.Zero;

        }
    }
    #endregion

    #region public class OutputOptions
    /// <summary>
    /// Output options.
    /// </summary>
    public class OutputOptions : IDisposable
    {
        #region Delegates
        public delegate void ErrorHandler(Error error);
        public delegate void BeginImageDelegate(int size, int width, int height, int depth, int face, int miplevel);
        public delegate bool WriteDataDelegate(IntPtr data, int size);
        public delegate void EndImageDelegate();
        #endregion

        #region Bindings
        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static IntPtr nvttCreateOutputOptions();

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttDestroyOutputOptions(IntPtr outputOptions);

        [DllImport("nvtt", CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetOutputOptionsFileName(IntPtr outputOptions, string fileName);

        //[DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        //private extern static void nvttSetOutputOptionsErrorHandler(IntPtr outputOptions, ErrorHandler errorHandler);

        private void ErrorCallback(Error error)
        {
            if (Error != null) Error(error);
        }

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetOutputOptionsOutputHeader(IntPtr outputOptions, bool b);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttSetOutputOptionsOutputHandler(IntPtr outputOptions, IntPtr beginImageHandler, IntPtr outputHandler, IntPtr endImageHandler);

        #endregion

        internal IntPtr _handle;

        internal OutputOptions()
        {
            _handle = nvttCreateOutputOptions();
            //nvttSetOutputOptionsErrorHandler(options, new ErrorHandler(ErrorCallback));
        }

        public void SetFileName(string fileName)
        {
            nvttSetOutputOptionsFileName(_handle, fileName);
        }

        public event ErrorHandler Error;

        public void SetOutputHeader(bool b)
        {
            nvttSetOutputOptionsOutputHeader(_handle, b);
        }

        public void SetOutputOptionsOutputHandler (BeginImageDelegate beginImageHandler, WriteDataDelegate outputHandler, EndImageDelegate endImageHandler)
        {
            IntPtr beginImage = Marshal.GetFunctionPointerForDelegate(beginImageHandler);
            IntPtr writeData = Marshal.GetFunctionPointerForDelegate(outputHandler);
            IntPtr endImage = Marshal.GetFunctionPointerForDelegate(endImageHandler);

            nvttSetOutputOptionsOutputHandler(this._handle, beginImage, writeData, endImage);
        }

        ~OutputOptions()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            if (_handle != IntPtr.Zero)
            {
                nvttSetOutputOptionsOutputHandler(this._handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                nvttDestroyOutputOptions(_handle);
            }
            _handle = IntPtr.Zero;
        }
    }
    #endregion

    #region public static class Compressor
    public class Compressor : IDisposable
    {
        #region Bindings
        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static IntPtr nvttCreateCompressor();

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static void nvttDestroyCompressor(IntPtr compressor);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static bool nvttCompress(IntPtr compressor, IntPtr inputOptions, IntPtr compressionOptions, IntPtr outputOptions);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private extern static int nvttEstimateSize(IntPtr compressor, IntPtr inputOptions, IntPtr compressionOptions);

        [DllImport("nvtt"), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr nvttErrorString(Error error);

        #endregion

        internal IntPtr _handle;

        public InputOptions InputOptions { get; private set; }
        public CompressionOptions CompressionOptions { get; private set; }
        public OutputOptions OutputOptions { get; private set; }

        public Compressor()
        {
            this._handle = nvttCreateCompressor();

            this.InputOptions       = new InputOptions();
            this.CompressionOptions = new CompressionOptions();
            this.OutputOptions      = new OutputOptions();

        }

        public bool Compress()
        {
            return nvttCompress(_handle, InputOptions._handle, CompressionOptions._handle, OutputOptions._handle);
        }

        public int EstimateSize(InputOptions input, CompressionOptions compression)
        {
            return nvttEstimateSize(_handle, input._handle, compression._handle);
        }

        public static string ErrorString(Error error)
        {
            return Marshal.PtrToStringAnsi(nvttErrorString(error));
        }

        ~Compressor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            if (this.OutputOptions != null)
                this.OutputOptions.Dispose();
            this.OutputOptions = null;
            if (this.CompressionOptions != null)
                this.CompressionOptions.Dispose();
            this.CompressionOptions = null;
            if (this.InputOptions != null)
                this.InputOptions.Dispose();
            this.InputOptions = null;

            if (_handle != IntPtr.Zero)
                nvttDestroyCompressor(_handle);
            _handle = IntPtr.Zero;
        }
    }
    #endregion

} // Nvidia.TextureTools namespace
