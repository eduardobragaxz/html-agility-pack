// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

namespace HtmlAgilityPack;

/// <summary>
/// Represents an HTML text node.
/// </summary>
public sealed class HtmlTextNode : HtmlNode
{
    #region Fields

    private string? _text;

    #endregion

    #region Constructors

    internal HtmlTextNode(HtmlDocument ownerdocument, int index)
        :
        base(HtmlNodeType.Text, ownerdocument, index)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or Sets the HTML between the start and end tags of the object. In the case of a text node, it is equals to OuterHtml.
    /// </summary>
    public override string? InnerHtml
    {
        get => OuterHtml; set => _text = value;
    }

    /// <summary>
    /// Gets or Sets the object and its content in HTML.
    /// </summary>
    public override string OuterHtml => _text is null ? base.OuterHtml! : _text;

    /// <summary>
    /// Gets or Sets the text of the node.
    /// </summary>
    public string? Text
    {
        get => _text is null ? base.OuterHtml : _text;
        set
        {
            _text = value;
            SetChanged();
        }
    }

    #endregion
}