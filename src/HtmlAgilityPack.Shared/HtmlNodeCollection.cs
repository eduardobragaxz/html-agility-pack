// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System.Collections;

namespace HtmlAgilityPack;

/// <summary>
/// Represents a combined list and collection of HTML nodes.
/// </summary>
/// <remarks>
/// Initialize the HtmlNodeCollection with the base parent node
/// </remarks>
public class HtmlNodeCollection : IList<HtmlNode>
{
    #region Fields

    private readonly List<HtmlNode> _items = [];

    #endregion
    #region Constructors

    #endregion

    #region Properties

    /// <summary>Gets the parent node associated to the collection.</summary>
    internal HtmlNode? ParentNode { get; init; }

    /// <summary>
    /// Gets a given node from the list.
    /// </summary>
    public int this[HtmlNode node]
    {
        get
        {
            int index = GetNodeIndex(node);
            return index == -1
                ? throw new ArgumentOutOfRangeException(nameof(node),
                    "Node \"" + node.CloneNode(false)?.OuterHtml +
                    "\" was not found in the collection")
                : index;
        }
    }

    /// <summary>
    /// Get node with tag name
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    public HtmlNode? this[string nodeName]
    {
        get
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (string.Equals(_items[i]?.Name, nodeName, StringComparison.OrdinalIgnoreCase))
                {
                    return _items[i];
                }
            }

