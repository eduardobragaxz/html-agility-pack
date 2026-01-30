// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

#if !METRO

namespace HtmlAgilityPack;

internal struct IOLibrary
{
    #region Internal Methods

    internal static void CopyAlways(string? source, string target)
    {
        if (!File.Exists(source))
        {
            return;
        }

        string? directoryName = Path.GetDirectoryName(target);

        if (directoryName is not null)
        {
            _ = Directory.CreateDirectory(directoryName);
        }

        MakeWritable(target);
        File.Copy(source, target, true);
    }
#if !PocketPC && !WINDOWS_PHONE
    internal static void MakeWritable(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);
    }
#else
    internal static void MakeWritable(string path)
    {
    }
#endif
    #endregion
}

#endif