// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

#if !METRO
namespace HtmlAgilityPack;

/// <summary>
/// Represents a base class for fragments in a mixed code document.
/// </summary>
public abstract class MixedCodeDocumentFragment
{
    #region Fields

    internal MixedCodeDocument Doc;
    internal int Index;
    internal int Length;
    internal int _lineposition;
    internal MixedCodeDocumentFragmentType _type;

    #endregion

    #region Constructors

    internal MixedCodeDocumentFragment(MixedCodeDocument doc, MixedCodeDocumentFragmentType type)
    {
        Doc = doc;
        _type = type;
        switch (type)
        {
            case MixedCodeDocumentFragmentType.Text:
                Doc._textfragments.Append(this);
                break;

            case MixedCodeDocumentFragmentType.Code:
                Doc._codefragments.Append(this);
                break;
        }

        Doc._fragments.Append(this);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the fragement text.
    /// </summary>
    public string? FragmentText
    {
        get
        {
            field ??= Doc._text?.Substring(Index, Length);

            return field;
        }
        internal set;
    }

    /// <summary>
    /// Gets the type of fragment.
    /// </summary>
    public MixedCodeDocumentFragmentType FragmentType => _type;

    /// <summary>
    /// Gets the line number of the fragment.
    /// </summary>
    public int Line { get; internal set; }

    /// <summary>
    /// Gets the line position (column) of the fragment.
    /// </summary>
    public int LinePosition => _lineposition;

    /// <summary>
    /// Gets the fragment position in the document's stream.
    /// </summary>
    public int StreamPosition => Index;

    #endregion
}
#endif