            return null;
        }
    }

    #endregion

    #region IList<HtmlNode> Members

    /// <summary>
    /// Gets the number of elements actually contained in the list.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Is collection read only
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets the node at the specified index.
    /// </summary>
    public HtmlNode this[int index]
    {
        get => _items[index]; set => _items[index] = value;
    }

    /// <summary>
    /// Add node to the collection
    /// </summary>
    /// <param name="node"></param>
    public void Add(HtmlNode node)
    {
        Add(node, true);
    }

    /// <summary>
    /// Add node to the collection
    /// </summary>
    /// <param name="node"></param>
    /// <param name="setParent"></param>
    public void Add(HtmlNode node, bool setParent)
    {
        _items.Add(node);

        if (setParent)
        {
            _ = (node?.ParentNode = ParentNode);
        }
    }

    /// <summary>
    /// Clears out the collection of HtmlNodes. Removes each nodes reference to parentnode, nextnode and prevnode
    /// </summary>
    public void Clear()
    {
        foreach (HtmlNode? node in _items)
        {
            _ = (node?.ParentNode = null);
            _ = (node?.NextSibling = null);
            _ = (node?.PreviousSibling = null);
        }

        _items.Clear();
    }

    /// <summary>
    /// Gets existence of node in collection
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(HtmlNode item)
    {
        return _items.Contains(item);
    }

    /// <summary>
    /// Copy collection to array
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(HtmlNode[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Get Enumerator
    /// </summary>
    /// <returns></returns>
    public List<HtmlNode>.Enumerator GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    /// <summary>
    /// Get Enumerator
    /// </summary>
    /// <returns></returns>
    IEnumerator<HtmlNode> IEnumerable<HtmlNode>.GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    /// <summary>
    /// Get Explicit Enumerator
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    /// <summary>
    /// Get index of node
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int IndexOf(HtmlNode item)
    {
        return _items.IndexOf(item);
    }

    /// <summary>
    /// Insert node at index
    /// </summary>
    /// <param name="index"></param>
    /// <param name="node"></param>
    public void Insert(int index, HtmlNode node)
    {
        HtmlNode? next = null;
        HtmlNode? prev = null;

        if (index > 0)
        {
            prev = _items[index - 1];
        }

        if (index < _items.Count)
        {
            next = _items[index];
        }

        _items.Insert(index, node);

        if (prev is not null)
        {
            if (node == prev)
            {
                throw new InvalidProgramException("Unexpected error.");
            }

            prev._nextnode = node;
        }

        _ = (next?._prevnode = node);

        _ = (node?._prevnode = prev);
        if (next == node)
        {
            throw new InvalidProgramException("Unexpected error.");
        }

        _ = (node?._nextnode = next);
        node?.SetParent(ParentNode);
    }

    /// <summary>
    /// Remove node
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Remove(HtmlNode item)
    {
        int i = _items.IndexOf(item);
        RemoveAt(i);
        return true;
    }

    /// <summary>
    /// Remove <see cref="HtmlNode"/> at index
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
        HtmlNode? next = null;
        HtmlNode? prev = null;
        HtmlNode oldnode = _items[index];

        // KEEP a reference since it will be set to null
        HtmlNode? parentNode = ParentNode ?? oldnode?._parentnode;

        if (index > 0)
        {
            prev = _items[index - 1];
        }

        if (index < (_items.Count - 1))
        {
            next = _items[index + 1];
        }

        _items.RemoveAt(index);

        if (prev is not null)
        {
            if (next == prev)
            {
                throw new InvalidProgramException("Unexpected error.");
            }

            prev._nextnode = next;
        }

        _ = (next?._prevnode = prev);

        _ = (oldnode?._prevnode = null);
        _ = (oldnode?._nextnode = null);
        _ = (oldnode?._parentnode = null);

        parentNode?.SetChanged();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Get first instance of node in supplied collection
    /// </summary>
    /// <param name="items"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static HtmlNode? FindFirst(HtmlNodeCollection items, string name)
    {
        foreach (HtmlNode node in items)
        {
            if (node!.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return node;
            }

            if (!node.HasChildNodes)
            {
                continue;
            }

            HtmlNode? returnNode = FindFirst(node.ChildNodes, name);
            if (returnNode is not null)
            {
                return returnNode;
            }
        }

        return null;
    }

    /// <summary>
    /// Add node to the end of the collection
    /// </summary>
    /// <param name="node"></param>
    public void Append(HtmlNode node)
    {
        HtmlNode? last = null;
        if (_items.Count > 0)
        {
            last = _items[^1];
        }

        _items.Add(node);
        node._prevnode = last;
        node._nextnode = null;
        node.SetParent(ParentNode);
        if (last is null)
        {
            return;
        }

        if (last == node)
        {
            throw new InvalidProgramException("Unexpected error.");
        }

        last._nextnode = node;
    }

    /// <summary>
    /// Get first instance of node with name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public HtmlNode? FindFirst(string name)
    {
        return FindFirst(this, name);
    }

    /// <summary>
    /// Get index of node
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int GetNodeIndex(HtmlNode node)
    {
        // TODO: should we rewrite this? what would be the key of a node?
        for (int i = 0; i < _items.Count; i++)
        {
            if (node == _items[i])
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Add node to the beginning of the collection
    /// </summary>
    /// <param name="node"></param>
    public void Prepend(HtmlNode node)
    {
        HtmlNode? first = null;
        if (_items.Count > 0)
        {
            first = _items[0];
        }

        _items.Insert(0, node);

        if (node == first)
        {
            throw new InvalidProgramException("Unexpected error.");
        }

        node._nextnode = first;
        node._prevnode = null;
        node.SetParent(ParentNode);

        _ = (first?._prevnode = node);
    }

    /// <summary>
    /// Remove node at index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool Remove(int index)
    {
        RemoveAt(index);
        return true;
    }

    /// <summary>
    /// Replace node at index
    /// </summary>
    /// <param name="index"></param>
    /// <param name="node"></param>
    public void Replace(int index, HtmlNode node)
    {
        HtmlNode? next = null;
        HtmlNode? prev = null;
        HtmlNode? oldnode = _items[index];

        if (index > 0)
        {
            prev = _items[index - 1];
        }

        if (index < (_items.Count - 1))
        {
            next = _items[index + 1];
        }

        _items[index] = node;

        if (prev is not null)
        {
            if (node == prev)
            {
                throw new InvalidProgramException("Unexpected error.");
            }

            prev._nextnode = node;
        }

        _ = (next?._prevnode = node);

        node._prevnode = prev;

        if (next == node)
        {
            throw new InvalidProgramException("Unexpected error.");
        }

        node._nextnode = next;
        node.SetParent(ParentNode);

        _ = (oldnode?._prevnode = null);
        _ = (oldnode?._nextnode = null);
        _ = (oldnode?._parentnode = null);
    }

    #endregion

    #region LINQ Methods

    /// <summary>
    /// Get all node descended from this collection
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Descendants()
    {
        foreach (HtmlNode? item in _items)
        {
            foreach (HtmlNode n in item?.Descendants()!)
            {
                yield return n;
            }
        }
    }

    /// <summary>
    /// Get all node descended from this collection with matching name
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Descendants(string name)
    {
        foreach (HtmlNode? item in _items)
        {
            foreach (HtmlNode n in item?.Descendants(name)!)
            {
                yield return n;
            }
        }
    }

    /// <summary>
    /// Gets all first generation elements in collection
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Elements()
    {
        foreach (HtmlNode? item in _items)
        {
            foreach (HtmlNode n in item?.ChildNodes!)
            {
                yield return n;
            }
        }
    }

    /// <summary>
    /// Gets all first generation elements matching name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<HtmlNode?> Elements(string name)
    {
        foreach (HtmlNode? item in _items)
        {
            foreach (HtmlNode n in item?.Elements(name)!)
            {
                yield return n;
            }
        }
    }

    /// <summary>
    /// All first generation nodes in collection
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode?> Nodes()
    {
        foreach (HtmlNode? item in _items)
        {
            foreach (HtmlNode n in item?.ChildNodes!)
            {
                yield return n;
            }
        }
    }

    #endregion
}