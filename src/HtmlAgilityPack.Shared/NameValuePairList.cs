// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

namespace HtmlAgilityPack;

internal sealed class NameValuePairList
{
    #region Fields

    internal readonly string? Text;
    private readonly List<KeyValuePair<string, string>> _allPairs;
    private readonly Dictionary<string, List<KeyValuePair<string, string>>> _pairsWithName;
    internal static readonly char[] separator = ['='];

    #endregion

    #region Constructors

    internal NameValuePairList() :
        this(null)
    {
    }

    internal NameValuePairList(string? text)
    {
        Text = text;
        _allPairs = [];
        _pairsWithName = [];

        Parse(text);
    }

    #endregion

    #region Internal Methods

    internal static string GetNameValuePairsValue(string? text, string name)
    {
        NameValuePairList l = new(text);
        return l.GetNameValuePairValue(name);
    }

    internal List<KeyValuePair<string, string>> GetNameValuePairs(string name)
    {
        return name is null
            ? _allPairs
            : _pairsWithName.TryGetValue(name, out List<KeyValuePair<string, string>>? value)
            ? value
            : [];
    }

    internal string GetNameValuePairValue(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        List<KeyValuePair<string, string>> al = GetNameValuePairs(name);
        if (al.Count == 0)
        {
            return string.Empty;
        }

        // return first item
        return al[0].Value.Trim();
    }

    #endregion

    #region Private Methods

    private void Parse(string? text)
    {
        _allPairs.Clear();
        _pairsWithName.Clear();
        if (text is null)
        {
            return;
        }

        string[] p = text.Split(';');
        foreach (string pv in p)
        {
            if (pv.Length == 0)
            {
                continue;
            }

            string[] onep = pv.Split(separator, 2);
            if (onep.Length == 0)
            {
                continue;
            }

            KeyValuePair<string, string> nvp = new(onep[0].Trim().ToLowerInvariant(),
                onep.Length < 2 ? "" : onep[1]);

            _allPairs.Add(nvp);

            // index by name
            if (!_pairsWithName.TryGetValue(nvp.Key, out List<KeyValuePair<string, string>>? al))
            {
                al = [];
                _pairsWithName.Add(nvp.Key, al);
            }

            al.Add(nvp);
        }
    }

    #endregion
}