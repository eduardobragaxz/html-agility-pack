// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

#if !METRO

using System.Collections;

namespace HtmlAgilityPack;

/// <summary>
/// Represents a list of mixed code fragments.
/// </summary>
public sealed class MixedCodeDocumentFragmentList : IEnumerable
{
    #region Fields

    private readonly List<MixedCodeDocumentFragment> _items = [];

    #endregion

    #region Constructors

    internal MixedCodeDocumentFragmentList(MixedCodeDocument doc)
    {
        Doc = doc;
    }

    #endregion

    #region Properties

    ///<summary>
    /// Gets the Document
    ///</summary>
    public MixedCodeDocument Doc { get; }

    /// <summary>
    /// Gets the number of fragments contained in the list.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gets a fragment from the list using its index.
    /// </summary>
    public MixedCodeDocumentFragment this[int index] => _items[index];

    #endregion

    #region IEnumerable Members

    /// <summary>
    /// Gets an enumerator that can iterate through the fragment list.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Appends a fragment to the list of fragments.
    /// </summary>
    /// <param name="newFragment">The fragment to append. May not be null.</param>
    public void Append(MixedCodeDocumentFragment newFragment)
    {
        ArgumentNullException.ThrowIfNull(newFragment);

        _items.Add(newFragment);
    }

    /// <summary>
    /// Gets an enumerator that can iterate through the fragment list.
    /// </summary>
    public MixedCodeDocumentFragmentEnumerator GetEnumerator()
    {
        return new MixedCodeDocumentFragmentEnumerator(_items);
    }

    /// <summary>
    /// Prepends a fragment to the list of fragments.
    /// </summary>
    /// <param name="newFragment">The fragment to append. May not be null.</param>
    public void Prepend(MixedCodeDocumentFragment newFragment)
    {
        ArgumentNullException.ThrowIfNull(newFragment);

        _items.Insert(0, newFragment);
    }

    /// <summary>
    /// Remove a fragment from the list of fragments. If this fragment was not in the list, an exception will be raised.
    /// </summary>
    /// <param name="fragment">The fragment to remove. May not be null.</param>
    public void Remove(MixedCodeDocumentFragment fragment)
    {
        ArgumentNullException.ThrowIfNull(fragment);

        int index = GetFragmentIndex(fragment);
        if (index == -1)
        {
            throw new IndexOutOfRangeException();
        }

        RemoveAt(index);
    }

    /// <summary>
    /// Remove all fragments from the list.
    /// </summary>
    public void RemoveAll()
    {
        _items.Clear();
    }

    /// <summary>
    /// Remove a fragment from the list of fragments, using its index in the list.
    /// </summary>
    /// <param name="index">The index of the fragment to remove.</param>
    public void RemoveAt(int index)
    {
        //MixedCodeDocumentFragment frag = (MixedCodeDocumentFragment) _items[index];
        _items.RemoveAt(index);
    }

    #endregion

    #region Internal Methods

    internal void Clear()
    {
        _items.Clear();
    }

    internal int GetFragmentIndex(MixedCodeDocumentFragment fragment)
    {
        ArgumentNullException.ThrowIfNull(fragment);

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == fragment)
            {
                return i;
            }
        }

        return -1;
    }

    #endregion

    #region Nested type: MixedCodeDocumentFragmentEnumerator

    /// <summary>
    /// Represents a fragment enumerator.
    /// </summary>
    public class MixedCodeDocumentFragmentEnumerator : IEnumerator
    {
        #region Fields

        private int _index;
        private readonly IList<MixedCodeDocumentFragment> _items;

        #endregion

        #region Constructors

        internal MixedCodeDocumentFragmentEnumerator(IList<MixedCodeDocumentFragment> items)
        {
            _items = items;
            _index = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        public MixedCodeDocumentFragment Current => _items[_index];

        #endregion

        #region IEnumerator Members

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            _index++;
            return _index < _items.Count;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            _index = -1;
        }

        #endregion
    }

    #endregion
}
#endif