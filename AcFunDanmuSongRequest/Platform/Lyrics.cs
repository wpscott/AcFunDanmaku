using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AcFunDanmuSongRequest.Platform;

public sealed class Lyrics : INotifyPropertyChanged
{
    private static readonly Regex Regex =
        new(
            @"^(\[((?<duration>(?<miniute>\d\d)\:(?<second>\d\d\.\d+))|(?<author>ar)|(?<title>ti)|(?<album>al)|(?<by>by)|(?<offset>offset)|(?<other>.*?))\])?(?<content>.*?)$",
            RegexOptions.Compiled);

    private readonly bool _hasTimeSpan;

    private readonly Lyric[] _lines;
    private int _index;

    private string _line1;
    private string _line2;


    private Lyrics(in IEnumerable<string> lines)
    {
        _lines = Parse(lines, out _hasTimeSpan);
        if (_lines.Length > 0) Line1 = _lines[_index++].Content;

        if (_lines.Length > 1) Line2 = _lines[_index++].Content;
    }

    public string Line1
    {
        get => _line1;
        set
        {
            _line1 = value;
            NotifyPropertyChanged(nameof(Line1));
        }
    }

    public string Line2
    {
        get => _line2;
        set
        {
            _line2 = value;
            NotifyPropertyChanged(nameof(Line2));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void Tick(TimeSpan timeSpan)
    {
        if (!_hasTimeSpan) return;
        var current = _lines[_index - 1];
        if (current.TimeSpan > timeSpan) return;
        if (_index >= _lines.Length - 1) return;
        Line1 = Line2;
        Line2 = _lines[_index++].Content;
    }

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static Lyrics Parse(in string lyrics)
    {
        return new(lyrics.Split('\r', '\n'));
    }

    private static Lyric[] Parse(in IEnumerable<string> lines, out bool hasTimeSpan)
    {
        hasTimeSpan = true;
        var list = new List<Lyric>(lines.Count());

        foreach (var line in lines)
        {
            var matched = Regex.Match(line);
            if (!matched.Success || matched.Groups["content"].Length == 0) continue;

            Trace.WriteLine(matched.Groups["content"].Value);

            if (hasTimeSpan)
            {
                if (string.IsNullOrEmpty(matched.Groups["duration"].Value))
                {
                    hasTimeSpan = false;
                    list.Add(new Lyric(TimeSpan.Zero, matched.Groups["content"].Value.Trim()));
                }
                else
                {
                    var time = matched.Groups["duration"].Length == 9
                        ? TimeSpan.ParseExact(matched.Groups["duration"].ValueSpan, "mm\\:ss\\.fff",
                            CultureInfo.InvariantCulture)
                        : TimeSpan.ParseExact(matched.Groups["duration"].ValueSpan, "mm\\:ss\\.ff",
                            CultureInfo.InvariantCulture);
                    list.Add(new Lyric(time, matched.Groups["content"].Value.Trim()));
                }
            }
            else
            {
                list.Add(new Lyric(TimeSpan.Zero, matched.Groups["content"].Value.Trim()));
            }
        }

        return list.ToArray();
    }
}

public readonly record struct Lyric(TimeSpan TimeSpan, string Content);