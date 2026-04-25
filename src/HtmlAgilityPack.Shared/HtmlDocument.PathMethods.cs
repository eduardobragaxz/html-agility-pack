// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

#if !METRO

using System.Text;

namespace HtmlAgilityPack;

public sealed partial class HtmlDocument
{
    /// <summary>
    /// Detects the encoding of an HTML document from a file first, and then loads the file.
    /// </summary>
    /// <param name="path">The complete file path to be read.</param>
    public void DetectEncodingAndLoad(string path)
    {
        DetectEncodingAndLoad(path, true);
    }

    /// <summary>
    /// Detects the encoding of an HTML document from a file first, and then loads the file.
    /// </summary>
    /// <param name="path">The complete file path to be read. May not be null.</param>
    /// <param name="detectEncoding">true to detect encoding, false otherwise.</param>
    public void DetectEncodingAndLoad(string path, bool detectEncoding)
    {
        ArgumentNullException.ThrowIfNull(path);

        Encoding? enc = detectEncoding ? DetectEncoding(path) : null;
        if (enc is null)
        {
            Load(path);
        }
        else
        {
            Load(path, enc);
        }
    }

    /// <summary>
    /// Detects the encoding of an HTML file.
    /// </summary>
    /// <param name="path">Path for the file containing the HTML document to detect. May not be null.</param>
    /// <returns>The detected encoding.</returns>
    public Encoding? DetectEncoding(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

#if NETSTANDARD1_3 || NETSTANDARD1_6
        using (StreamReader sr = new StreamReader(File.OpenRead(path), OptionDefaultStreamEncoding))
#else
        using StreamReader sr = new(path, OptionDefaultStreamEncoding);
#endif
        Encoding? encoding = DetectEncoding(sr);
        return encoding;
    }

    /// <summary>
    /// Loads an HTML document from a file.
    /// </summary>
    /// <param name="path">The complete file path to be read. May not be null.</param>
    public void Load(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

#if NETSTANDARD1_3 || NETSTANDARD1_6
        using (StreamReader sr = new StreamReader(File.OpenRead(path), OptionDefaultStreamEncoding))
#else
        using StreamReader sr = new(path, OptionDefaultStreamEncoding);
#endif
        Load(sr);
    }

    /// <summary>
    /// Loads an HTML document from a file.
    /// </summary>
    /// <param name="path">The complete file path to be read. May not be null.</param>
    /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
    public void Load(string path, bool detectEncodingFromByteOrderMarks)
    {
        ArgumentNullException.ThrowIfNull(path);

#if NETSTANDARD1_3 || NETSTANDARD1_6
        using (StreamReader sr = new StreamReader(File.OpenRead(path), detectEncodingFromByteOrderMarks))
#else
        using StreamReader sr = new(path, detectEncodingFromByteOrderMarks);
#endif
        Load(sr);
    }

    /// <summary>
    /// Loads an HTML document from a file.
    /// </summary>
    /// <param name="path">The complete file path to be read. May not be null.</param>
    /// <param name="encoding">The character encoding to use. May not be null.</param>
    public void Load(string path, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(path);

        ArgumentNullException.ThrowIfNull(encoding);

#if NETSTANDARD1_3 || NETSTANDARD1_6
        using (StreamReader sr = new StreamReader(File.OpenRead(path), encoding))
#else
        using StreamReader sr = new(path, encoding);
#endif
        Load(sr);
    }

    /// <summary>
    /// Loads an HTML document from a file.
    /// </summary>
    /// <param name="path">The complete file path to be read. May not be null.</param>
    /// <param name="encoding">The character encoding to use. May not be null.</param>
    /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
    public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
    {
        ArgumentNullException.ThrowIfNull(path);

        ArgumentNullException.ThrowIfNull(encoding);

#if NETSTANDARD1_3 || NETSTANDARD1_6
        using (StreamReader sr = new StreamReader(File.OpenRead(path), encoding, detectEncodingFromByteOrderMarks))
#else
        using StreamReader sr = new(path, encoding, detectEncodingFromByteOrderMarks);
#endif
        Load(sr);
    }

    /// <summary>
    /// Loads an HTML document from a file.
    /// </summary>
    /// <param name="path">The complete file path to be read. May not be null.</param>
    /// <param name="encoding">The character encoding to use. May not be null.</param>
    /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
    /// <param name="buffersize">The minimum buffer size.</param>
    public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
    {
        ArgumentNullException.ThrowIfNull(path);

        ArgumentNullException.ThrowIfNull(encoding);

#if NETSTANDARD1_3 || NETSTANDARD1_6
        using (StreamReader sr = new StreamReader(File.OpenRead(path), encoding, detectEncodingFromByteOrderMarks, buffersize))

#else
        using StreamReader sr = new(path, encoding, detectEncodingFromByteOrderMarks, buffersize);
#endif
        Load(sr);
    }

    /// <summary>
    /// Saves the mixed document to the specified file.
    /// </summary>
    /// <param name="filename">The location of the file where you want to save the document.</param>
    public void Save(string filename)
    {
#if NETSTANDARD1_3 || NETSTANDARD1_6
        using (StreamWriter sw = new StreamWriter(File.OpenWrite(filename), GetOutEncoding()))
#else
        using StreamWriter sw = new(filename, false, GetOutEncoding());
#endif
        Save(sw);
    }

    /// <summary>
    /// Saves the mixed document to the specified file.
    /// </summary>
    /// <param name="filename">The location of the file where you want to save the document. May not be null.</param>
    /// <param name="encoding">The character encoding to use. May not be null.</param>
    public void Save(string filename, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(filename);

        ArgumentNullException.ThrowIfNull(encoding);
#if NETSTANDARD1_3 || NETSTANDARD1_6
        using (StreamWriter sw = new StreamWriter(File.OpenWrite(filename), encoding))
#else
        using StreamWriter sw = new(filename, false, encoding);
#endif
        Save(sw);
    }
}
#endif