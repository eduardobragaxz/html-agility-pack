// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

#if !METRO
namespace HtmlAgilityPack;

/// <summary>
/// Represents a fragment of code in a mixed code document.
/// </summary>
public sealed class MixedCodeDocumentCodeFragment : MixedCodeDocumentFragment
{
    #region Fields


    #endregion

    #region Constructors

    internal MixedCodeDocumentCodeFragment(MixedCodeDocument doc)
        :
        base(doc, MixedCodeDocumentFragmentType.Code)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the fragment code text.
    /// </summary>
    public string Code
    {
        get
        {
            if (field is null)
            {
                field = FragmentText?.Substring(Doc.TokenCodeStart.Length,
                    FragmentText.Length - Doc.TokenCodeEnd.Length -
                    Doc.TokenCodeStart.Length - 1).Trim();
                if (field!.StartsWith('='))
                {
                    field = string.Concat(Doc.TokenResponseWrite, field.AsSpan(1, field.Length - 1));
                }
            }

            return field;
        }
        set;
    }

    #endregion
}
#endif