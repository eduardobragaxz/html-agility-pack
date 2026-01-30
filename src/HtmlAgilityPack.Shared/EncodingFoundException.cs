// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System.Text;

namespace HtmlAgilityPack;

internal class EncodingFoundException : Exception
{
    #region Fields

    private readonly Encoding _encoding;

    #endregion

    #region Constructors

    internal EncodingFoundException(Encoding encoding)
    {
        _encoding = encoding;
    }

    #endregion

    #region Properties

    internal Encoding Encoding
    {
        get { return _encoding; }
    }

    #endregion
}